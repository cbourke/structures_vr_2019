using System.Collections;
using System.Collections.Generic;
using VRTK;
using UnityEngine;

public class GridNodeBehavior : MonoBehaviour {
    public VRTK_InteractableObject linkedObject;
    public constructorController myConstructorController = null;
    public LineRenderer previewLineRenderer = null;
    public PointerController pointerController;
    bool canUse = true;
    private bool isSelected = false;
	// Use this for initialization
	void Start () {
        myConstructorController = GameObject.FindWithTag("gameControllers").GetComponent<constructorController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void VRTKUse()
    {
        if (canUse)
        {
            switch (pointerController.getPointerMode()){
                case pointerModes.node: {
                    myConstructorController.setPoint(this.transform.position, buildingObjects.Frame);
                    canUse = false;
                    break;
                }
                case pointerModes.frame: {
                    pointerController.selectionController.select(this);
                    break;
                }
                    
            }
        }
    }

    public void VRTKUnuse()
    {
        Debug.Log("UNUSE");
        canUse = true;
    }

    public void VRTKTouch()
    {
        if (previewLineRenderer.isVisible) {
            previewLineRenderer.SetPosition(1, this.transform.position);
        }
        
    }

    public void setPreviewLineRenderer(LineRenderer newPreviewLineRenderer)
    {
        this.previewLineRenderer = newPreviewLineRenderer;
    }

    public void setPointerController(PointerController pc){
        pointerController = pc;
    }

    public bool getSelected(){
		return isSelected;
	}

	public void setSelected(bool selected) {
		isSelected = selected;
	}
}
