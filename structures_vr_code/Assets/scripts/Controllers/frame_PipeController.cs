using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is attatched to the pipe frame prefab */
/* It is used for correctly scaling the prefab */
public class frame_PipeController : MonoBehaviour
{
    public GameObject frame;
    public GameObject center;

    /// <summary>
    /// Sets the frames prefab to be the correct sizes
    /// </summary>
    public void SetDimensions(float diameter, float thickness)
    {
        Transform trans = frame.transform;
        trans.localScale = new Vector3(diameter, trans.localScale.y, diameter);
        float centerThickness = diameter - (2*thickness);
        center.transform.localScale = new Vector3(centerThickness, center.transform.localScale.y, centerThickness);
    }
}
