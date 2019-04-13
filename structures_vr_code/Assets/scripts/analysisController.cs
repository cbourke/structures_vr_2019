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
        double frameScale = targetFrame.getTransform().lossyScale.y;
        double frameLength = targetFrame.getLength();
        double baseX = 0;
        double baseY = 0;
        double baseZ = 0;
        Vector3 basePosition;

        while(targetSpline.nodes.Count < specialPointsJointDispl.numberResults)
        {

            targetSpline.AddNode(new SplineMesh.SplineNode(Vector3.zero, Vector3.zero));
        }

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
            basePosition = new Vector3((float)baseX, (float)baseY, (float)baseZ);

            Matrix4x4 globalToLocal = transform.worldToLocalMatrix;
            Matrix4x4 localToGlobal = transform.localToWorldMatrix;

            Vector3 basePositionGlobal = transform.TransformPoint(basePosition);

            double newPointDeltaXGlobal = (-1 * u1) * visualizationScale;
            double newPointDeltaYGlobal = (-1 * u2) * visualizationScale;
            double newPointDeltaZGlobal = (-1 * u3) * visualizationScale;
            Vector3 delta = new Vector3((float)newPointDeltaXGlobal, (float)newPointDeltaYGlobal, (float)newPointDeltaZGlobal);
            Vector3 deltaLocal = transform.InverseTransformVector(delta);

            //Vector3 newPointGlobal = basePositionGlobal + new Vector3((float)newPointDeltaXGlobal, (float)newPointDeltaYGlobal, (float)newPointDeltaZGlobal);

            //Vector3 newPointLocal = transform.InverseTransformPoint(basePositionGlobal);

            Vector3 newPointLocal = basePosition + deltaLocal;





            Vector3 updatedNodePosition = new Vector3(newPointLocal.x, newPointLocal.y, newPointLocal.z);
            Vector3 updatedNodeDirection = new Vector3(updatedNodePosition.x, updatedNodePosition.y + (float)0.001, updatedNodePosition.z);
            Vector3 updatedNodeUp = new Vector3(0, 0, -1);
            //lookAt = Quaternion.Euler((float)(r3 / frameScale), (float)(r1), (float)(r2 / frameScale)) * lookAt;

            //SplineMesh.SplineNode newSplineNode = new SplineMesh.SplineNode(nodePosition, lookAt);
            targetSpline.nodes[i].Position = updatedNodePosition;
            targetSpline.nodes[i].Direction = updatedNodeDirection;
            targetSpline.nodes[i].Up = updatedNodeUp;

        }

        

    }

    public void visualizeUndeformed(string frameName)
    {
        Frame targetFrame = myConstructorController.findFrame(frameName);
        targetFrame.setReleaseNeither();
        SplineMesh.Spline targetSpline = targetFrame.GetGameObject().GetComponentInChildren<SplineMesh.Spline>();
        int numNodesPrevious = targetSpline.nodes.Count;

        while (targetSpline.nodes.Count > 2)
        {
            targetSpline.RemoveNode(targetSpline.nodes[targetSpline.nodes.Count - 1]);
        }

        targetSpline.nodes[0].Position = new Vector3(0, 0, 0);
        targetSpline.nodes[0].Direction = new Vector3(0, (float)0.001, 0);
        targetSpline.nodes[1].Position = new Vector3(0, 1, 0);
        targetSpline.nodes[1].Position = new Vector3(0, (float)1.001, 0);
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
