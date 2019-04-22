using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This file is used to test various functions */
/* Its a good idea to test things here so it is a more controlled enviroment than doing it in VR */
/* NOTE that this only runs if the debug gameobject is enabled in the Unity editor */
public class debugDrawing : MonoBehaviour {

	public constructorController myConstructorController;
    public xmlController myXmlController;
    public materialsController myMaterialController;
    public sectionController mySectionController;
    public unitsController myUnitsController;
    public gridController myGridController;
    public SapTranslatorIpcHandler mySapTranslatorIpcHandler;
    public analysisController myAnalysisController;

    Vector3 origin = new Vector3(0,0,0);
    Vector3 p1 = new Vector3(1,0,0);
    Vector3 p2 = new Vector3(0,1,0);
    Vector3 p3 = new Vector3(2,1,0);
    Vector3 p4 = new Vector3(0,2,0);
    Vector3 p5 = new Vector3(3,2,0);
    Vector3 p6 = new Vector3(0,3,0);

    Vector3 p7 = new Vector3(0, 1, 1);
    Vector3 p8 = new Vector3(2, 1, 1);
    Vector3 p9 = new Vector3(0, 2, 1);
    Vector3 p10 = new Vector3(2, 2, 1);
    string frameName;
    void Start () {
        //xmlController.GetComponent<xmlController>().sendFileToSap(); //send to SAP2000
        

        

    }

    private void Update()
    {
        if (Input.GetKeyDown("d")) {
            Debug.Log("START DEBUG");
            drawGrid();
            populateDropdowns();
            Debug.Log("DEBUG Populated Dropdowns.");
            //testSections();
            mySectionController.SetCurrentFrameSection("Sec_Steel_I");
            Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
            myConstructorController.setPoint(p4, buildingObjects.Frame);
            myConstructorController.setPoint(p5, buildingObjects.Frame);

            Debug.Log("DEBUG Tested Sections.");
            //testJoints();
            myConstructorController.createJointRestraint(p4, 'f');
            myConstructorController.createJointRestraint(p5, 'f');

            Debug.Log("DEBUG Tested Joints.");
            //testFrameDeletion();
            //Debug.Log("DEBUG Tested Frame Deletion.");

            
        }

        if (Input.GetKeyDown("r"))
        {
            frameName = "Frame_i=[" + p4.x + ":" + p4.z + ":" + p4.y + "]-j=[" + p5.x + ":" + p5.z + ":" + p5.y + "]";
            mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsSetupDeselectAllCasesAndCombosForOutput()");
            mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsSetupSetCaseSelectedForOutput(DEAD, true)");
            //mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsFrameJointForce(" + frameName + ", 0)");
            //mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsFrameForce(" + frameName + ", 0)");
            mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: customResultsGetFrameSpecialPointDispl(" + frameName + ")");

        }

        if (Input.GetKeyDown("v"))
        {
            myAnalysisController.visualizeDeformation(frameName, myAnalysisController.getLatestJointDisplResult());
        }
    }

    /// <summary>
    /// creates some building materials and frame sections
    /// this is useful to populate the various UI dropdowns so that you don't have to manualy create these each time 
    /// </summary>
    void populateDropdowns()
    {
        myMaterialController.addBuildingMaterial("Steel01", "United States", "steel", "ASTM A36", "Grade 36");
        myMaterialController.addBuildingMaterial("Steel02", "United States", "steel", "ASTM A500", "Grade C");
        myMaterialController.addBuildingMaterial("Aluminum01", "United States", "aluminum", "ASTM", "Alloy 6061 T6");

        mySectionController.addIFrameSection("Sec_Steel_I", "Steel01", 0.3f, 0.12f, 0.01f, 0.007f, 0.12f, 0.01f);
        mySectionController.addPipeFrameSection("Sec_Steel_Pipe", "Steel02", 0.2f, 0.01f);
        mySectionController.addTubeFrameSection("Sec_Aluminum_Tube", "Aluminum01", 0.16f, 0.1f, 0.007f, 0.007f);

    }

    /// <summary>
    /// Used for testing unit conversions
    /// </summary>
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

    /// <summary>
    /// creates various frame sections and draws a few frames
    /// </summary>
    void testSections()
    {
        mySectionController.SetCurrentFrameSection("Sec_Aluminum_Tube"); 
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(origin, buildingObjects.Frame);
		myConstructorController.setPoint(p1, buildingObjects.Frame);

        mySectionController.SetCurrentFrameSection("Sec_Steel_Pipe");
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(p2, buildingObjects.Frame);
		myConstructorController.setPoint(p3, buildingObjects.Frame);

        mySectionController.SetCurrentFrameSection("Sec_Steel_I");
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(p4, buildingObjects.Frame);
        myConstructorController.setPoint(p5, buildingObjects.Frame);
    }

    /// <summary>
    /// creates a few joint restraints
    /// </summary>
    void testJoints()
    {
        myConstructorController.createJointRestraint(origin, 'f');
        myConstructorController.createJointRestraint(p1, 'r');
        myConstructorController.createJointRestraint(p1, 'f'); // overwrite

        myConstructorController.createJointRestraint(p2, 'p');
        myConstructorController.createJointRestraint(p3, 'p');

        myConstructorController.createJointRestraint(p4, 'r');
        myConstructorController.createJointRestraint(p5, 'r');
        myConstructorController.deleteJointRestraint(p5); // delete JR

        myConstructorController.createJointRestraint(p6, 'f'); // This is meant to fail, as there is no frame endpoint here
    }

    /// <summary>
    /// creates and deletes frame sections
    /// </summary>
    void testFrameDeletion()
    {
        mySectionController.SetCurrentFrameSection("Sec_Steel_I");
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(p7, buildingObjects.Frame);
        myConstructorController.setPoint(p8, buildingObjects.Frame);

        mySectionController.SetCurrentFrameSection("Sec_Steel_I");
        Debug.Log("Current frame section: " + mySectionController.GetCurrentFrameSection().GetName());
        myConstructorController.setPoint(p9, buildingObjects.Frame);
        myConstructorController.setPoint(p10, buildingObjects.Frame);

        string deleteFrameName = "Frame_i=[" + p9.x + ":" + p9.z + ":" + p9.y + "]-j=[" + p10.x + ":" + p10.z + ":" + p10.y + "]";
        myConstructorController.deleteFrame(deleteFrameName);

        
    }

    /// <summary>
    /// Draws a few frame sections
    /// </summary>
    void drawFrames()
    {
        drawGrid();
        myConstructorController.setPoint(p2, buildingObjects.Frame);
        myConstructorController.setPoint(p3, buildingObjects.Frame);

        mySectionController.SetCurrentFrameSection("Sec_Aluminum_Tube");
        myConstructorController.setPoint(p4, buildingObjects.Frame);
        myConstructorController.setPoint(p5, buildingObjects.Frame);

        mySectionController.SetCurrentFrameSection("Sec_Steel_Pipe");
        myConstructorController.setPoint(origin, buildingObjects.Frame);
        myConstructorController.setPoint(p1, buildingObjects.Frame);

    }

    /// <summary>
    /// generates the grid nodes
    /// </summary>
    void drawGrid()
    {
        myGridController.createGrid(5, 5, 5, 5f);
    }
}
