using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{


public class pointer : MonoBehaviour {
    private RaycastHit vision;
    public float rayLength = 5;
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
		    ShootLaserFromTargetPosition( transform.position, transform.forward, rayLength );
	}
 
	void ShootLaserFromTargetPosition( Vector3 targetPosition, Vector3 direction, float length ) {
		    direction.y = direction.y - .5f; //adjust the angle of the laser down

		    Vector3 startPosition = targetPosition - (.2f * direction);
		    Vector3 endPosition = targetPosition + ( length * direction );

		    Ray ray = new Ray( startPosition, direction );
		    RaycastHit sphereHit;
		    Vector3 nodePoint;
		    GrabTypes startingGrabType = hand.GetGrabStarting();

		    int layerMask = 1 << 8; //gets layer 8

            if (Physics.SphereCast(ray, rayRadius, out sphereHit, rayLength, layerMask)) {
			    nodePoint = sphereHit.transform.position;
			    endPosition = sphereHit.point;

                tempLineRenderer.SetPosition(1, endPosition);

                if (startingGrabType == GrabTypes.Pinch && sphereHit.collider.CompareTag("Grid") {
                    // User "grabs" a grid node
                    constructorController.GetComponent<constructorController>().setPoint(nodePoint, buildingObjects.Frame);
                }

                /*this if statement is for the frame_mask*/

                else if (startingGrabType == GrabTypes.Pinch && sphereHit.collider.CompareTag("Frame") {
                    int frameID = sphereHit.transform.GetInstanceID();
                    constructorController.GetComponent<constructorController>().deleteFrame(frameID);
                }
            }

		    laserLineRenderer.SetPosition( 0, targetPosition );
		    laserLineRenderer.SetPosition( 1, endPosition );
	}
}

}