using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;
using VRTK;

public class ZoomByGrabbing : MonoBehaviour {
    bool gripClickedLeft = false;
    bool gripClickedRight = false;
    public VRTK_ControllerEvents controllerEventsLeft;
    public VRTK_ControllerEvents controllerEventsRight;
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
    
        Debug.Log("ttest");
        controllerEventsLeft.GripClicked += GripClickedLeft;
        controllerEventsLeft.GripUnclicked += GripUnclickedLeft;
        controllerEventsRight.GripClicked += GripClickedRight;
        controllerEventsRight.GripUnclicked += GripUnclickedRight;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (gripClickedLeft && gripClickedRight)
        {
            isScaling = true;
            originalDistanceBetweenHands = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            originalScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            pivot = new Vector2(VRCamera.transform.position.x, VRCamera.transform.position.z);
            pivotToTransformPosition = new Vector2(this.transform.position.x - VRCamera.transform.position.x, this.transform.position.z - VRCamera.transform.position.z);
        } 
        else if (!gripClickedLeft || !gripClickedRight)
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


    private void GripClickedLeft(object sender, ControllerInteractionEventArgs e)
    {
        gripClickedLeft = true;
        Debug.Log("clickleft");
    }
    private void GripUnclickedLeft(object sender, ControllerInteractionEventArgs e)
    {
        gripClickedLeft = false;
    }
    private void GripClickedRight(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("clickright");
        gripClickedRight = true;
    }
    private void GripUnclickedRight(object sender, ControllerInteractionEventArgs e)
    {
        gripClickedRight = false;
    }

}
