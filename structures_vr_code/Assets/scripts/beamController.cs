using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamController : MonoBehaviour {

	public GameObject beam;

	// Use this for initialization
	void Start () {
		Vector3 origin = new Vector3(0,0,0);
		Vector3 p1 = new Vector3(0,0,1);
		Vector3 p2 = new Vector3(0,1,0);
		Vector3 p3 = new Vector3(0,1,1);
		Vector3 p4 = new Vector3(1,0,0);
		Vector3 p5 = new Vector3(1,0,1);
		Vector3 p6 = new Vector3(1,1,0);
		Vector3 p7 = new Vector3(1,1,1);
		
		createBeam(origin, p1);
		createBeam(origin, p2);
		createBeam(origin, p3);
		createBeam(origin, p4);
		createBeam(origin, p5);
		createBeam(origin, p6);
		createBeam(origin, p7);

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

