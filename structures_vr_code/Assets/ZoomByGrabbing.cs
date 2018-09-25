using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ZoomByGrabbing : MonoBehaviour {
    public GameObject leftHand;
    public GameObject rightHand;
    bool isScaling = false;
    float originalDistanceBetweenHands;
    float currentDistanceBetweenHands;
    float scale;
    Vector3 originalScale;
	// Use this for initialization
	void Start () {
        Vector3 originalScale = this.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if ((leftHand.GetComponent<Hand>().grabGripAction.GetState(leftHand.GetComponent<Hand>().handType)
            && rightHand.GetComponent<Hand>().grabGripAction.GetStateDown(rightHand.GetComponent<Hand>().handType))

            ^ (rightHand.GetComponent<Hand>().grabGripAction.GetState(rightHand.GetComponent<Hand>().handType)
            && leftHand.GetComponent<Hand>().grabGripAction.GetStateDown(leftHand.GetComponent<Hand>().handType)))
        {
            isScaling = true;
            originalDistanceBetweenHands = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            originalScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        } else if (!leftHand.GetComponent<Hand>().grabGripAction.GetState(leftHand.GetComponent<Hand>().handType)
            || !rightHand.GetComponent<Hand>().grabGripAction.GetState(rightHand.GetComponent<Hand>().handType))
        {
            isScaling = false;
        }

        if (isScaling)
        {
            currentDistanceBetweenHands = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            scale = (originalDistanceBetweenHands - Mathf.Sqrt(currentDistanceBetweenHands));
            Vector3 scaleVector = new Vector3(scale, scale, scale);
            this.transform.localScale = new Vector3(originalScale.x + scale, originalScale.y + scale, originalScale.z + scale);
        }

    }
}
