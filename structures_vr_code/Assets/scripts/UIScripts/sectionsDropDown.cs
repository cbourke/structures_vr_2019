using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Attatched to toolsUI -> Define -> Frame Sections -> Dropdown */
/* Toggles the gameobjects containing the different define frame section panels */

public class sectionsDropDown : MonoBehaviour {

    TMP_Dropdown m_Dropdown;
    public GameObject iFrame;
    public GameObject pipe;
    public GameObject tube;

    void Start()
    {
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
        iFrame.SetActive(true);
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMP_Dropdown change)
    {
        if (change.value == 0)
        {
            iFrame.SetActive(true);
            pipe.SetActive(false);
            tube.SetActive(false);
        } else if (change.value == 1)
        {
            iFrame.SetActive(false);
            pipe.SetActive(true);
            tube.SetActive(false);
        } else
        {
            iFrame.SetActive(false);
            pipe.SetActive(false);
            tube.SetActive(true);
        }
    }
}

