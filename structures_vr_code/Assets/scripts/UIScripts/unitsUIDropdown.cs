using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class unitsUIDropdown : MonoBehaviour
{
    public unitsController unitCtr;
    private TMP_Dropdown unitDropdown;

    void Start()
    {
        unitDropdown = GetComponent<TMP_Dropdown>();
        unitDropdown.ClearOptions();
        unitDropdown.AddOptions(unitCtr.getUnits());
        unitDropdown.value = 9;
        DropdownValueChanged(unitDropdown);
        unitDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(unitDropdown);
        });
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        unitCtr.setUnits(dropdown.options[dropdown.value].text);
    }
}
