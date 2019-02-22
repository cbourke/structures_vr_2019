using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame_PipeController : MonoBehaviour
{
    public GameObject frame;
    public GameObject center;

    public void SetDimensions(float diameter, float thickness)
    {
        Transform trans = frame.transform;
        trans.localScale = new Vector3(diameter, trans.localScale.y, diameter);
        float centerThickness = diameter - (2*thickness);
        center.transform.localScale = new Vector3(centerThickness, center.transform.localScale.y, centerThickness);
    }
}
