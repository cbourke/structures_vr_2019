using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This file is used to test various functions */
/* Its a good idea to test things here so it is a more controlled enviroment than doing it in VR */
/* NOTE that this only runs if the debug gameobject is enabled in the Unity editor */
public class debugBuilding1 : MonoBehaviour {

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

    Vector3 p7 = new Vector3(0, 1, 1);
    Vector3 p8 = new Vector3(2, 1, 1);
    Vector3 p9 = new Vector3(0, 2, 1);
    Vector3 p10 = new Vector3(2, 2, 1);

    void Start () {
        //xmlController.GetComponent<xmlController>().sendFileToSap(); //send to SAP2000

        drawGrid();
        populateDropdowns();

        drawIBeams();
        //drawPipes();
        //drawTubes();



    }
	
    /// <summary>
    /// creates some building materials and frame sections
    /// this is useful to populate the various UI dropdowns so that you don't have to manualy create these each time 
    /// </summary>
    void populateDropdowns()
    {	
        myMaterialController.addBuildingMaterial("Steel02", "United States", "steel", "ASTM A500", "Grade C");
        myMaterialController.addBuildingMaterial("Aluminum01", "United States", "aluminum", "ASTM", "Alloy 6061 T6");

        mySectionController.addIFrameSection("Sec_Steel_I", "Steel01", 0.3f, 0.12f, 0.01f, 0.007f, 0.12f, 0.01f);
        mySectionController.addTubeFrameSection("Sec_Aluminum_Tube", "Aluminum01", 0.16f, 0.1f, 0.007f, 0.007f);
        mySectionController.addPipeFrameSection("Sec_Steel_Pipe", "Steel02", 0.15f, 0.01f);
        mySectionController.addPipeFrameSection("Pipe_Small", "Steel02", 0.03f, 0.01f);
    }

    
    /// <summary>
    /// creates various frame sections and draws a few frames
    /// </summary>
    void drawIBeams()
    {
        mySectionController.SetCurrentFrameSection("Sec_Steel_I");
        drawFrame(new Vector3(1, 3, 4), new Vector3(1, 0, 4), buildingObjects.Frame);
        drawFrame(new Vector3(6, 3, 0), new Vector3(6, 0, 0), buildingObjects.Frame);
        drawFrame(new Vector3(6, 3, 4), new Vector3(6, 0, 4), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 0), new Vector3(1, 0, 0), buildingObjects.Frame);


    }
    
    /// <summary>
    /// creates various frame sections and draws a few frames O
    /// </summary>
    void drawPipes()
    {
        mySectionController.SetCurrentFrameSection("Sec_Steel_Pipe");
        //roof
        drawFrame(new Vector3(6, 4, 0), new Vector3(6, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(6, 3, 4), new Vector3(6, 3, 6), buildingObjects.Frame);
        drawFrame(new Vector3(5, 3, 6), new Vector3(5, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(5, 3, 4), new Vector3(5, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(4, 4, 0), new Vector3(4, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(4, 3, 4), new Vector3(4, 3, 6), buildingObjects.Frame);
        drawFrame(new Vector3(3, 3, 6), new Vector3(3, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(3, 3, 4), new Vector3(3, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 4), new Vector3(3, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 2), new Vector3(3, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 0), new Vector3(3, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 6), new Vector3(1, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 4), new Vector3(0, 3, 5), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 4), new Vector3(0, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 2), new Vector3(0, 3, 2), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 0), new Vector3(0, 3, 0), buildingObjects.Frame);
        drawFrame(new Vector3(3, 4, 0), new Vector3(2, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(2, 3, 4), new Vector3(2, 3, 6), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 3), new Vector3(3, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 1), new Vector3(3, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 3), new Vector3(0, 3, 3), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 1), new Vector3(0, 3, 1), buildingObjects.Frame);

        drawFrame(new Vector3(6, 4, 0), new Vector3(3, 4, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 0), new Vector3(6, 3, 0), buildingObjects.Frame);
        drawFrame(new Vector3(6, 3, 0), new Vector3(6, 3, 4), buildingObjects.Frame);
        drawFrame(new Vector3(6, 4, 0), new Vector3(6, 3, 0), buildingObjects.Frame);

        drawFrame(new Vector3(0, 3, 4), new Vector3(1, 2, 4), buildingObjects.Frame);
        drawFrame(new Vector3(0, 3, 0), new Vector3(1, 2, 0), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 6), new Vector3(1, 2, 4), buildingObjects.Frame);
        drawFrame(new Vector3(6, 2, 4), new Vector3(6, 3, 6), buildingObjects.Frame);
    }

    /// <summary>
    /// creates various frame sections and draws a few frames []
    /// </summary>
    void drawTubes()
    {
        mySectionController.SetCurrentFrameSection("Sec_Aluminum_Tube");
        
        drawFrame(new Vector3(1, 3, 4), new Vector3(1, 0, 4), buildingObjects.Frame);
        drawFrame(new Vector3(6, 3, 0), new Vector3(6, 0, 0), buildingObjects.Frame);
        drawFrame(new Vector3(6, 3, 4), new Vector3(6, 0, 4), buildingObjects.Frame);
        drawFrame(new Vector3(1, 3, 0), new Vector3(1, 0, 0), buildingObjects.Frame);
    }

    /// <summary>
    /// generates the grid nodes
    /// </summary>
    void drawGrid()
    {
        myGridController.createGrid(7, 5, 7, 1f);
    }

    void drawFrame(Vector3 p1, Vector3 p2, buildingObjects obj) {
        myConstructorController.setPoint(p1, obj);
		myConstructorController.setPoint(p2, obj);

    }
}
