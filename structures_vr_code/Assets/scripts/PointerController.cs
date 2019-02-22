using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum pointerModes
{
    draw,
    select
}

public class PointerController : MonoBehaviour {
    public Component framePointer;
    public Component gridNodePointer;
    public Component TeleportPointer;
    public Component UIPointer;
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
        mode = newMode;
        switch(newMode) {
            case pointerModes.draw:
            {

                break;
            }
            case pointerModes.select:
            {

                break;
            }
            default: { break; }
        }
    }

    public void toggleFramesOrNodes(){
        framePointer.GetComponent<VRTK.VRTK_Pointer>().Toggle(!framePointer.GetComponent<VRTK.VRTK_Pointer>().isActiveAndEnabled);
        gridNodePointer.GetComponent<VRTK.VRTK_Pointer>().Toggle(!gridNodePointer.GetComponent<VRTK.VRTK_Pointer>().isActiveAndEnabled);
    }
    
}
