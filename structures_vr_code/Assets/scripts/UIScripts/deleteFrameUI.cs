using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class deleteFrameUI : MonoBehaviour
{
    public constructorController myConstructorController;
    public selectionController mySelectionController;
    public TMP_Text buttonText;

    private bool isWait = false;
    private string deleteText;
    private string confirmText;

    void Start()
    {
        deleteText = buttonText.text;
        confirmText = "Sure?";
    }

    public void onClick() {
        if(isWait) {
            // user has confirmed they want to delete
            List<Frame> selection = new List<Frame>();
            selection = mySelectionController.GetSelectedFrames();
            foreach (Frame f in selection)
            {
                myConstructorController.deleteFrame(f.getName());
            }
            mySelectionController.deselect(selection);
            StopCoroutine(deleteCheck());
            buttonText.text = deleteText;
            isWait = false;
        } else {
            // delete button has not been pressed yet
            buttonText.text = confirmText;
            StartCoroutine(deleteCheck());
        }
    }

    IEnumerator deleteCheck()
    {
        isWait = true;
        yield return new WaitForSeconds(5);
        isWait = false;
        buttonText.text = deleteText;
        
    }
}
