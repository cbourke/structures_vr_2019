using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamController : MonoBehaviour {

	public GameObject beam;
    Vector3 p1;
    Vector3 p2;
    bool hasPoint = false;

	// Use this for initialization
	void Start () {
    }

    public void setPoint(Vector3 point)
    {
        if(!hasPoint)
        {
            p1 = point;
            hasPoint = true;
        } else
        {
            if(p1 != point)
            {
                p2 = point;
                createBeam(p1, p2);
                hasPoint = false;
            }
        }
    }
	
	void createBeam(Vector3 pA, Vector3 pB) {
    	Vector3 between = pB - pA;

    	float distance = between.magnitude;
		Vector3 angle = new Vector3(Vector3.Angle(between, transform.right), Vector3.Angle(between, transform.up), Vector3.Angle(between, transform.forward));
		Transform trans = beam.transform;

    	trans.position = pA + (between / 2.0f);
		trans.Rotate(angle);
    	trans.LookAt(pB);
		trans.localScale = new Vector3(.05f, .05f, distance);

		Instantiate(beam, trans.position, trans.rotation);
	}
	// Update is called once per frame
	void Update () {
		
	}
}

public class Beam { 
	public Vector3 startPos {get; set;}
	public Vector3 endPos {get; set;}
	private Vector3 direction;

	public Beam(Vector3 start, Vector3 end) {
		direction = start - end;
	}

	public override string ToString()
    {
		return "string";
    }
}

