using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class holds a reference to a frame gameobjects frame class and selection controller */
/* This is needed for the the selection tool */
public class FrameBehavior : MonoBehaviour
{
    private selectionController mySelectionController;
    private PointerController myPointerController;

    private Frame myFrame;
    private bool canUse = true;

    /// <summary>
    /// Sets the frame reference
    /// </summary>
    public void setMyFrame(Frame newFrame)
    {
        myFrame = newFrame;
    }

    /// <summary>
    /// Returns the frame reference
    /// </summary>
    public Frame getMyFrame()
    {
        return myFrame;
    }

    /// <summary>
    /// Sets the selection controller
    /// </summary>
    public void setMySelectionController(selectionController newSelectionController)
    {
        mySelectionController = newSelectionController;
    }

    /// <summary>
    /// Returns the selection controller
    /// </summary>
    public selectionController getMySelectionController()
    {
        return mySelectionController;
    }

    /// <summary>
    /// Sets the pointer controller
    /// </summary>
    public void setMyPointerController(PointerController newPointerController)
    {
        myPointerController = newPointerController;
    }

    /// <summary>
    /// Returns the pointer controller
    /// </summary>
    public PointerController getMyPointerController()
    {
        return myPointerController;
    }

    /// <summary>
    /// Called when a frame is selected with the selection tool
    /// </summary>
    public void onVRTKUse()
    {
        if (canUse)
        {
            switch (myPointerController.getPointerMode()){
                case pointerModes.draw:
                case pointerModes.selectNode: {
                    break;
                }
                case pointerModes.selectFrame: {
                    mySelectionController.select(myFrame);
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
}
