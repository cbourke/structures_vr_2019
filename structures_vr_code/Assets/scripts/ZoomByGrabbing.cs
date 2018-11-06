using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class ZoomByGrabbing : MonoBehaviour {
    public GameObject leftHand;
    public GameObject rightHand;
    bool isScaling = false;
    float originalDistanceBetweenHands;
    float currentDistanceBetweenHands;
    float scale;
    Vector2 pivot;
    Vector2 pivotToTransformPosition;
    Vector3 originalScale;
    GameObject VRCamera;
    public bool isModel;


    // Use this for initialization
    void Start () {
        Vector3 originalScale = this.transform.localScale;
        VRCamera = GameObject.Find("VRCamera");
        pivot = new Vector2(VRCamera.transform.position.x, VRCamera.transform.position.y);
        pivotToTransformPosition = new Vector2(this.transform.position.x - VRCamera.transform.position.x, this.transform.position.y - VRCamera.transform.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        if ((leftHand.GetComponent<Hand>().grabGripAction.GetState(leftHand.GetComponent<Hand>().handType)
            && rightHand.GetComponent<Hand>().grabGripAction.GetStateDown(rightHand.GetComponent<Hand>().handType))

            || (rightHand.GetComponent<Hand>().grabGripAction.GetState(rightHand.GetComponent<Hand>().handType)
            && leftHand.GetComponent<Hand>().grabGripAction.GetStateDown(leftHand.GetComponent<Hand>().handType)))
        {
            isScaling = true;
            originalDistanceBetweenHands = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            originalScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            pivot = new Vector2(VRCamera.transform.position.x, VRCamera.transform.position.z);
            pivotToTransformPosition = new Vector2(this.transform.position.x - VRCamera.transform.position.x, this.transform.position.z - VRCamera.transform.position.z);
        } else if (!leftHand.GetComponent<Hand>().grabGripAction.GetState(leftHand.GetComponent<Hand>().handType)
            || !rightHand.GetComponent<Hand>().grabGripAction.GetState(rightHand.GetComponent<Hand>().handType))
        {
            isScaling = false;
        }

        if (isScaling)
        {
            currentDistanceBetweenHands = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            scale = Mathf.Sqrt((originalDistanceBetweenHands / currentDistanceBetweenHands));
            this.transform.localScale = new Vector3(Mathf.Clamp(originalScale.x * scale, 0.25f, 3f), 
                                                    Mathf.Clamp(originalScale.y * scale, 0.25f, 3f),
                                                    Mathf.Clamp(originalScale.z * scale, 0.25f, 3f));
            if (transform.localScale.x < 3f) {
                this.transform.position = new Vector3(pivot.x + (scale * pivotToTransformPosition.x), this.transform.position.y, pivot.y + (scale * pivotToTransformPosition.y));
            }

        }
    }   
}
