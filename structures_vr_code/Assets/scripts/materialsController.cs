using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialsController : MonoBehaviour {
    public SapTranslatorIpcHandler mySapTranslatorIpcHandler;
    public xmlController myXmlController;
    public BuildingMaterial currentMaterial;

    private List<BuildingMaterial> buildingMaterials = new List<BuildingMaterial>();


    void Awake()
    {
        currentMaterial = addBuildingMaterial("Steel01", "United States", "steel", "ASTM A36", "Grade 36");
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

    public BuildingMaterial addBuildingMaterial(string givenName, string region, string type, string standard, string grade)
    {
        BuildingMaterial newMaterial = new BuildingMaterial(givenName, region, type, standard, grade);
        addBuildingMaterial(newMaterial);
        return newMaterial;
    }

    public void addBuildingMaterial(BuildingMaterial newMaterial)
    {
        buildingMaterials.Add(newMaterial);
        myXmlController.GetComponent<xmlController>().addBuildingMaterialToXMLList(newMaterial);


        string sapTranslatorCommand = "VRE to SAPTranslator: propMaterialAddMaterial(" + 
            newMaterial.GetMaterialType() + ", " + newMaterial.GetRegion() + ", " + 
            newMaterial.GetStandard() + ", " + newMaterial.GetGrade() + ", " + newMaterial.GetName() + ")";
        // arguments: (matType, region, standard, grade, userName)
        mySapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);

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
            myXmlController.GetComponent<xmlController>().deletebuildingMaterialFromXMLList(name);

            string sapTranslatorCommand = "VRE to SAPTranslator: propMaterialDelete(" + bm.GetName() + ")";
            // arguments: (name)
            mySapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);
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

    public List<BuildingMaterial> GetMaterials() {
        return buildingMaterials;
    }
}
