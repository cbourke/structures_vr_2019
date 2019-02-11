using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class createNewProject : MonoBehaviour {

	public TMP_InputField xText;
	public TMP_InputField yText;
	public TMP_InputField zText;
	public TMP_InputField spacingText;
	
	public GameObject gridController;
	public GameObject constructorController;
	public unitsController units;

	void Start() {
		if(gridController == null || constructorController == null) {
			Debug.LogError("Assign the controllers to the file-create button");
		}
	}

	public void clickNew() {
		Debug.Log("Create");
		constructorController.GetComponent<constructorController>().deleteAll();
 		float spacing = float.Parse(spacingText.text);
		float spacingMeters = (float)(units.getLengthMeters(spacing));
		int x = int.Parse(xText.text);
		int y = int.Parse(yText.text);
		int z = int.Parse(zText.text);
		gridController.GetComponent<generateGrid>().createGrid(x,y,z,spacingMeters);
	}
}
