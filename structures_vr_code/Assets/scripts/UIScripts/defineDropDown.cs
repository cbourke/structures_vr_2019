using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class defineDropDown : MonoBehaviour {

    TMP_Dropdown m_Dropdown;
    public GameObject material;
    public GameObject section;
    public GameObject group;

    void Start()
    {
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
        material.SetActive(true);
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMP_Dropdown change)
    {
        if (change.value == 0)
        {
            material.SetActive(true);
            section.SetActive(false);
            group.SetActive(false);
        } else if (change.value == 1)
        {
            material.SetActive(false);
            section.SetActive(true);
            group.SetActive(false);
        } else
        {
            material.SetActive(false);
            section.SetActive(false);
            group.SetActive(true);
        }
    }
}

