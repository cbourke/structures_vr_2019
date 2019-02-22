using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame_PipeController : MonoBehaviour
{
    public GameObject frame;

    public void SetDimensions(float diameter, float thickness)
    {
        Transform trans = frame.transform;
        trans.localScale = new Vector3(diameter, trans.localScale.y, diameter);
    }
}
