using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame_TubeController : MonoBehaviour
{   
    public GameObject frame;
    public GameObject center;

    public void SetDimensions(float outsideDepth, float outsideWidth, float flangeThickness, float webThickness)
    {
        Transform trans = frame.transform;
        trans.localScale = new Vector3(outsideWidth, trans.localScale.y, outsideDepth);
        float flange = outsideDepth - (2*flangeThickness);
        float web = outsideWidth - (2*webThickness);
        center.transform.localScale = new Vector3(web, center.transform.localScale.y, flange);
    }
}
