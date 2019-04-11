using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analysisController : MonoBehaviour
{
    public SapTranslatorIpcHandler mySapTranslatorIPCHandler;
    public int resultHistorySlots;

    private frameJointForce latestFrameJointForceResult;
    private List<frameJointForce> frameJointForceHistory = new List<frameJointForce>();

    private jointDispl latestJointDisplResult;
    private List<jointDispl> jointDisplHistory = new List<jointDispl>();

    
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
        string debugMessage = "set latest FrameJointForce result:\n" + resultObject.ToString();
        Debug.Log(debugMessage);
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

    public double deflectionEquation(double x, double m1, double m2, double thetaA, double deltaA, double length, double youngModulus, double momentOfInertia) //Derived by CIVE 498 students
    {
        // This equation was derived by the CIVE 498 students who helped the CSCE Senior Design team.
        // The equation returns the deflection at a point x along a frame member.
        // INPUTS:
        // x = the undeformed distance along the frame member from the "A" node of the frame member to the point in question.
        // m1 = the moment at node "A" of the frame. (Obtained from SAP2000)
        // m2 = the moment at node "B" of the frame. (Obtained from SAP2000)
        // thetaA = the rotation of node "A" in the direction of deflection being considered. (Obtained from SAP2000)
        // length = the total length of the frame being considered. (Obtained from frame)
        // youngModulus = the Young's Modulus of the frame member. ??? Is this dependent on the cross-section and material? It might be obtainable from SAP2000. Else, user input.
        // momentOfInertia = the Moment of Inertia of the frame member. ??? Is this dependent on the cross-section and material? It might be obtainable from SAP2000. Else, user input.

        double deflection;
        deflection = 1 / (youngModulus * momentOfInertia) * ((m1 * (x * x) / 2) - ((m1 + m2) / (6 * length) * (x * x * x))) + ((thetaA * x) + deltaA);
        return deflection;
    }
}
