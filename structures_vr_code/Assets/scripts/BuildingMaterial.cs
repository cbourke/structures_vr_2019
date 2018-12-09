using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaterial {
    private string userDefinedName = "Default_Material_Name";
    private string region = BuildingMaterialAttributes.Regions.UNITEDSTATES;
    private string type = BuildingMaterialAttributes.Regions.UnitedStatesTypes.STEEL;
    private string standard = BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A992;
    private string grade = BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A992Grades.GRADE_50;

    public BuildingMaterial() // If constructed with no arguments (This is needed for xml serialization, I think?)
    {

    }

    public BuildingMaterial(string givenName) // If constructed with name argument
    {
        SetName(givenName);
    }

    public BuildingMaterial(string givenName, string region, string type, string standard, string grade)
    {
        SetName(givenName);
        SetAttributes(region, type, standard, grade);
    }

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

    public void resetDefaultAttributes()
    {
        region = BuildingMaterialAttributes.Regions.UNITEDSTATES;
        type = BuildingMaterialAttributes.Regions.UnitedStatesTypes.STEEL;
        standard = BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A992;
        grade = BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A992Grades.GRADE_50;
    }

    public void SetName(string newName) // Can be anything
    {
        userDefinedName = newName;
    }

    public void SetRegion(string newRegion) // Must be valid in BuildingMaterialAttributes.cs
    {
        region = newRegion;
    }

    public void SetType(string newType) // We only need to check validity within domain of defined constants, not all numbers...
    {
        type = newType;
    }

    public void SetStandard(string newStandard)
    {
        standard = newStandard;
    }

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
