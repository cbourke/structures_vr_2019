using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Attatched to toolsUI -> Define -> Frame Sections -> I-Frame -> Create */
/* Creates a new I Frame section */
public class createIFrameUI : MonoBehaviour
{
    public TMP_InputField height;
    public TMP_InputField topWidth;
    public TMP_InputField topThick;
    public TMP_InputField webThick;
    public TMP_InputField botWidth;
    public TMP_InputField botThick;
    public TMP_InputField IFrameName;
    public TMP_Dropdown materialDropdown;
    public sectionController mySectionController;
    public unitsController myUnitsController;

    public void createIFrame() {
        string materialName = (materialDropdown.options[materialDropdown.value].text);
        float heightVal = myUnitsController.getLengthMeters(float.Parse(height.text));
        float tWidthVal = myUnitsController.getLengthMeters(float.Parse(topWidth.text));
        float tThickVal = myUnitsController.getLengthMeters(float.Parse(topThick.text));
        float webVal = myUnitsController.getLengthMeters(float.Parse(webThick.text));
        float bWidthVal = myUnitsController.getLengthMeters(float.Parse(botWidth.text));
        float bThickVal = myUnitsController.getLengthMeters(float.Parse(botThick.text));
        
        mySectionController.GetComponent<sectionController>().addIFrameSection(IFrameName.text, materialName, heightVal, tWidthVal, tThickVal, webVal, bWidthVal, bThickVal);
    
    }
}
