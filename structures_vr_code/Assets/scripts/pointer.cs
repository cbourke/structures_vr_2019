using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{


public class pointer : MonoBehaviour {
    private RaycastHit vision;
    public float maxRayLength = 5;
	public float rayRadius = .05f;
	public float laserWidth = 0.01f;

	private bool isGrabbed;
	private Rigidbody grabbedObject;
	
    public LineRenderer tempLineRenderer;

	public LineRenderer laserLineRenderer;
	public Hand hand;
	public GameObject constructorController;

	void Start() {
            tempLineRenderer = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LineRenderer>();

            Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
	        laserLineRenderer.SetPositions( initLaserPositions );
	        laserLineRenderer.startWidth = laserWidth;
	        laserLineRenderer.endWidth = laserWidth;
	        laserLineRenderer.enabled = true;
	}
 

	void Update() {
		ShootLaserFromTargetPosition( transform.position, transform.forward, maxRayLength );
	}
 
	void ShootLaserFromTargetPosition( Vector3 targetPosition, Vector3 direction, float maxLength ) {
		direction.y = direction.y - .5f; //adjust the angle of the laser down

		Vector3 startPosition = targetPosition - (.2f * direction);
		Vector3 endPosition = targetPosition + ( maxLength * direction );

		Ray ray = new Ray( startPosition, direction );
		RaycastHit rayHit;
		Vector3 nodePoint;
		GrabTypes startingGrabType = hand.GetGrabStarting();

		int gridLayer = 1 << 8;
		int uiLayer = 1 << 5;
		if(Physics.Raycast(ray, out rayHit, maxLength, uiLayer)) {
			// used to hit UI elents
			nodePoint = rayHit.transform.position;
			endPosition = rayHit.point;
			if(rayHit.collider.isTrigger && startingGrabType == GrabTypes.Pinch) {
				rayHit.collider.gameObject.GetComponent<uiCollider>().uiHit();
			}	
		} else if(Physics.SphereCast(ray, rayRadius, out rayHit, maxLength, gridLayer)) {
			// used to hit the grid nodes
			nodePoint = rayHit.transform.position;
			endPosition = rayHit.point;
			tempLineRenderer.SetPosition(1, endPosition);

            if (startingGrabType == GrabTypes.Pinch) {
                // User "grabs" a grid node
                constructorController.GetComponent<constructorController>().setPoint(nodePoint, buildingObjects.Frame);
            }
		}
		laserLineRenderer.SetPosition( 0, targetPosition );
		laserLineRenderer.SetPosition( 1, endPosition );

	}
}

}