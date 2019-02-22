using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameReference : MonoBehaviour
{
    private Frame myFrame;

    public void setMyFrame(Frame newFrame)
    {
        myFrame = newFrame;
    }

    public Frame getMyFrame()
    {
        return myFrame;
    }
}
