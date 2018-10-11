using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{


public class pointer : MonoBehaviour {
    private RaycastHit vision;
    public float rayLength;
	public float rayRadius;
	public float laserWidth = 0.01f;

	private bool isGrabbed;
	private Rigidbody grabbedObject;
	

	public LineRenderer laserLineRenderer;
	public Hand hand;
	public GameObject constructorController;

	void Start() {
	Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
	laserLineRenderer.SetPositions( initLaserPositions );
	laserLineRenderer.startWidth = laserWidth;
	laserLineRenderer.endWidth = laserWidth;
	laserLineRenderer.enabled = true;
	}
 

	void Update() {
		ShootLaserFromTargetPosition( transform.position, transform.forward, rayLength );
	}
 
	void ShootLaserFromTargetPosition( Vector3 targetPosition, Vector3 direction, float length ) {
		direction.y = direction.y - .5f;
		Ray ray = new Ray( targetPosition, direction );
		RaycastHit sphereHit;
		Vector3 endPosition = targetPosition + ( length * direction );
		Vector3 nodePoint;
		GrabTypes startingGrabType = hand.GetGrabStarting();

		if(Physics.SphereCast(ray, rayRadius, out sphereHit, rayLength)) {
			endPosition = sphereHit.point;
			nodePoint = sphereHit.transform.position;

            if (startingGrabType == GrabTypes.Pinch && sphereHit.collider.CompareTag("Grid")) {
                // User "grabs" a grid node
                constructorController.GetComponent<constructorController>().setPoint(nodePoint, buildingObjects.Frame);
            }
		}

		laserLineRenderer.SetPosition( 0, targetPosition );
		laserLineRenderer.SetPosition( 1, endPosition );
	}
}

}