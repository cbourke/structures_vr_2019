using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class materialsUIdropdown : MonoBehaviour {

	public GameObject materialController;
	public TMP_Dropdown region;
	public TMP_Dropdown type;
	public TMP_Dropdown standard;
	public TMP_Dropdown grade;
	public TMP_InputField matName;
	
	Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> materialDict;

	void Start()
	{
		materialDict = materialController.GetComponent<materialDefinitions>().getDict();

		// region event
        region.onValueChanged.AddListener(delegate {
            regionDropdownValueChanged();
        });
		// type event
        type.onValueChanged.AddListener(delegate {
            typeDropdownValueChanged();
        });
		// standard event
        standard.onValueChanged.AddListener(delegate {
            standardDropdownValueChanged();
        });
		
		List<string> regionList = new List<string>(materialDict.Keys);
		string regionString = null;
		
		setDropdownValues(region, regionList);
		regionDropdownValueChanged();
		typeDropdownValueChanged();
		standardDropdownValueChanged();
	}
	

    void regionDropdownValueChanged()
    {
		// update values of type dropdown
		string regionType;
		regionType = region.options[region.value].text;
		
		List<string> typeList = new List<string>(materialDict[regionType].Keys);
		setDropdownValues(type, typeList);
    }

    void typeDropdownValueChanged()
    {
		// update values of standard dropdown
		string regionType;
		string typeType;
		regionType = region.options[region.value].text;
		typeType = type.options[type.value].text;

		List<string> standardList = new List<string>(materialDict[regionType][typeType].Keys);
		setDropdownValues(standard, standardList);
    }

    void standardDropdownValueChanged()
    {
		// update values of grade dropdown
        string regionType;
		string typeType;
		string standardType;
		regionType = region.options[region.value].text;
		typeType = type.options[type.value].text;
		standardType = standard.options[standard.value].text;

		List<string> gradeList = new List<string>(materialDict[regionType][typeType][standardType]);
		setDropdownValues(grade, gradeList);
    }

	void setDropdownValues(TMP_Dropdown dropdown, List<string> list)
	{
		dropdown.ClearOptions();
		dropdown.AddOptions(list);
		dropdown.value = 0;
	}

	public void createMaterialClick() {
		string regionType;
		string typeType;
		string standardType;
		string gradeType;
		regionType = region.options[region.value].text;
		typeType = type.options[type.value].text;
		standardType = standard.options[standard.value].text;
		gradeType = grade.options[grade.value].text;
		if(matName.text != null) {
        	materialController.GetComponent<materialsController>().addBuildingMaterial(matName.text, regionType, typeType, standardType, gradeType);
			Debug.Log("material " + matName.text + " created");
		} else
		{
			Debug.LogError("Input a name for the material!");
		}
    }
}
