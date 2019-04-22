using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Attatched to toolsUI -> Define -> Frame Sections -> I-Frame -> DropDown */
/* Attatched to toolsUI -> Define -> Frame Sections -> Pipe -> DropDown */
/* Attatched to toolsUI -> Define -> Frame Sections -> Tube -> DropDown */
/* There may be other places this script exists */
/* This script gets a list of defined building materials from the materialsController
   and populates the dropdown that is passed in with the values */
public class materialListDropdown : MonoBehaviour {

	public materialsController myMaterialController;
	public TMP_Dropdown dropdown;

	// Use this for initialization
	void Start () {
	}

	
	void OnEnable()
	{
		List<BuildingMaterial> materialList = myMaterialController.GetComponent<materialsController>().GetMaterials();
		dropdown.ClearOptions();
		List<string> materialNames = new List<string>();
		
		foreach(BuildingMaterial mat in materialList)
		{
			string name = mat.GetName();
			Debug.Log("name = " + name);
			materialNames.Add(name);
		}
		foreach(string str in materialNames)
		{
			Debug.Log("names == " + str);
		}
		dropdown.AddOptions(materialNames);
		//dropdown.value = 0;
	}
	

}
