using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugDrawing : MonoBehaviour {

	public GameObject constructorController;
    public GameObject xmlController;
    public GameObject materialController;
    public GameObject sectionController;
    public GameObject unitsController;

    Vector3 origin = new Vector3(0,0,0);
    Vector3 p1 = new Vector3(1,0,0);
    Vector3 p2 = new Vector3(0,1,0);
    Vector3 p3 = new Vector3(1,1,0);
    Vector3 p4 = new Vector3(0,2,0);
    Vector3 p5 = new Vector3(1,2,0);
    Vector3 p6 = new Vector3(0,3,0);

	
	void Start () {
        
        //xmlController.GetComponent<xmlController>().sendFileToSap(); //send to SAP2000
        Debug.Log("START DEBUG");
        testConversions();
    }
	
    void testConversions()
    {
        unitsController uC = unitsController.GetComponent<unitsController>();
        int lengthMeter = 5;
        int forceNewton = 15;
        int tempCel = 20;
        uC.setUnits("N, m, C");
        Debug.Log("Force: " + uC.getForceUnit() + ", Length: " + uC.getLengthUnit() + ", Temp: " + uC.getTempUnit());
        Debug.Log(tempCel + " Celcius is " + uC.getTemperature(tempCel) + " Celcius");
        uC.setUnits("Kip, ft, F");
        Debug.Log("Force: " + uC.getForceUnit() + ", Length: " + uC.getLengthUnit() + ", Temp: " + uC.getTempUnit());
        
        Debug.Log(lengthMeter + " meters is " + uC.getLength(lengthMeter) + " feet");
        Debug.Log(forceNewton + " newtons is " + uC.getForce(forceNewton) + " Kips");
        Debug.Log(tempCel + " Celcius is " + uC.getTemperature(tempCel) + " farenheit");
        
        int lengthFeet = 8;
        int forceKips = 120;
        int tempFaren = 75;
        Debug.Log(lengthFeet + " feet is " + uC.getLengthMeters(lengthFeet) + " meters");
        Debug.Log(forceKips + "  Kips is " + uC.getForceNewtons(forceKips) + " newtons");
        Debug.Log(tempFaren + "  farenheit is " + uC.getTemperatureCelcius(tempFaren) + " Celcius");
        
        
    }

    void testSections()
    {
        sectionController.GetComponent<sectionController>().addIFrameSection("Sec_Steel_I", "Steel01", 0.3, 0.12, 0.01, 0.007, 0.12, 0.01);
        sectionController.GetComponent<sectionController>().addPipeFrameSection("Sec_Steel_Pipe", "Steel02", 0.2, 0.01);
        sectionController.GetComponent<sectionController>().addTubeFrameSection("Sec_Aluminum_Tube", "Aluminum01", 0.16, 0.1, 0.007, 0.007);

        sectionController.GetComponent<sectionController>().SetCurrentFrameSection("Sec_Steel_I");
        Debug.Log("Current frame section: " + sectionController.GetComponent<sectionController>().GetCurrentFrameSection().GetName());
        constructorController.GetComponent<constructorController>().setPoint(origin, buildingObjects.Frame);
		constructorController.GetComponent<constructorController>().setPoint(p1, buildingObjects.Frame);

        sectionController.GetComponent<sectionController>().SetCurrentFrameSection("Sec_Steel_Pipe");
        Debug.Log("Current frame section: " + sectionController.GetComponent<sectionController>().GetCurrentFrameSection().GetName());
        constructorController.GetComponent<constructorController>().setPoint(p2, buildingObjects.Frame);
		constructorController.GetComponent<constructorController>().setPoint(p3, buildingObjects.Frame);

        sectionController.GetComponent<sectionController>().SetCurrentFrameSection("Sec_Aluminum_Tube");
        Debug.Log("Current frame section: " + sectionController.GetComponent<sectionController>().GetCurrentFrameSection().GetName());
        constructorController.GetComponent<constructorController>().setPoint(p4, buildingObjects.Frame);
        constructorController.GetComponent<constructorController>().setPoint(p5, buildingObjects.Frame);
    }

    void testJoints()
    {
        constructorController.GetComponent<constructorController>().createJointRestraint(origin, 'f');
        constructorController.GetComponent<constructorController>().createJointRestraint(p1, 'r');
        constructorController.GetComponent<constructorController>().createJointRestraint(p1, 'f'); // overwrite

        constructorController.GetComponent<constructorController>().createJointRestraint(p2, 'p');
        constructorController.GetComponent<constructorController>().createJointRestraint(p3, 'p');

        constructorController.GetComponent<constructorController>().createJointRestraint(p4, 'r');
        constructorController.GetComponent<constructorController>().createJointRestraint(p5, 'r');
        constructorController.GetComponent<constructorController>().deleteJointRestraint(p5); // delete

        constructorController.GetComponent<constructorController>().createJointRestraint(p6, 'f'); // This is meant to fail, as there is no frame endpoint here
    }

	void testMaterials()
    {
        
        // this is broken because the way building materials is being handled has changed
        /*
        materialController.GetComponent<materialsController>().addBuildingMaterial("Steel01", BuildingMaterialAttributes.Regions.UNITEDSTATES, BuildingMaterialAttributes.Regions.UnitedStatesTypes.STEEL, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A53, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A53Grades.GRADE_B);
        materialController.GetComponent<materialsController>().addBuildingMaterial("Steel02", BuildingMaterialAttributes.Regions.UNITEDSTATES, BuildingMaterialAttributes.Regions.UnitedStatesTypes.STEEL, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A500, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A500Grades.GRADE_B_fy_46);
        materialController.GetComponent<materialsController>().addBuildingMaterial("Aluminum01", BuildingMaterialAttributes.Regions.UNITEDSTATES, BuildingMaterialAttributes.Regions.UnitedStatesTypes.ALUMINUM, BuildingMaterialAttributes.Regions.UnitedStatesTypes.AluminumStandards.ASTM, BuildingMaterialAttributes.Regions.UnitedStatesTypes.AluminumStandards.ASTMGrades.GRADE_Alloy_6063_T6);
        
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
}
