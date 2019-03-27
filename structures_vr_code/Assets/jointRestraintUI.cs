using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class jointRestraintUI : MonoBehaviour
{
    public TMP_Dropdown typeDropdown;
    public selectionController mySelectionController;
    public constructorController myConstructorController;

    private char type;

    // Start is called before the first frame update
    void Start()
    {
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
        // get selection
        // create list of points
        // call constructorcontroller
        
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
}
