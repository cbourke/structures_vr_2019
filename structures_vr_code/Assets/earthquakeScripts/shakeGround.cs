using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeGround : MonoBehaviour
{
    public float minThrust;
    public float maxThrust;
    public Rigidbody rb;
    

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        Vector3 velocity = rb.velocity;
        float forceX = Random.Range(minThrust, maxThrust);
        Vector3 forceNew = new Vector3(forceX, 0, 0);

        rb.AddForce(forceNew, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("update");
        
        //float forceZ = Random.Range(minThrust, maxThrust);
       

        //forceX = -(forceX);
        //forceZ = -Random.Range(minThrust, maxThrust);

        //forceNew = new Vector3(forceX, 0, 0);
        //rb.AddForce(forceNew, ForceMode.Impulse);

    }
}
