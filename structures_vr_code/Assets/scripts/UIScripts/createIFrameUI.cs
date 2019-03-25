using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        float heightVal = myUnitsController.getLength(float.Parse(height.text));
        float tWidthVal = myUnitsController.getLength(float.Parse(topWidth.text));
        float tThickVal = myUnitsController.getLength(float.Parse(topThick.text));
        float webVal = myUnitsController.getLength(float.Parse(webThick.text));
        float bWidthVal = myUnitsController.getLength(float.Parse(botWidth.text));
        float bThickVal = myUnitsController.getLength(float.Parse(botThick.text));
        
        mySectionController.GetComponent<sectionController>().addIFrameSection(IFrameName.text, materialName, heightVal, tWidthVal, tThickVal, webVal, bWidthVal, bThickVal);
    
    }
}
