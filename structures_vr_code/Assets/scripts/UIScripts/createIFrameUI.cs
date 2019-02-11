﻿using System.Collections;
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
    public GameObject sectionController;

    public void createIFrame() {
        string materialName = (materialDropdown.options[materialDropdown.value].text);
        float heightVal = float.Parse(height.text);
        float tWidthVal = float.Parse(topWidth.text);
        float tThickVal = float.Parse(topThick.text);
        float webVal = float.Parse(webThick.text);
        float bWidthVal = float.Parse(botWidth.text);
        float bThickVal = float.Parse(botThick.text);
        
        sectionController.GetComponent<sectionController>().addIFrameSection(IFrameName.text, materialName, heightVal, tWidthVal, tThickVal, webVal, bWidthVal, bThickVal);
    }
}
