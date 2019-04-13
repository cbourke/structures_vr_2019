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
                // TODO
            }
            



    }
}
