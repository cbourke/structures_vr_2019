using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shakeGround : MonoBehaviour
{
    public float minThrust;
    public float maxThrust;
    public Rigidbody rb;
    public Rigidbody buildingRB;
    public Button eqButton;
    private bool eq = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        eqButton.onClick.AddListener(() => shakeOnClick(minThrust, maxThrust));
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("update");

        if (eq)
        {
            float forceZ = Random.Range(minThrust, maxThrust);
            float forceX = Random.Range(minThrust, maxThrust);

            Vector3 forceNew = new Vector3(forceX, 0, forceZ);
           
            rb.AddForce(forceNew, ForceMode.Impulse);
        }
        

    }

    void shakeOnClick(float minT, float maxT)
    {
        Debug.Log("EQ Begin");
        eq = !eq;
        if (eq)
        {
            buildingRB.freezeRotation = false;
            eqButton.GetComponentInChildren<Text>().text = "Stop Earthquake";
        } else
        {
           buildingRB.freezeRotation = true;
            eqButton.GetComponentInChildren<Text>().text = "Start Earthquake";
        }
        /*
        while (endTime > Time.time)
        {
            float forceX = Random.Range(minT, maxT);
            Vector3 forceNew = new Vector3(forceX, 0, 0);
            rb.AddForce(forceNew, ForceMode.Impulse);
        }
        */
    }
}
