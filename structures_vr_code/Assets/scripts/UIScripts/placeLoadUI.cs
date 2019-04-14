using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Scripts for the Delete UI button */
/* Contains the logic for deleting a selection of frames */
public class placeLoadUI : MonoBehaviour
{
    public constructorController myConstructorController;
    public selectionController mySelectionController;
    public SapTranslatorIpcHandler mySapTranslatorIpcHandler;



    void Start()
    {

    }

    public void onClick() {

            // user has confirmed they want to delete
            List<Frame> selection = new List<Frame>();
            selection = mySelectionController.GetSelectedFrames();
            foreach (Frame f in selection)
            {
            string command = "VRE to SAPTranslator: frameObjSetLoadPoint(" + f.getName() + ", DEAD, 1, 10, 0.5, 1000, Global, true, true, 0)";
                mySapTranslatorIpcHandler.enqueueToOutputBuffer(command);
            }
            



    }
}
