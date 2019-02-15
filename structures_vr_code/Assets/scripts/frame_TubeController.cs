﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame_TubeController : MonoBehaviour
{   
    public void SetDimensions(float outsideDepth, float outsideWidth, float flangeThickness, float webThickness)
    {
        Transform trans = gameObject.transform;
        trans.localScale = new Vector3(outsideDepth, trans.localScale.y, outsideWidth);
    }
}
