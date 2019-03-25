using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class handles the creation, deletion, and storing of materials */
public class materialsController : MonoBehaviour {
    public SapTranslatorIpcHandler mySapTranslatorIpcHandler;
    public xmlController myXmlController;
    public BuildingMaterial currentMaterial;

    private List<BuildingMaterial> buildingMaterials = new List<BuildingMaterial>();

    /// <summary>
    /// Sets a default material
    /// </summary>
    void Awake()
    {
        currentMaterial = addBuildingMaterial("Steel01", "United States", "steel", "ASTM A36", "Grade 36");
    }

    /// <summary>
    /// Used for XML serialization
    /// </summary>
    public void addBuildingMaterial()
    {
        BuildingMaterial newMaterial = new BuildingMaterial();
        addBuildingMaterial(newMaterial);
    }

    /// <summary>
    /// Construct a material with just a name
    /// </summary>
    public void addBuildingMaterial(string givenName)
    {
        BuildingMaterial newMaterial = new BuildingMaterial(givenName);
        addBuildingMaterial(newMaterial);
    }

    /// <summary>
    /// Construct a new material. This is called by the UI create materials page
    /// Returns the material
    /// </summary>
    public BuildingMaterial addBuildingMaterial(string givenName, string region, string type, string standard, string grade)
    {
        BuildingMaterial newMaterial = new BuildingMaterial(givenName, region, type, standard, grade);
        addBuildingMaterial(newMaterial);
        return newMaterial;
    }

    /// <summary>
    /// Adds a BuildingMaterial to the list of materials
    /// This is called after a new material is created given its name or name and parameters
    /// Also sends the data to SAP Translator
    /// </summary>
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

    /// <summary>
    /// Searches for a building material given its name and returns it if it exists
    /// returns null if the material does not exist
    /// </summary>
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

    /// <summary>
    /// Deletes a building material given its name
    /// </summary>
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

    /// <summary>
    /// Sets the current building material that the user is using
    /// </summary>
    public void setCurrentMaterial(string name)
    {
        BuildingMaterial bm = findBuildingMaterialWithName(name);
        if (bm != null)
        {
            currentMaterial = bm;
        }
    }

    /// <summary>
    /// Returns the current material being used
    /// </summary>
    public BuildingMaterial GetCurrentBuildingMaterial()
    {
        return currentMaterial;
    }

    /// <summary>
    /// Returns a list of all the building materials
    /// </summary>
    public List<BuildingMaterial> GetMaterials() {
        return buildingMaterials;
    }
}
