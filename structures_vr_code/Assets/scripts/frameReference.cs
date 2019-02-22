using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameReference : MonoBehaviour
{
    private selectionController mySelectionController;
    private Frame myFrame;

    public void setMyFrame(Frame newFrame)
    {
        myFrame = newFrame;
    }

    public Frame getMyFrame()
    {
        return myFrame;
    }

    public void setMySelectionController(selectionController newSelectionController)
    {
        mySelectionController = newSelectionController;
    }

    public selectionController getMySelectionController()
    {
        return mySelectionController;
    }

    public void onVRTKUse()
    {
        mySelectionController.select(myFrame);
    }
}
