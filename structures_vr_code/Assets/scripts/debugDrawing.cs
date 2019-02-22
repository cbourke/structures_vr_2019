using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugDrawing : MonoBehaviour {

	public constructorController myConstructorController;
    public xmlController myXmlController;
    public materialsController myMaterialController;
    public sectionController mySectionController;
    public unitsController myUnitsController;
    public gridController myGridController;

    Vector3 origin = new Vector3(0,0,0);
    Vector3 p1 = new Vector3(1,0,0);
    Vector3 p2 = new Vector3(0,1,0);
    Vector3 p3 = new Vector3(2,1,0);
    Vector3 p4 = new Vector3(0,2,0);
    Vector3 p5 = new Vector3(3,2,0);
    Vector3 p6 = new Vector3(0,3,0);

	
	void Start () {
        //xmlController.GetComponent<xmlController>().sendFileToSap(); //send to SAP2000
        Debug.Log("START DEBUG");
        populateDropdowns();
        drawFrames();
    }
	
    void populateDropdowns()
    {
        			
        //myMaterialController.addBuildingMaterial("Steel01", "United States", "steel", "ASTM A36", "Grade 36");
        
        //mySectionController.addIFrameSection("Sec_Steel_I", "Steel01", 0.3f, 0.12f, 0.01f, 0.007f, 0.12f, 0.01f);
        mySectionController.addPipeFrameSection("Sec_Steel_Pipe", "Steel01", 0.2f, 0.01f);
        mySectionController.addTubeFrameSection("Sec_Aluminum_Tube", "Steel01", 0.16f, 0.1f, 0.007f, 0.007f);

    }

    void testConversions()
    {
        unitsController uC = myUnitsController.GetComponent<unitsController>();
        uC.populateDict();
        int lengthMeter = 5;
        int forceNewton = 15;
        int tempCel = 20;
        uC.setUnits("N, m, C");
        Debug.Log("Force: " + uC.getForceUnit() + ", Length: " + uC.getLengthUnit() + ", Temp: " + uC.getTempUnit());
        Debug.Log(tempCel + " Celcius is " + uC.getTemperature(tempCel) + " Celcius"); // correct
        uC.setUnits("Kip, ft, F");
        Debug.Log("Force: " + uC.getForceUnit() + ", Length: " + uC.getLengthUnit() + ", Temp: " + uC.getTempUnit());
        
        Debug.Log(lengthMeter + " meters is " + uC.getLength(lengthMeter) + " feet"); // correct
        Debug.Log(forceNewton + " newtons is " + uC.getForce(forceNewton) + " Kips"); // correct
        Debug.Log(tempCel + " Celcius is " + uC.getTemperature(tempCel) + " farenheit"); // correct
        
        int lengthFeet = 8;
        int forceKips = 120;
        int tempFaren = 75;
        Debug.Log(lengthFeet + " feet is " + uC.getLengthMeters(lengthFeet) + " meters"); // correct
        Debug.Log(forceKips + "  Kips is " + uC.getForceNewtons(forceKips) + " newtons"); // correct +-2 N
        Debug.Log(tempFaren + "  farenheit is " + uC.getTemperatureCelcius(tempFaren) + " Celcius"); // correct

        int lengthInches = 346;
        int forceLb = 3;
        
        uC.setUnits("lb, in, C");
        Debug.Log(lengthInches + " inches is " + uC.getLengthMeters(lengthInches) + "meters");
        Debug.Log(forceLb + " lb is " + uC.getForceNewtons(forceLb) + " Newtons");
        Debug.Log(lengthMeter + " meters is " + uC.getLength(lengthMeter) + " inches");
        Debug.Log(forceNewton + " newtons is " + uC.getForce(forceNewton) + " lb");
        
        int lenghtCM = 400;
        int forceKN = 2;
        uC.setUnits("KN, cm, C");

        Debug.Log(lenghtCM + " centimeters is " + uC.getLengthMeters(lenghtCM) + "meters ");
        Debug.Log(lengthMeter + " meters is " + uC.getLength(lengthMeter) + "cm ");
        Debug.Log(forceKN + " kn is " + uC.getForceNewtons(forceKN) + " newtons");
        Debug.Log(forceNewton + " newtons is " + uC.getForce(forceNewton) + " KN");

        int lengthMM = 3600;
        int forceKgf = 3;
        uC.setUnits("Kgf, mm, C");

        Debug.Log(lengthMM + " mm is " + uC.getLengthMeters(lengthMM)+ " meters");
        Debug.Log(lengthMeter + " meters is " + uC.getLength(lengthMeter)+ " mm");
        Debug.Log(forceKgf + " kgf is "+ uC.getForceNewtons(forceKgf) + " newtons");
        Debug.Log(forceNewton + " newtons is " + uC.getForce(forceNewton) + " Kgf");
        
        int forceTonf = 2;
        uC.setForceUnit("Tonf");
        Debug.Log(forceTonf + " tonf is " + uC.getForceNewtons(forceTonf)+ "newtons");
        Debug.Log(forceNewton + " newtons is " + uC.getForce(forceNewton)+ " tonf");
    }

    void testSections()
    {
        mySectionController.addIFrameSection("Sec_Steel_I", "Steel01", 0.3f, 0.12f, 0.01f, 0.007f, 0.12f, 0.01f);
        mySectionController.addPipeFrameSection("Sec_Steel_Pipe", "Steel02", 0.2f, 0.01f);
        mySectionController.addTubeFrameSection("Sec_Aluminum_Tube", "Aluminum01", 0.16f, 0.1f, 0.007f, 0.007f);

        mySectionController.SetCurrentFrameSection("Sec_Steel_I");
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(origin, buildingObjects.Frame);
		myConstructorController.setPoint(p1, buildingObjects.Frame);

        mySectionController.SetCurrentFrameSection("Sec_Steel_Pipe");
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(p2, buildingObjects.Frame);
		myConstructorController.setPoint(p3, buildingObjects.Frame);

        mySectionController.SetCurrentFrameSection("Sec_Aluminum_Tube");
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(p4, buildingObjects.Frame);
        myConstructorController.setPoint(p5, buildingObjects.Frame);
    }

    void testJoints()
    {
        myConstructorController.createJointRestraint(origin, 'f');
        myConstructorController.createJointRestraint(p1, 'r');
        myConstructorController.createJointRestraint(p1, 'f'); // overwrite

        myConstructorController.createJointRestraint(p2, 'p');
        myConstructorController.createJointRestraint(p3, 'p');

        myConstructorController.createJointRestraint(p4, 'r');
        myConstructorController.createJointRestraint(p5, 'r');
        myConstructorController.deleteJointRestraint(p5); // delete

        myConstructorController.createJointRestraint(p6, 'f'); // This is meant to fail, as there is no frame endpoint here
    }

	void testMaterials()
    {
        
        // this is broken because the way building materials is being handled has changed
        /*
        myMaterialController.addBuildingMaterial("Steel01", BuildingMaterialAttributes.Regions.UNITEDSTATES, BuildingMaterialAttributes.Regions.UnitedStatesTypes.STEEL, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A53, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A53Grades.GRADE_B);
        myMaterialController.addBuildingMaterial("Steel02", BuildingMaterialAttributes.Regions.UNITEDSTATES, BuildingMaterialAttributes.Regions.UnitedStatesTypes.STEEL, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A500, BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A500Grades.GRADE_B_fy_46);
        myMaterialController.addBuildingMaterial("Aluminum01", BuildingMaterialAttributes.Regions.UNITEDSTATES, BuildingMaterialAttributes.Regions.UnitedStatesTypes.ALUMINUM, BuildingMaterialAttributes.Regions.UnitedStatesTypes.AluminumStandards.ASTM, BuildingMaterialAttributes.Regions.UnitedStatesTypes.AluminumStandards.ASTMGrades.GRADE_Alloy_6063_T6);
        
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

    void drawFrames()
    {
        mySectionController.addTubeFrameSection("Sec_Aluminum_Tube", "Steel01", 0.16f, 0.1f, 0.007f, 0.007f);
        mySectionController.SetCurrentFrameSection("Sec_Aluminum_Tube");

        myGridController.createGrid(5, 5, 5, 1f);
        myConstructorController.setPoint(p2, buildingObjects.Frame);
        myConstructorController.setPoint(p3, buildingObjects.Frame);

        //mySectionController.SetCurrentFrameSection("Sec_Aluminum_Tube");
        myConstructorController.setPoint(p4, buildingObjects.Frame);
        myConstructorController.setPoint(p5, buildingObjects.Frame);

        //mySectionController.SetCurrentFrameSection("Sec_Steel_Pipe");
        myConstructorController.setPoint(origin, buildingObjects.Frame);
        myConstructorController.setPoint(p1, buildingObjects.Frame);

    }
}
