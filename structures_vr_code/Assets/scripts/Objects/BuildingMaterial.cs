using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class defines a BuildingMaterial object */
public class BuildingMaterial {
    private string userDefinedName = "Default_Material_Name";
    private string region;
    private string type;
    private string standard;
    private string grade;

	/// <summary>
    /// If constructed with no arguments (This is needed for xml serialization, I think?)
	/// </summary>
    public BuildingMaterial()
    {

    }

	/// <summary>
    /// Constructor given name
	/// </summary>
    public BuildingMaterial(string givenName)
    {
        SetName(givenName);
    }

	/// <summary>
	/// Constructor given the name, region, type, standard, and grade
    /// This is used by the UI's Define New Material pane
	/// </summary>
    public BuildingMaterial(string givenName, string region, string type, string standard, string grade)
    {
        SetName(givenName);
        SetAttributes(region, type, standard, grade);
    }

	/// <summary>
	/// Sets the values of a material
	/// </summary>
    public void SetAttributes(string region, string type, string standard, string grade)
    {
        SetRegion(region);
        if (region == this.region) // If setRegion was successful
        {
            SetType(type);
            if (type == this.type) // If setType was successful
            {
                SetStandard(standard);
                if (standard == this.standard) // If setStandard was successful
                {
                    SetGrade(grade);
                }
            }
        }
        
    }

	/// <summary>
	/// Sets the material name
	/// </summary>
    public void SetName(string newName)
    {
        userDefinedName = newName;
    }

	/// <summary>
	/// Sets a the materials region
    /// Must be valid in BuildingMaterialAttributes.cs
	/// </summary>
    public void SetRegion(string newRegion) 
    {
        region = newRegion;
    }

	/// <summary>
	/// Sets the material type
    /// We only need to check validity within domain of defined constants, not all numbers...
	/// </summary>
    public void SetType(string newType)
    {
        type = newType;
    }

	/// <summary>
	/// Sets the standard
	/// </summary>
    public void SetStandard(string newStandard)
    {
        standard = newStandard;
    }

	/// <summary>
	/// Sets the grade
	/// </summary>
    public void SetGrade(string newGrade)
    {
        grade = newGrade;
    }

    public string GetName()
    {
        return userDefinedName;
    }

    public string GetRegion()
    {
        return region;
    }

    public string GetMaterialType()
    {
        return type;
    }

    public string GetStandard()
    {
        return standard;
    }

    public string GetGrade()
    {
        return grade;
    }

}
