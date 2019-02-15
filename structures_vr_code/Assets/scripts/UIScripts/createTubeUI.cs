using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class createTubeUI : MonoBehaviour
{

    public TMP_InputField depth;
    public TMP_InputField width;
    public TMP_InputField flange;
    public TMP_InputField web;
    public TMP_InputField tubeName;
    public TMP_Dropdown materialDropdown;
    public sectionController mySectionController;

    public void createTube() {
        string materialName = (materialDropdown.options[materialDropdown.value].text);
        float depthVal = float.Parse(depth.text);
        float widthVal = float.Parse(width.text);
        float flangeVal = float.Parse(flange.text);
        float webVal = float.Parse(web.text);
        
        mySectionController.GetComponent<sectionController>().addTubeFrameSection(tubeName.text, materialName, depthVal, widthVal, flangeVal, webVal);
    }
}
