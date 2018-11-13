using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugDrawing : MonoBehaviour {

	public GameObject constructorController;
    public GameObject xmlController;

	// Use this for initialization
	void Start () {

		Vector3 origin = new Vector3(0,0,0);
		Vector3 p1 = new Vector3(1,0,0);
		Vector3 p2 = new Vector3(0,1,0);
		Vector3 p3 = new Vector3(1,1,0);
		Vector3 p4 = new Vector3(0,2,0);
		Vector3 p5 = new Vector3(1,2,0);
		Vector3 p6 = new Vector3(0,3,0);
		
		/* TODO: rewrite this so it doesn't look horrible */

		constructorController.GetComponent<constructorController>().setPoint(origin, buildingObjects.Frame);
		constructorController.GetComponent<constructorController>().setPoint(p1, buildingObjects.Frame);
		
		constructorController.GetComponent<constructorController>().setPoint(p2, buildingObjects.Frame);
		constructorController.GetComponent<constructorController>().setPoint(p3, buildingObjects.Frame);

        constructorController.GetComponent<constructorController>().setPoint(p4, buildingObjects.Frame);
        constructorController.GetComponent<constructorController>().setPoint(p5, buildingObjects.Frame);


        constructorController.GetComponent<constructorController>().createJointRestraint(origin, 'f');
        constructorController.GetComponent<constructorController>().createJointRestraint(p1, 'r');
        constructorController.GetComponent<constructorController>().createJointRestraint(p1, 'f'); // overwrite

        constructorController.GetComponent<constructorController>().createJointRestraint(p2, 'p');
        constructorController.GetComponent<constructorController>().createJointRestraint(p3, 'p');

        constructorController.GetComponent<constructorController>().createJointRestraint(p4, 'r');
        constructorController.GetComponent<constructorController>().createJointRestraint(p5, 'r');
        constructorController.GetComponent<constructorController>().deleteJointRestraint(p5); // delete

        constructorController.GetComponent<constructorController>().createJointRestraint(p6, 'f'); // This is meant to fail, as there is no frame endpoint here

        xmlController.GetComponent<xmlController>().sendFileToSap(); //send to SAP2000



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
