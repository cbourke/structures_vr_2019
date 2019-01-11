using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum pointerModes
{
    drawFrameAtNode,
    selectNode,
    deleteFrameAtNode,
    drawFrameAtFrame,
    selectFrame,
    deleteFrame,
}

public class PointerController : MonoBehaviour {
    public Component framePointer;
    public Component gridNodePointer;
    public Component TeleportPointer;
    public Component UIPointer;
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
        switch(newMode) {
            case pointerModes.drawFrameAtNode:
            case pointerModes.selectNode:
            case pointerModes.deleteFrameAtNode:
                {
                    framePointer.GetComponent<VRTK.VRTK_Pointer>().Toggle(false);
                    gridNodePointer.GetComponent<VRTK.VRTK_Pointer>().Toggle(true);
                    break;
                }
            case pointerModes.drawFrameAtFrame:
            case pointerModes.selectFrame:
            case pointerModes.deleteFrame:
                {
                    framePointer.GetComponent<VRTK.VRTK_Pointer>().Toggle(true);
                    gridNodePointer.GetComponent<VRTK.VRTK_Pointer>().Toggle(false);
                    break;
                }
            default: { break; }
        }
    }
    
}
