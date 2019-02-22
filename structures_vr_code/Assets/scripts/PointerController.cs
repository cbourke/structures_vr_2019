using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum pointerModes
{
    draw,
    select
}

public class PointerController : MonoBehaviour {
    public VRTK.VRTK_Pointer drawPointer;
    public VRTK.VRTK_UIPointer UIPointer;
    public VRTK.VRTK_Pointer TeleportPointer;
    public VRTK.VRTK_Pointer selectPointer;
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
                    selectPointer.Toggle(false);
                    drawPointer.Toggle(true);
                    break;
                }
            case pointerModes.select:
                {
                    drawPointer.Toggle(false);
                    selectPointer.Toggle(true);
                    break;
                }
            default: { break; }
        }
    }

    public void setPointerModeToDraw()
    {
        setPointerMode(pointerModes.draw);
    }

    public void setPointerModeToSelect()
    {
        setPointerMode(pointerModes.select);
    }
}
