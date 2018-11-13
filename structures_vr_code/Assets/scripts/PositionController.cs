using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PositionController : MonoBehaviour {

    public bool isModel;

	private ZoomByGrabbing zoomComponent;
	private Teleport teleportComponent;
	private GameObject teleportObject;

	Vector3 modelPosition = new Vector3(0f, -3.75f, 0f);
	Vector3 modelScale = new Vector3(5f, 5f, 5f);

	private Vector3 originalPosition = new Vector3(0f,0f,0f);
	private Vector3 originalScale = new Vector3(1f,1f,1f);

	// Use this for initialization
	void Start () {
		teleportObject = GameObject.Find("Teleporting");

		zoomComponent = this.GetComponent<ZoomByGrabbing>();
		teleportComponent = teleportObject.GetComponentInChildren<Teleport>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toggleModelMode() {
		isModel = !isModel;

		if(isModel) {
			zoomComponent.enabled = false;
			teleportComponent.enabled = false;
			SetPosition(modelPosition, modelScale);
		}
		else if (!isModel) {
			zoomComponent.enabled = true;
			teleportComponent.enabled = true;
			SetPosition(originalPosition, originalScale);
		}
    }

	void SetPosition(Vector3 position, Vector3 scale) {
		transform.localScale = scale;
		transform.position = position;
	}
}
