using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialsController : MonoBehaviour {
    public GameObject xmlController;
    public static BuildingMaterial defaultBuildingMaterial = new BuildingMaterial("A992fy50");
    public BuildingMaterial currentMaterial;

    private List<BuildingMaterial> buildingMaterials = new List<BuildingMaterial>();


    private void Start()
    {
        addBuildingMaterial(defaultBuildingMaterial);
        currentMaterial = defaultBuildingMaterial;
    }

    public void addBuildingMaterial() // If constructed with no arguments (This is needed for xml serialization)
    {
        BuildingMaterial newMaterial = new BuildingMaterial();
        addBuildingMaterial(newMaterial);
    }

    public void addBuildingMaterial(string givenName) // If constructed with name argument
    {
        BuildingMaterial newMaterial = new BuildingMaterial(givenName);
        addBuildingMaterial(newMaterial);
    }

    public void addBuildingMaterial(string givenName, int region, int type, int standard, int grade)
    {
        BuildingMaterial newMaterial = new BuildingMaterial(givenName, region, type, standard, grade);
        addBuildingMaterial(newMaterial);
    }

    public void addBuildingMaterial(BuildingMaterial newMaterial)
    {
        buildingMaterials.Add(newMaterial);
        //xmlController.GetComponent<xmlController>().addMaterialToXMLList(newMaterial);
    }
}
