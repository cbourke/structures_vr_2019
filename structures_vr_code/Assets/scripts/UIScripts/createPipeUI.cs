using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Attatched to toolsUI -> Define -> Frame Sections -> Pipe -> Create */
/* Creates a new Pipe  */
public class createPipeUI : MonoBehaviour
{
    public TMP_InputField diameter;
    public TMP_InputField thickness;
    public TMP_InputField tubeName;
    public TMP_Dropdown materialDropdown;
    public sectionController mySectionController;
    public unitsController myUnitsController;

    public void createTube() {
        string materialName = (materialDropdown.options[materialDropdown.value].text);
        float diameterVal = myUnitsController.getLengthMeters(float.Parse(diameter.text));
        float thickVal = myUnitsController.getLengthMeters(float.Parse(thickness.text));
        
        mySectionController.GetComponent<sectionController>().addPipeFrameSection(tubeName.text, materialName, diameterVal, thickVal);
    }
}
