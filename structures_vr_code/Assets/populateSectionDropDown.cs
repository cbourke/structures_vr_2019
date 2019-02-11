using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class populateSectionDropDown : MonoBehaviour
{
    public sectionController mySectionController;
    private TMP_Dropdown dropdown;

	void OnEnable()
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();
        List<FrameSection> sections = mySectionController.getSectionList();
        List<string> sectionNames = new List<string>();
        foreach(FrameSection section in sections)
        {
            sectionNames.Add(section.GetName());
        }
        dropdown.AddOptions(sectionNames);

    }
}
