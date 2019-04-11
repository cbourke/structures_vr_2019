using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analysisController : MonoBehaviour
{
    public  materialsController myMaterialController;
    public sectionController mySectionController;
    public constructorController myConstructorController;
    public SapTranslatorIpcHandler mySapTranslatorIPCHandler;
    public int deflectionStationsPerFrame;
    public int resultHistorySlots;
    public float visualizationScale;

    private frameJointForce latestFrameJointForceResult;
    private List<frameJointForce> frameJointForceHistory = new List<frameJointForce>();

    private frameForce latestFrameForceResult;
    private List<frameForce> frameForceHistory = new List<frameForce>();

    private jointDispl latestJointDisplResult;
    public List<jointDispl> jointDisplHistory = new List<jointDispl>();

    
    public frameJointForce getLatestFrameJointForceResult()
    {
        return latestFrameJointForceResult;
    }

    public void setLatestFrameJointForceResult(frameJointForce resultObject)
    {
        latestFrameJointForceResult = resultObject;
        frameJointForceHistory.Insert(0, resultObject);
        if (frameJointForceHistory.Count > resultHistorySlots)
        {
            frameJointForceHistory.RemoveAt(frameJointForceHistory.Count - 1);
        }
        string debugMessage = "set latest FrameJointForce result: \n" + resultObject.ToString();
        Debug.Log(debugMessage);

        int numResults = resultObject.numberResults;
    }

    public frameForce getLatestFrameForceResult()
    {
        return latestFrameForceResult;
    }

    public void setLatestFrameForceResult(frameForce resultObject)
    {
        latestFrameForceResult = resultObject;
        frameForceHistory.Insert(0, resultObject);
        if (frameForceHistory.Count > resultHistorySlots)
        {
            frameForceHistory.RemoveAt(frameForceHistory.Count - 1);
        }
        string debugMessage = "set latest FrameForce result: \n" + resultObject.ToString();
        Debug.Log(debugMessage);

        int numResults = resultObject.numberResults;
    }


    public jointDispl getLatestJointDisplResult()
    {
        return latestJointDisplResult;
    }

    public void setLatestJointDisplResult(jointDispl resultObject)
    {
        latestJointDisplResult = resultObject;
        jointDisplHistory.Insert(0, resultObject);
        if (jointDisplHistory.Count > resultHistorySlots)
        {
            jointDisplHistory.RemoveAt(frameJointForceHistory.Count - 1);
        }
        string debugMessage = "set latest JointDispl result:\n" + resultObject.ToString();
        Debug.Log(debugMessage);

    }


    public void visualizeDeformation(string frameName, jointDispl specialPointsJointDispl)
    {
        Frame targetFrame = myConstructorController.findFrame(frameName);
        targetFrame.setReleaseNeither();
        SplineMesh.Spline targetSpline = targetFrame.GetGameObject().GetComponentInChildren<SplineMesh.Spline>();
        int numNodesPrevious = targetSpline.nodes.Count;
        double frameScale = targetFrame.getTransform().localScale.y;
        double frameLength = targetFrame.getLength();
        double baseX = 0;
        double baseY = 0;
        double baseZ = 0;

        int maxIndex = specialPointsJointDispl.numberResults - 1;
        for(int i = 0; i <= maxIndex; i++)
        {
            
            double u1 = specialPointsJointDispl.u1[i];
            double u2 = specialPointsJointDispl.u2[i];
            double u3 = specialPointsJointDispl.u3[i];
            double r1 = specialPointsJointDispl.r1[i];
            double r2 = specialPointsJointDispl.r2[i];
            double r3 = specialPointsJointDispl.r3[i];

            baseY = (double)i / (double)maxIndex;

            //The mapping of SAP's 1, 2, 3 axes to unity's x, y, z in the frame prefab is:
            // 1 = y
            // 2 = x
            // 3 = z

            double newPointX = baseX + (-1 * u2 * visualizationScale);
            double newPointY = baseY + ((u1 / frameScale) * visualizationScale);
            double newPointZ = baseZ + (-1* u3 * visualizationScale);

            Vector3 nodePosition = new Vector3((float)newPointX, (float)newPointY, (float)newPointZ);

            Vector3 lookAt = new Vector3((float)(newPointX), (float)(newPointY + 0.001), (float)(newPointZ));
            //lookAt = Quaternion.Euler((float)(r3 / frameScale), (float)(r1), (float)(r2 / frameScale)) * lookAt;

            SplineMesh.SplineNode newSplineNode = new SplineMesh.SplineNode(nodePosition, lookAt);
            newSplineNode.Up = new Vector3(0, 0, -1);
            targetSpline.AddNode(newSplineNode);
        }
        // Remove the nodes used for the previous shape
        for (int i = 0; i < numNodesPrevious; i++)
        {
            targetSpline.RemoveNode(targetSpline.nodes[0]);
        }


    }

    public static double deflectionEquation(double x, double m1, double m2, double thetaA, double deltaA, double length, double youngModulus, double momentOfInertia) //Derived by CIVE 498 students
    {
        // This equation was derived by the CIVE 498 students who helped the CSCE Senior Design team.
        // The equation returns the deflection at a point x along a frame member.
        // INPUTS:
        // x = the undeformed distance along the frame segment from the "A" node of the frame segment to the point in question.
        // m1 = the moment at node "A" of the frame. (Obtained from SAP2000)
        // m2 = the moment at node "B" of the frame. (Obtained from SAP2000)
        // thetaA = the rotation of node "A" in the direction of deflection being considered. (Obtained from SAP2000)
        // length = the total length of the frame segment being considered. (Obtained from frame)
        // youngModulus = the Young's Modulus of the frame member. (Obtained from SAP2000)
        // momentOfInertia = the Moment of Inertia of the frame member. (Obtained from SAP2000)

        double deflection;
        deflection = (1 / (youngModulus * momentOfInertia)) * (((m1 * (x * x)) / 2.0) - (((m1 + m2) / (6.0 * length)) * (x * x * x))) + (thetaA * x) + deltaA;
        //deflection = 1 / (youngModulus * momentOfInertia) * (((m1 * (x * x)) / 2) - (((m1 + m2) / (6 * length)) * (x * x * x))) + (deltaA);
        Debug.Log("Computed deflection = "+ deflection +"with youngModulus=" + youngModulus + "; momentOfInertia=" + momentOfInertia + "; m1=" + m1 + "; m2=" + m2 + "; length="+ length + "; deltaA="+deltaA+"; thetaA="+ thetaA +"; x=" + x);
        return deflection;
    }

    public static double angleEquation(double x, double m1, double m2, double thetaA, double length, double youngModulus, double momentOfInertia)
    {
        double angle;
        angle = (1 / (youngModulus * momentOfInertia)) * ((m1 * x) - (((m1 + m2) / (2.0 * length)) * (x * x))) + thetaA;
        Debug.Log("Computed angle = " + angle + "with youngModulus=" + youngModulus + "; momentOfInertia=" + momentOfInertia + "; m1=" + m1 + "; m2=" + m2 + "; length=" + length + "; thetaA=" + thetaA + "; x=" + x);
        return angle;
    }

}
