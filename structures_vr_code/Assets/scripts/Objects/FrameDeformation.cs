using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameDeformation : MonoBehaviour
{
    // This class is modeled after FrameForce.
    // FrameForce is modeled after SAP2000's OAPI Function "SapObject.SapModel.Results.FrameForce".
    // This class was created because the OAPI does not provide information about
    // displacement (translation and rotation) of points along the length of a frame member.
    // It gives displacements at the endpoints ("")It only gives moments and forces for intermediate points.
    public string name; // the name of the Frame object, group, or element
    public string itemType;
    public int numberResults;
    public string[] obj;
    public double[] objSta;
    public string[] elm;
    public double[] elmSta;
    public string[] loadCase;
    public string[] stepType;
    public double[] stepNum;
    public double[] translation1;
    public double[] translation2;
    public double[] translation3;
    public double[] rotation1;
    public double[] rotation2;
    public double[] rotation3;
}
