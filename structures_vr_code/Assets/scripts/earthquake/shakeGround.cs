using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeGround : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;
    

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        Vector3 startingForce = new Vector3(this.thrust, 0.0f, this.thrust);
        rb.AddForce(startingForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update () {
        Vector3 velocity = rb.velocity;
        float forceX = velocity.x * -2;
        float forceZ = velocity.z * -2;
        Vector3 forceNew = new Vector3(forceX, 0f, forceZ);
        rb.AddForce(forceNew, ForceMode.Impulse);
    }
}
