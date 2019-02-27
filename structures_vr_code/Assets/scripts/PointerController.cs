using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum pointerModes
{
    node,
    frame
}

public class PointerController : MonoBehaviour {
    public VRTK.VRTK_Pointer nodePointer;
    public VRTK.VRTK_Pointer framePointer;
    public VRTK.VRTK_UIPointer UIPointer;
    public VRTK.VRTK_Pointer TeleportPointer;
    public selectionController selectionController;

    private pointerModes mode;
    public pointerModes defaultPointerMode;

    // Use this for initialization
    void Start () {
        setPointerMode(defaultPointerMode);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public pointerModes getPointerMode()
    {
        return mode;
    }

    public void setPointerMode(pointerModes newMode)
    {
        Debug.Log("SETMODE");
        mode = newMode;
        switch(newMode) {
            case pointerModes.node:
                {
                    Debug.Log("node active");
                    framePointer.Toggle(false);
                    nodePointer.Toggle(true);
                    break;
                }
            case pointerModes.frame:
                {
                    Debug.Log("frame active");
                    framePointer.Toggle(true);
                    nodePointer.Toggle(false);
                    break;
                }
            default: { break; }
        }
    }

    public void setPointerModeToNode()
    {
        setPointerMode(pointerModes.node);
    }

    public void setPointerModeToFrame()
    {
        setPointerMode(pointerModes.frame);
    }
}
