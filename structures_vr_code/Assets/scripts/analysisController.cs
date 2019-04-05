using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analysisController : MonoBehaviour
{
    public  materialsController myMaterialController;
    public sectionController mySectionController;
    public constructorController myConstructorController;
    public SapTranslatorIpcHandler mySapTranslatorIPCHandler;
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
        mySapTranslatorIPCHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsJointDispl(" + resultObject.pointElm[1] + ", 0)");
        mySapTranslatorIPCHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsJointDispl(" + resultObject.pointElm[0] + ", 0)");
        
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


    public void visualizeDeformation(frameJointForce frameJointForceInput, frameForce frameForceInput, jointDispl jointDisplInputA, jointDispl jointDisplInputB)
    {
        string frameName = frameJointForceInput.obj[0];
        Frame targetFrame = myConstructorController.findFrame(frameName);
        FrameSection targetSection = mySectionController.findFrameSection(targetFrame.getSectionPropertyName());
        SplineMesh.Spline targetSpline = targetFrame.GetGameObject().GetComponentInChildren<SplineMesh.Spline>();
        /*
        double momentA = latestFrameJointForceResult.m2[0];
        double momentB = latestFrameJointForceResult.m2[1];
        double thetaA = jointDisplInput.r2[0];
        double deltaA = jointDisplInput.u3[0];
        double length = targetFrame.getLength();
        FrameSection targetSection = mySectionController.findFrameSection(targetFrame.getSectionPropertyName());
        double momentOfInertia = targetSection.SectProps.I33;
        double youngModulus = myMaterialController.findBuildingMaterialWithName(targetSection.GetMaterialName()).MPIsotropic.E;
        */
        
        double frameLength = targetFrame.getLength();
        double youngModulus = myMaterialController.findBuildingMaterialWithName(targetSection.GetMaterialName()).MPIsotropic.E; //This is constant for a frame span
        double momentOfInertia = targetSection.SectProps.I33; //This is constant for a frame span
        ; // This is constant for a frame span
        // The rest will be recomputed at each segmentation:
        SplineMesh.SplineNode[] splineNodesForward = new SplineMesh.SplineNode[frameForceInput.numberResults - 1];
        double[] deflectionsForward = new double[frameForceInput.numberResults];
        double[] deflectionsBackward = new double[frameForceInput.numberResults];
        double momentA = -1 * frameForceInput.m3[0];
        double momentB = frameForceInput.m3[1];
        //double momentA = frameForceInput.m3[0];
        //double momentB = -1 * frameForceInput.m3[1];
        double thetaA = jointDisplInputA.r3[0];
        double deltaA = jointDisplInputA.u2[0];
        double length = frameForceInput.objSta[1] - frameForceInput.objSta[0];
        double totalSpanDistance = 0;
        for (int i = 1; i <= frameForceInput.numberResults - 2; i++)
        {
            double x = length; //frameForceInput.objSta[i];
            //totalSpanDistance += length;

            //double proportion = totalSpanDistance / frameLength;

            double deflection = deflectionEquation(x, momentA, momentB, thetaA, deltaA, length, youngModulus, momentOfInertia);
            double angle = angleEquation(x, momentA, momentB, thetaA, length, youngModulus, momentOfInertia);

            //Vector3 newNodePosition = new Vector3(0, (float)proportion, (float)deflection * visualizationScale);
            //Vector3 newNodeDirection = new Vector3(0, (float)proportion + (float)0.001, (float)deflection * visualizationScale);
            //SplineMesh.SplineNode newNode = new SplineMesh.SplineNode(newNodePosition, newNodeDirection);
            //newNode.Up = new Vector3(0, 0, -1);
            //splineNodesForward[i] = newNode;
            //targetSpline.InsertNode(0 + i, newNode); //Insert the new node into the spline that controls this frame object's mesh
            deflectionsForward[i] = deflection;
            //Now, update the parameters for the next segment:
            momentA = -1 * frameForceInput.m3[i];
            momentB = frameForceInput.m3[i + 1];
            //momentA = frameForceInput.m3[i];
            //momentB = -1 * frameForceInput.m3[i + 1];
            thetaA = angle;
            deltaA = deflection;
            length = frameForceInput.objSta[i + 1] - frameForceInput.objSta[i];
        }

        //TODO: Now do it in reverse; then we can take the average points between the forward and reverse 
        SplineMesh.SplineNode[] splineNodesBackward = new SplineMesh.SplineNode[frameForceInput.numberResults - 1];
        int n = frameForceInput.numberResults - 2;
        momentA = -1 * frameForceInput.m3[n+1];
        momentB = frameForceInput.m3[n];
        //momentA = frameForceInput.m3[n+1];
        //momentB = -1 * frameForceInput.m3[n];
        thetaA = jointDisplInputB.r2[0];
        deltaA = jointDisplInputB.u3[0];
        length = frameForceInput.objSta[n+1] - frameForceInput.objSta[n];
        totalSpanDistance = frameLength;
        for (int i = n; i >= 1; i--)
        {
            double x = length;
            //totalSpanDistance -= length;
            //double proportion = totalSpanDistance / frameLength;

            double deflection = deflectionEquation(x, momentA, momentB, thetaA, deltaA, length, youngModulus, momentOfInertia);
            double angle = angleEquation(x, momentA, momentB, thetaA, length, youngModulus, momentOfInertia);

            //Vector3 newNodePosition = new Vector3(0, (float)proportion, (float)deflection * visualizationScale);
            //Vector3 newNodeDirection = new Vector3(0, (float)proportion + (float)0.001, (float)deflection * visualizationScale);
            //SplineMesh.SplineNode newNode = new SplineMesh.SplineNode(newNodePosition, newNodeDirection);
            //newNode.Up = new Vector3(0, 0, -1);
            //splineNodesBackward[i] = newNode;
            deflectionsBackward[i] = deflection;

            //Now, update the parameters for the next segment:
            momentA = -1 * frameForceInput.m3[i];
            momentB = frameForceInput.m3[i - 1];
            //momentA = frameForceInput.m3[i];
            //momentB = -1 * frameForceInput.m3[i - 1];
            thetaA = angle;
            deltaA = deflection;
            length = frameForceInput.objSta[i] - frameForceInput.objSta[i-1];
        }

        //Now add the spline points
        totalSpanDistance = 0;
        for (int i = 1; i <= n; i++)
        {
            double averageDeflection = (deflectionsForward[i] + deflectionsBackward[i]) / 2.0;
            length = frameForceInput.objSta[i] - frameForceInput.objSta[i-1];
            totalSpanDistance += length;
            double proportion = totalSpanDistance / frameLength;
            Vector3 newNodePosition = new Vector3(0, (float)proportion, (float)averageDeflection * visualizationScale);
            Vector3 newNodeDirection = new Vector3(0, (float)proportion + (float)0.001, (float)averageDeflection * visualizationScale);
            SplineMesh.SplineNode newNode = new SplineMesh.SplineNode(newNodePosition, newNodeDirection);
            newNode.Up = new Vector3(0, 0, -1);
            targetSpline.InsertNode(0 + i, newNode); //Insert the new node into the spline that controls this frame object's mesh
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
