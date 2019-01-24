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
	
	//Create a List of new Dropdown options
	List<string> m_DropOptions = new List<string> { "Option 1", "Option 2"};
	//This is the Dropdown

	void Start()
	{
		
		// region event
        region.onValueChanged.AddListener(delegate {
            regionDropdownValueChanged(type);
        });
		// type event
        type.onValueChanged.AddListener(delegate {
            typeDropdownValueChanged(standard);
        });
		// standard event
        standard.onValueChanged.AddListener(delegate {
            standardDropdownValueChanged(grade);
        });
		
		Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> materialDict = materialController.GetComponent<materialDefinitions>().getDict();
		List<string> regionList = new List<string>(materialDict.Keys);
		string regionString = null;
		foreach(string str in regionList)
		{
			regionString = str;
			Debug.Log(str);
		}
		List<string> typeList = new List<string>(materialDict[regionString].Keys);
		foreach(string str in typeList)
		{
			//regionString = str;
			Debug.Log(str);
		}

		//region.ClearOptions();
		//List<string> keyList = new List<string>(BuildingMaterialAttributes1.materialCollection.Keys);
		//region.AddOptions(convertToList(BuildingMaterialAttributes.Regions.members));
		//region.AddOptions(convertToList(BuildingMaterialAttributes.Regions.members));

		// this sets the value to US. it probably shouldn't be hardcoded like this, but oh well
		region.value = 7;



        
	}
	
	void Update()
	{
		
	}

    void regionDropdownValueChanged(TMP_Dropdown dropdown)
    {
		// update values of type dropdown
		string regionType;
		regionType = region.options[region.value].text;
		regionType = Regex.Replace(regionType, @"\s+", "");
		regionType += "Types";
		
		/*
		string name = "BuildingMaterialAttributes.Regions." + regionType + ".getMembers";
		obj = (name)Activator.CreateInstance("MyAssembly", ClassName))
		
		object list = Activator.CreateInstance(newType);
		string[] listString = (string[])list;
		convertToList(listString);
		Debug.Log(listString.ToString());
		 */
		//Debug.Log(typeof(BuildingMaterialAttributes.Regions).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BuildingMaterialAttributes.Regions))));
        dropdown.value = 2;
    }

    void typeDropdownValueChanged(TMP_Dropdown dropdown)
    {
		// update values of standard dropdown
        dropdown.value = 2;
    }

    void standardDropdownValueChanged(TMP_Dropdown dropdown)
    {
		// update values of grade dropdown
        dropdown.value = 2;
    }

	List<string> convertToList(string[] members) {
		List<string> memberList = new List<string>();
		for (int i = 0; i<members.Length; i++){
			memberList.Add(members[i]);
		}
		return memberList;
	}

}
