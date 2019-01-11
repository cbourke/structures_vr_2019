using System.Collections;
using System.Collections.Generic;
using VRTK;
using UnityEngine;

public class GridNodeBehavior : MonoBehaviour {
    public VRTK_InteractableObject linkedObject;
    public GameObject constructorController = null;
    public LineRenderer previewLineRenderer = null;
    bool canUse = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void VRTKUse()
    {
        if (canUse)
        {
            constructorController.GetComponent<constructorController>().setPoint(this.transform.position, buildingObjects.Frame);
            canUse = false;
        }
    }

    public void VRTKUnuse()
    {
        canUse = true;
    }

    public void VRTKTouch()
    {
        if (previewLineRenderer.isVisible) {
            previewLineRenderer.SetPosition(1, this.transform.position);
        }
    }

    public void setConstructorController(GameObject newConstructorController)
    {
        this.constructorController = newConstructorController;
    }

    public void setPreviewLineRenderer(LineRenderer newPreviewLineRenderer)
    {
        this.previewLineRenderer = newPreviewLineRenderer;
    }
}
