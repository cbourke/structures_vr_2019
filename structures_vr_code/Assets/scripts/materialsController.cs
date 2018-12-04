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
        xmlController.GetComponent<xmlController>().addBuildingMaterialToXMLList(newMaterial);
    }

    public BuildingMaterial findBuildingMaterialWithName(string name)
    {
        foreach(BuildingMaterial bm in buildingMaterials)
        {
            if (bm.GetName() == name) {
                return bm;
            }
        }
        return null;
    }

    public void deleteBuildingMaterialWithName(string name)
    {
        BuildingMaterial bm = findBuildingMaterialWithName(name);
        if (bm != null)
        {
            buildingMaterials.Remove(bm);
            xmlController.GetComponent<xmlController>().deletebuildingMaterialFromXMLList(name);
        }
        if (buildingMaterials.Count == 0)
        {
            addBuildingMaterial(defaultBuildingMaterial);
        }
    }
}
