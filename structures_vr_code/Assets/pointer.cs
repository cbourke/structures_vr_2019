using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pointer : MonoBehaviour {
    private RaycastHit vision;
    public float rayLength;
	public float rayRadius;
	public float laserWidth = 0.01f;

	private bool isGrabbed;
	private Rigidbody grabbedObject;
	

	public LineRenderer laserLineRenderer;

	void Start() {
	Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
	laserLineRenderer.SetPositions( initLaserPositions );
	laserLineRenderer.startWidth = laserWidth;
	laserLineRenderer.endWidth = laserWidth;
	laserLineRenderer.enabled = true;
	}
 
 	void FixedUpdate() {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        //if (Physics.Raycast(transform.position, fwd, 10))
        //    print("There is something in front of the object!");
    }

	void Update() {
		ShootLaserFromTargetPosition( transform.position, transform.forward, rayLength );
	}
 
	void ShootLaserFromTargetPosition( Vector3 targetPosition, Vector3 direction, float length ) {
		Ray ray = new Ray( targetPosition, direction );
		RaycastHit sphereHit;
		Vector3 endPosition = targetPosition + ( length * direction );

		if(Physics.SphereCast(ray, rayRadius, out sphereHit, rayLength)) {
			Debug.Log("WOW! SOMETHING HIT!" + sphereHit.point);
			endPosition = sphereHit.point;
		}

		laserLineRenderer.SetPosition( 0, targetPosition );
		laserLineRenderer.SetPosition( 1, endPosition );
	}
}
