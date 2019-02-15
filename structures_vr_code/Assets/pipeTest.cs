using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeTest : MonoBehaviour
{
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
    	trans.LookAt(Vector3.zero);
        trans.rotation *= Quaternion.Euler(90, 90, 90);
    }
}
