using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame_TubeController : MonoBehaviour
{   
    public GameObject frame;

    public void SetDimensions(float outsideDepth, float outsideWidth, float flangeThickness, float webThickness)
    {
        Transform trans = frame.transform;
        trans.localScale = new Vector3(outsideWidth, trans.localScale.y, outsideDepth);
    }
}
