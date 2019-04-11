using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class jointRestraintUI : MonoBehaviour
{
    public TMP_Dropdown typeDropdown;
    public selectionController mySelectionController;
    public constructorController myConstructorController;
    public TMP_Text deleteButtonText;

    private char type;
    private bool isWait = false;
    private string deleteText;
    private string confirmText;

    // Start is called before the first frame update
    void Start()
    {
        deleteText = deleteButtonText.text;
        confirmText = "Sure?";
    }

    
    public void createJoints()
    {
        getRestraintType();
        List<GridNode> nodes = mySelectionController.GetSelectedNodes();
        foreach (GridNode n in nodes)
        {
            Debug.Log("pos: " + n.getLocation());
            myConstructorController.createJointRestraint(n.getLocation(), type);
        }
    }
    
    public void deleteJoints()
    {
        if(isWait) {
            // user has confirmed they want to delete
            List<GridNode> nodes = mySelectionController.GetSelectedNodes();
            foreach (GridNode n in nodes)
            {
                myConstructorController.deleteJointRestraint(n.getLocation());
            }
            StopCoroutine(deleteCheck());
            deleteButtonText.text = deleteText;
            isWait = false;
        } else {
            // delete button has not been pressed yet
            deleteButtonText.text = confirmText;
            StartCoroutine(deleteCheck());
        }
    }

    public void deselectAll() {
        mySelectionController.clearNodeSelection();
    }

    void getRestraintType() {
        string dropDownValue = typeDropdown.options[typeDropdown.value].text;
        Debug.Log("dropdownvalue: " + dropDownValue);
        if(dropDownValue == "Fixed") {
            type = 'f';
        } else if(dropDownValue == "Pin") {
            type = 'p';
        } else if(dropDownValue == "Roller") {
            type = 'r';
        } else {
            Debug.LogError("Invalid restraint type");
        }
    }


    IEnumerator deleteCheck()
    {
        isWait = true;
        yield return new WaitForSeconds(5);
        isWait = false;
        deleteButtonText.text = deleteText;
        
    }

}
