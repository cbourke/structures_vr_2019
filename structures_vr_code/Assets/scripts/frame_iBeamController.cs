﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class frame_iBeamController : MonoBehaviour
{

    private float outsideHeight;
    private float webThickness;
    private float flangeWidth;
    private float flangeThickness;

    public GameObject web;  //cubeMid
    public GameObject topFlange; //cubeTop
    public GameObject bottomFlange; //cubeBottom
    public frameReference reference;

    public VRTK_InteractObjectHighlighter hl1;
    /*
                 |  | - web Thickness
       { +-------------------+
       { |                   |
       { +-------+  +--------+
       {         |  |
       {         |  |
       {         |  |
  depth{         |  |
       {         |  |
       {         |  |
       {         |  |
       { +-------+  +--------+ }
       { |                   | }= Flange thickness
       { +-------------------+ }
         |___________________|
             flange Width
    */


    public void SetDimensions(float outsideHeight, float flangeW, float flangeT, float webT, double bottomFlangeWidth, double bottomFlangeThickness)
    {
        //currently both the top and bottom flange are being set to the same width and thickness
        this.outsideHeight = outsideHeight;
        this.flangeWidth = flangeW;
        this.flangeThickness = flangeT;
        this.webThickness = webT;

        Vector3 webScale = new Vector3(webThickness, outsideHeight, 1);
        web.GetComponent<Transform>().localScale = webScale;

        Vector3 topScale = new Vector3(flangeWidth, flangeThickness,  1);
        topFlange.GetComponent<Transform>().localScale = new Vector3 (topScale.x / webScale.x, topScale.y / webScale.y, topScale.z / webScale.z);
        topFlange.GetComponent<Transform>().localPosition = new Vector3 (0, (float)0.5 - (flangeThickness / 2)/webScale.y, 0);
        Vector3 bottomScale = new Vector3(flangeWidth, flangeThickness, 1);
        bottomFlange.GetComponent<Transform>().localScale = new Vector3 (bottomScale.x / webScale.x, bottomScale.y / webScale.y, bottomScale.z / webScale.z);
        bottomFlange.GetComponent<Transform>().localPosition = new Vector3 (0, (float)-0.5 + (flangeThickness / 2)/webScale.y, 0);

        highlightObject(true);
    }

    public void setHiglight() {
        Debug.Log("SET highlight");
        highlightObject(reference.getMyFrame().getSelected());
    }

    private void highlightObject(bool isHighlight) {
        Debug.Log("BOOL: " + isHighlight);
        if(isHighlight) {
            hl1.Highlight(Color.blue);
        } else {
            hl1.Unhighlight();
        }
    }
}