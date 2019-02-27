using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public enum pointerModes
{
    node,
    frame
}

public class PointerController : MonoBehaviour {
    public VRTK_Pointer nodePointer;
    public VRTK_Pointer framePointer;
    public VRTK_UIPointer UIPointer;
    public VRTK_Pointer TeleportPointer;
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

    public void setPointerModeToNode()
    {
        setPointerMode(pointerModes.node);
    }

    public void setPointerModeToFrame()
    {
        setPointerMode(pointerModes.frame);
    }
}
