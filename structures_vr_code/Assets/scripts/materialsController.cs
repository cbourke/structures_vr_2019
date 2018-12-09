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
       // addBuildingMaterial(defaultBuildingMaterial);
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

    public void addBuildingMaterial(string givenName, string region, string type, string standard, string grade)
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
        if (buildingMaterials.Count > 0)
        {
            foreach (BuildingMaterial bm in buildingMaterials)
            {
                if (bm.GetName().Equals(name))
                {
                    return bm;
                }
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
            //addBuildingMaterial(defaultBuildingMaterial);
        }
    }

    public void setCurrentMaterial(string name)
    {
        BuildingMaterial bm = findBuildingMaterialWithName(name);
        if (bm != null)
        {
            currentMaterial = bm;
        }
    }

    public BuildingMaterial GetCurrentBuildingMaterial()
    {
        return currentMaterial;
    }
}
