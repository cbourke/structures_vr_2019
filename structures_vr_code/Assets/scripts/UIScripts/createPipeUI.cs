﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class createPipeUI : MonoBehaviour
{

    public TMP_InputField diameter;
    public TMP_InputField thickness;
    public TMP_InputField tubeName;
    public TMP_Dropdown materialDropdown;
    public GameObject sectionController;

    public void createTube() {
        string materialName = (materialDropdown.options[materialDropdown.value].text);
        float diameterVal = float.Parse(diameter.text);
        float thickVal = float.Parse(thickness.text);
        
        sectionController.GetComponent<sectionController>().addPipeFrameSection(tubeName.text, materialName, diameterVal, thickVal);
    }
}
