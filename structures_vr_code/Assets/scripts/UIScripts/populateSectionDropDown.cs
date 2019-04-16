using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Attatched to toolsUI -> Draw -> Edit -> Section */
/* Used to populate the sections dropdown on the draw pane */
/* Whenever the UI draw pane is enabled it gets the list of sections from the section controller and updates the dropdown */
public class populateSectionDropDown : MonoBehaviour
{
    public sectionController mySectionController;
    private TMP_Dropdown dropdown;

	void OnEnable()
    {
        dropdown = gameObject.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        List<FrameSection> sections = mySectionController.getSectionList();
        List<string> sectionNames = new List<string>();
        foreach(FrameSection section in sections)
        {
            sectionNames.Add(section.GetName());
        }
        dropdown.AddOptions(sectionNames);

        dropdown.onValueChanged.AddListener(delegate {
            dropdownValueChanged();
        });
    }

    void dropdownValueChanged() {
        mySectionController.SetCurrentFrameSection(dropdown.options[dropdown.value].text);
    }
}
