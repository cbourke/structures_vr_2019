using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public enum pointerModes
{
    node,
    frame
}

/* This class controlls the pointer, i.e. switches between pointer modes */
public class PointerController : MonoBehaviour {
    public VRTK_Pointer nodePointer;
    public VRTK_Pointer framePointer;
    public VRTK_UIPointer UIPointer;
    public VRTK_Pointer TeleportPointer;
    public selectionController selectionController;

    private pointerModes mode;
    public pointerModes defaultPointerMode;

    /// <summary>
    /// Sets the default pointer mode
    /// </summary>
    void Start () {
        setPointerMode(defaultPointerMode);
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
            case pointerModes.node:
                {
                    Debug.Log("node active");
                    framePointer.gameObject.SetActive(false);
                    nodePointer.gameObject.SetActive(true);
                    
                    break;
                }
            case pointerModes.frame:
                {
                    Debug.Log("frame active");
                    framePointer.gameObject.SetActive(true);
                    nodePointer.gameObject.SetActive(false);
                    break;
                }
            default: { break; }
        }
    }

    /// <summary>
    /// Sets the pointer mode to node
    /// This is called when the Ui canvas is switched to the draw tool
    /// </summary>
    public void setPointerModeToNode()
    {
        setPointerMode(pointerModes.node);
    }

    /// <summary>
    /// Sets the pointer mode to frame
    /// This is called when the Ui canvas is switched to the select tool
    /// </summary>
    public void setPointerModeToFrame()
    {
        setPointerMode(pointerModes.frame);
    }
}
