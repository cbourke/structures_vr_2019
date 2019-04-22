using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Attatched to toolsUI -> Define -> Frame Sections -> Tube -> Create */
/* Creates a new Tube  */
public class createTubeUI : MonoBehaviour
{
    public TMP_InputField depth;
    public TMP_InputField width;
    public TMP_InputField flange;
    public TMP_InputField web;
    public TMP_InputField tubeName;
    public TMP_Dropdown materialDropdown;
    public sectionController mySectionController;
    public unitsController myUnitsController;

    public void createTube() {
        string materialName = (materialDropdown.options[materialDropdown.value].text);
        float depthVal = myUnitsController.getLengthMeters(float.Parse(depth.text));
        float widthVal = myUnitsController.getLengthMeters(float.Parse(width.text));
        float flangeVal = myUnitsController.getLengthMeters(float.Parse(flange.text));
        float webVal = myUnitsController.getLengthMeters(float.Parse(web.text));
        
        mySectionController.GetComponent<sectionController>().addTubeFrameSection(tubeName.text, materialName, depthVal, widthVal, flangeVal, webVal);
    }
}
