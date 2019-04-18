using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public enum pointerModes
{
    draw,
    selectFrame,
    selectNode,
    UI
}

/* This class controlls the pointer, i.e. switches between pointer modes */
/* NOTE this script is attatched to the right controller scripts, it is not in the gameControllers gameobejct */
public class PointerController : MonoBehaviour {
    public VRTK_Pointer nodePointer;
    public VRTK_Pointer framePointer;
    public VRTK_Pointer UIPointer;

    public VRTK_StraightPointerRenderer nodeRenderer;
    public VRTK_StraightPointerRenderer frameRenderer;
    public VRTK_StraightPointerRenderer uiRenderer;
    public GameObject scaleTarget;

    public VRTK_Pointer TeleportPointer;
    public selectionController selectionController;

    private pointerModes mode;
    private pointerModes drawSelectState;
    public float scale = 1;

    /// <summary>
    /// Sets the default pointer mode
    /// </summary>
    void Start () {
        setPointerModeToUI();
    }

    /// <summary>
    /// Scale the pointers based on the size of the player
    /// </summary>
    void Update()
    {
        nodeRenderer.scaleFactor = scaleTarget.transform.localScale.x*.001f;
        frameRenderer.scaleFactor = scaleTarget.transform.localScale.x*.001f;
        uiRenderer.scaleFactor = scaleTarget.transform.localScale.x*.001f;
    }
	
    /// <summary>
    /// Returns the current pointer mode
    /// </summary>
    public pointerModes getPointerMode()
    {
        return mode;
    }

    /// <summary>
    /// Sets the pointer mode
    /// The way we are handling this is to toggle whether each gameobject containing the pointer scripts is active.
    /// These gameobjects are under the ControllerScripts/RightController in the Unity editor
    /// </summary>
    public void setPointerMode(pointerModes newMode)
    {
        mode = newMode;
        switch(newMode) {
            case pointerModes.UI:
            {
                Debug.Log("Pointer mode: " + newMode);
                framePointer.gameObject.SetActive(false);
                nodePointer.gameObject.SetActive(false);
                UIPointer.gameObject.SetActive(true);
                break;
            }
            case pointerModes.draw:
            case pointerModes.selectNode:
            {
                Debug.Log("Pointer mode: " + newMode);
                framePointer.gameObject.SetActive(false);
                nodePointer.gameObject.SetActive(true);
                UIPointer.gameObject.SetActive(false);
                break;
            }
            case pointerModes.selectFrame:
            {
                Debug.Log("Pointer mode: select frame");
                framePointer.gameObject.SetActive(true);
                nodePointer.gameObject.SetActive(false);
                UIPointer.gameObject.SetActive(false);
                break;
            }
            default: { break; }
        }
    }

    /// <summary>
    /// Sets the pointer mode to node
    /// This is called when the Ui canvas is switched to the draw tool
    /// </summary>
    public void setPointerModeToDraw()
    {
        drawSelectState = pointerModes.draw;
        setPointerMode(pointerModes.draw);
    }

    /// <summary>
    /// Sets the pointer mode to frame select
    /// This is called when the Ui canvas is switched to the select tool
    /// </summary>
    public void setPointerModeToFrameSelect()
    {
        drawSelectState = pointerModes.selectFrame;
        setPointerMode(pointerModes.selectFrame);
    }

    /// <summary>
    /// Sets the pointer mode to node select
    /// This is called when the Ui canvas is switched to placing restraints
    /// </summary>
    public void setPointerModeToNodeSelect()
    {
        setPointerMode(pointerModes.selectNode);
    }
    
    /// <summary>
    /// Sets the pointer mode to only UI
    /// This is called when the Ui canvas is switched off of a UI function that requires the pointer to 
    /// hit nodes or frames
    /// </summary>
    public void setPointerModeToUI()
    {
        setPointerMode(pointerModes.UI);
    }

    /// <summary>
    /// Sets the pointer mode to the previos Draw/Select mode
    /// This is needed because draw and select frames are used in the same Ui canvas, so if a user 
    /// switches off the canvas to a different menu item on the radial menu, we need to save their state
    /// so when they return the correct pointer is activated
    /// </summary>
    public void setPointerModeToPreviousDrawSelect()
    {
        switch(drawSelectState) {
            case pointerModes.draw:
            {
                setPointerModeToDraw();
                break;
            }
            case pointerModes.selectFrame:
            {
                setPointerModeToFrameSelect();
                break;
            }
            default: { break; }
        }
    }
}
