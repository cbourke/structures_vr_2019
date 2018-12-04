using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{


    public class pointer : MonoBehaviour {
        public float maxRayLength = 5;
        public float rayRadius = .05f;
	    public float laserWidth = 0.01f;
        public bool isEraser = false;
        public string workingElement = "frame";

	    public LineRenderer laserLineRenderer;
	    public Hand hand;
	    public GameObject constructorController;

        private RaycastHit vision;
	    private bool isGrabbed;
	    private Rigidbody grabbedObject;
        private LineRenderer tempLineRenderer;

	
	    void Start() {
		    // TODO we need a better way to set the tempLineRenderer because GameObject.FindGameobjectWithTag is very inefficient
		    tempLineRenderer = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LineRenderer>();
		    Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
		    laserLineRenderer.SetPositions( initLaserPositions );
		    laserLineRenderer.startWidth = laserWidth;
		    laserLineRenderer.endWidth = laserWidth;
		    laserLineRenderer.enabled = true;
	    }
 

	    void Update() {
            float scaledRayLength = maxRayLength * transform.lossyScale.x;
            ShootLaserFromTargetPosition(transform.position, transform.forward, scaledRayLength);
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
            int framesLayer = 1 << 9;
                if (Physics.Raycast(ray, out rayHit, maxLength, uiLayer))
                {
                    // used to hit UI elents
                    nodePoint = rayHit.transform.position;
                    endPosition = rayHit.point;
                    if (rayHit.collider.isTrigger && startingGrabType == GrabTypes.Pinch)
                    {
                        rayHit.collider.gameObject.GetComponent<uiCollider>().uiHit();
                    }
                }
                else
                {
                    // we might want to rewrite this to be able to handle more than 2 pointer modes
                    // and be more modular (lots of code is copied)
                    switch(isEraser)
                    {
                        case true:
                        {
                            if (Physics.SphereCast(ray, rayRadius, out rayHit, maxLength, framesLayer))
                            {
                                // used to hit existing frames for deletion
                                nodePoint = rayHit.transform.position;
                                endPosition = rayHit.point;
                                tempLineRenderer.SetPosition(1, endPosition);

                                if (startingGrabType == GrabTypes.Pinch)
                                {
                                    // User "grabs" a grid node
                                    constructorController.GetComponent<constructorController>().deleteFrame(rayHit.transform.gameObject.GetInstanceID());
                                }
                            }
                            break;
                        }
                        case false:
                        {
                                if (Physics.SphereCast(ray, rayRadius, out rayHit, maxLength, gridLayer))
                                {
                                    // used to hit the grid nodes
                                    nodePoint = rayHit.transform.position;
                                    endPosition = rayHit.point;
                                    tempLineRenderer.SetPosition(1, nodePoint);

                                    
                                }
                            if (startingGrabType == GrabTypes.Pinch)
                            {
                                // User "grabs" a grid node
                                constructorController.GetComponent<constructorController>().setPoint(tempLineRenderer.GetPosition(1), buildingObjects.Frame);
                            }
                            break;
                        }
                    }

                
                }
		    laserLineRenderer.SetPosition( 0, targetPosition );
		    laserLineRenderer.SetPosition( 1, endPosition );

	    }

        public void toggleEraserMode()
        {
            isEraser = !isEraser;
        }

        public void SetWorkingElement(string type)
        {
            workingElement = type;
        }
    }
}