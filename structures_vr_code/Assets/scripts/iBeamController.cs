using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iBeamController : MonoBehaviour
{

    public float depth;
    public float webThickness;
    public float flangeWidth;
    public float flangeThickness;
    public float length;

    public GameObject web;  //cubeMid
    public GameObject topFlange; //cubeTop
    public GameObject bottomFlange; //cubeBottom

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

    Transform trans;

    // Use this for initialization
    void Start()
    {
        trans = GetComponent<Transform>();
        SetDimensions((float) 0.5, (float) 0.5, (float) 0.2, (float)0.2, (float)2.0);
    }

    // Update is called once per frame
    void Update()
    {
        //SetDimensions(depth, flangeWidth, flangeThickness, webThickness,  length);
    }

    void SetDimensions(float d, float flangeW, float flangeT, float webT, float length)
    {
        this.depth = d;
        this.flangeWidth = flangeW;
        this.flangeThickness = flangeT;
        this.webThickness = webT;
        this.length = 1;

        Vector3 webScale = new Vector3(webThickness, depth, length);
        web.GetComponent<Transform>().localScale = webScale;

        Vector3 topScale = new Vector3(flangeWidth, flangeThickness,  length);
        topFlange.GetComponent<Transform>().localScale = new Vector3 (topScale.x / webScale.x, topScale.y / webScale.y, topScale.z / webScale.z);
        topFlange.GetComponent<Transform>().localPosition = new Vector3 (0, (float)0.5 - (flangeThickness / 2)/webScale.y, 0);
        Vector3 bottomScale = new Vector3(flangeWidth, flangeThickness, length);
        bottomFlange.GetComponent<Transform>().localScale = new Vector3 (bottomScale.x / webScale.x, bottomScale.y / webScale.y, bottomScale.z / webScale.z);
        bottomFlange.GetComponent<Transform>().localPosition = new Vector3 (0, (float)-0.5 + (flangeThickness / 2)/webScale.y, 0);

    }
}