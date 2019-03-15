using System.Collections;
using System.Collections.Generic;
using VRTK;
using UnityEngine;

/* this class is attatched to each grid node */
/* it houses the functions that are called when a node is "used", "unused", etc */
public class GridNodeBehavior : MonoBehaviour {
    public VRTK_InteractableObject linkedObject;
    
    private constructorController myConstructorController;
    private LineRenderer previewLineRenderer;
    private PointerController myPointerController;
    private selectionController mySelectionController;

    bool canUse = true;
    private GridNode myGridNode;

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
            switch (myPointerController.getPointerMode()){
                case pointerModes.draw: {
                    bool shouldDeselect = myConstructorController.setPoint(this.transform.position, buildingObjects.Frame);
                    if(shouldDeselect) {
                        //deselect all the nodes
                        mySelectionController.clearDrawNode();
                    } else {
                        mySelectionController.setDrawNode(myGridNode);
                    }
                    canUse = false;
                    break;
                }
                case pointerModes.selectNode: {
                    myPointerController.selectionController.select(myGridNode);
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
        myPointerController = pc;
    }

    /// <summary>
    /// Sets the gridNode reference
    /// </summary>
    public void setMyGridNode(GridNode newGridNode)
    {
        myGridNode = newGridNode;
    }

    /// <summary>
    /// Returns the gridNode reference
    /// </summary>
    public GridNode getMyGridNode()
    {
        return myGridNode;
    }

    /// <summary>
    /// Sets the selection controller
    /// </summary>
    public void setSelectionController(selectionController newSelectionController)
    {
        mySelectionController = newSelectionController;
    }

    /// <summary>
    /// Returns the selection controller
    /// </summary>
    public selectionController getSelectionController()
    {
        return mySelectionController;
    }
}
