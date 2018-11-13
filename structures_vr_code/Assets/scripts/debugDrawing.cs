using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugDrawing : MonoBehaviour {

	public GameObject constructorControllerPrefab;

	// Use this for initialization
	void Start () {

		Vector3 origin = new Vector3(0,0,0);
		Vector3 p1 = new Vector3(0,0,1);
		Vector3 p2 = new Vector3(2,2,2);
		Vector3 p3 = new Vector3(1,1,1);
		Vector3 p4 = new Vector3(1,0,1);
		Vector3 p5 = new Vector3(3,0,1);
		Vector3 p6 = new Vector3(0,2,1);
		
		/* TODO: rewrite this so it doesn't look horrible */

		constructorControllerPrefab.GetComponent<constructorController>().setPoint(origin, buildingObjects.Frame);
		constructorControllerPrefab.GetComponent<constructorController>().setPoint(p1, buildingObjects.Frame);
		
		constructorControllerPrefab.GetComponent<constructorController>().setPoint(p3, buildingObjects.Frame);
		constructorControllerPrefab.GetComponent<constructorController>().setPoint(p6, buildingObjects.Frame);

		constructorControllerPrefab.GetComponent<constructorController>().setPoint(origin, buildingObjects.Frame);
		constructorControllerPrefab.GetComponent<constructorController>().setPoint(p2, buildingObjects.Frame);

		
		



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
