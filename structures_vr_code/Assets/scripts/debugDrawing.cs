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



        // Testing BuildingMaterial objects:
        /* 
        BuildingMaterial bm1 = new BuildingMaterial();
        BuildingMaterial bm2 = new BuildingMaterial("I_am_bm_2");
        BuildingMaterial bm3 = new BuildingMaterial("Three", BuildingMaterialAttributes.Regions.UNITEDSTATES,
            BuildingMaterialAttributes.Regions.UnitedStatesTypes.ALUMINUM,
            BuildingMaterialAttributes.Regions.UnitedStatesTypes.AluminumStandards.ASTM,
            BuildingMaterialAttributes.Regions.UnitedStatesTypes.AluminumStandards.ASTMGrades.GRADE_Alloy_5052_H34);

        Debug.Log("bm1: " + bm1.GetUserDefinedName() + ", " + bm1.GetRegion() + ", " + bm1.GetMaterialType() + ", " + bm1.GetStandard() + ", " + bm1.GetGrade());
        Debug.Log("bm2: " + bm2.GetUserDefinedName() + ", " + bm2.GetRegion() + ", " + bm2.GetMaterialType() + ", " + bm2.GetStandard() + ", " + bm2.GetGrade());
        Debug.Log("bm3: " + bm3.GetUserDefinedName() + ", " + bm3.GetRegion() + ", " + bm3.GetMaterialType() + ", " + bm3.GetStandard() + ", " + bm3.GetGrade());

        bm3.SetGrade(BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A36Grades.GRADE_36); // Should not work; merely reset grade to aluminum ASTM's first grade option
        Debug.Log("bm3 (1): " + bm3.GetUserDefinedName() + ", " + bm3.GetRegion() + ", " + bm3.GetMaterialType() + ", " + bm3.GetStandard() + ", " + bm3.GetGrade());

        bm3.SetStandard(BuildingMaterialAttributes.Regions.UnitedStatesTypes.RebarStandards.ASTM_A706); // Should not work; merely reset standard to aluminum's first standard option
        Debug.Log("bm3 (2): " + bm3.GetUserDefinedName() + ", " + bm3.GetRegion() + ", " + bm3.GetMaterialType() + ", " + bm3.GetStandard() + ", " + bm3.GetGrade());

        bm3.SetType(503); // Should not work; merely reset standard to United States' first type option
        Debug.Log("bm3 (3): " + bm3.GetUserDefinedName() + ", " + bm3.GetRegion() + ", " + bm3.GetMaterialType() + ", " + bm3.GetStandard() + ", " + bm3.GetGrade());

        bm3.SetRegion(111); // Should not work; no change should be made.
        Debug.Log("bm3 (4): " + bm3.GetUserDefinedName() + ", " + bm3.GetRegion() + ", " + bm3.GetMaterialType() + ", " + bm3.GetStandard() + ", " + bm3.GetGrade());
        */

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
