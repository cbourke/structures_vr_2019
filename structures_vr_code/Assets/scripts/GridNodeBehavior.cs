using System.Collections;
using System.Collections.Generic;
using VRTK;
using UnityEngine;

/* this class is attatched to each grid node */
/* it houses the functions that are called when a node is "used", "unused", etc */
public class GridNodeBehavior : MonoBehaviour {
    public VRTK_InteractableObject linkedObject;
    public constructorController myConstructorController = null;
    public LineRenderer previewLineRenderer = null;
    public PointerController pointerController;
    bool canUse = true;
    private bool isSelected = false;

	void Start () {
        myConstructorController = GameObject.FindWithTag("gameControllers").GetComponent<constructorController>();
	}

    /// <summary>
    /// Either selects the node or sets a point for drawing depending on what the current pointer mode is
    /// </summary>
    public void VRTKUse()
    {
        Debug.Log("Gridnode USE");
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

    /// <summary>
    /// Sets canUse to true. This bool is needed to prevent clicking a node to register multiple times
    /// </summary>
    public void VRTKUnuse()
    {
        Debug.Log("Gridnode UNUSE");
        canUse = true;
    }

    /// <summary>
    /// Used to set the preview line renderer
    /// </summary>
    public void VRTKTouch()
    {
        if (previewLineRenderer.isVisible) {
            previewLineRenderer.SetPosition(1, this.transform.position);
        }
        
    }

    /// <summary>
    /// Sets the line renderer component
    /// </summary>
    public void setPreviewLineRenderer(LineRenderer newPreviewLineRenderer)
    {
        this.previewLineRenderer = newPreviewLineRenderer;
    }

    /// <summary>
    /// Sets the pointer controller component
    /// </summary>
    public void setPointerController(PointerController pc){
        pointerController = pc;
    }

    /// <summary>
    /// Returns selection state
    /// </summary>
    public bool getSelected(){
		return isSelected;
	}

    /// <summary>
    /// Sets selection state
    /// @TODO needs to highlight the node if it is being drawn with
    /// </summary>
	public void setSelected(bool selected) {
		isSelected = selected;
	}
}
