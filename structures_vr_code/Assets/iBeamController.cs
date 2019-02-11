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
    }

    // Update is called once per frame
    void Update()
    {
        //SetDimensions((float) 0.5, (float) 0.5, (float) 0.2, (float)0.2, (float)2.0);
        SetDimensions(depth, flangeWidth, flangeThickness, webThickness, length);
    }

    void SetDimensions(float d, float flangeW, float flangeT, float webT, float length)
    {
        this.depth = d;
        this.webThickness = webT;
        this.flangeWidth = flangeW;
        this.flangeThickness = flangeT;
        this.length = length;

        Vector3 webScale = new Vector3(webThickness, depth, length);

        web.GetComponent<Transform>().localScale = webScale;

        Vector3 topScale = new Vector3(flangeThickness, flangeWidth, length);

        topFlange.GetComponent<Transform>().localScale = topScale;

        Vector3 bottomScale = new Vector3(flangeThickness, flangeWidth, length);

        bottomFlange.GetComponent<Transform>().localScale = bottomScale;

    }
}