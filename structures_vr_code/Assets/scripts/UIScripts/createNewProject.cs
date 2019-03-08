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
	
	public gridController myGridController;
	public constructorController myConstructorController;
	public unitsController myUnitsController;

    public Object jointRestraint;

	void Start() {
		if(myGridController == null || myConstructorController == null) {
			Debug.LogError("Assign the controllers to the file-create button");
		}
	}

	public void clickNew() {
		Debug.Log("Create");
		myConstructorController.deleteAll();
        float spacing = float.Parse(spacingText.text);
		float spacingMeters = (float)(myUnitsController.getLengthMeters(spacing));
		int x = int.Parse(xText.text);
		int y = int.Parse(yText.text);
		int z = int.Parse(zText.text);
		myGridController.createGrid(x,y,z,spacingMeters);
	}
}
