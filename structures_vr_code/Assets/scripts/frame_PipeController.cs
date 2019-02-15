using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame_PipeController : MonoBehaviour
{
    public void SetDimensions(float diameter, float thickness)
    {
        Transform trans = gameObject.transform;
        trans.localScale = new Vector3(diameter, trans.localScale.y, diameter);
    }
}
