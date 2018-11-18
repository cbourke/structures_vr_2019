using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaterial : MonoBehaviour {
    public string userDefinedName = "Default_Material_Name";
    private int region = BuildingMaterialAttributes.Regions.UNITEDSTATES;
    private int type = BuildingMaterialAttributes.Regions.UnitedStatesTypes.STEEL;
    private int standard = BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A992;
    private int grade = BuildingMaterialAttributes.Regions.UnitedStatesTypes.SteelStandards.A992Grades.GRADE_50;

    public BuildingMaterial() // If constructed with no arguments (This is needed for xml serialization)
    {

    }

    public BuildingMaterial(string givenName) // If constructed with name argument
    {
        SetName(givenName);
    }

    public BuildingMaterial(string givenName, int region, int type, int standard, int grade)
    {
        SetName(givenName);
        SetAttributes(region, type, standard, grade);
    }

    public void SetAttributes(int region, int type, int standard, int grade)
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

    public void SetRegion(int newRegion) // Must be valid in BuildingMaterialAttributes.cs
    {
        if(System.Array.BinarySearch(BuildingMaterialAttributes.Regions.members, newRegion) >= 0) {
            region = newRegion;
            type = region + 1000;
            standard = type + 100;
            grade = standard + 10;
        } else {
            Debug.Log("Could not change region of material \"" + userDefinedName + "\".");
        }
    }

    public void SetType(int newType) // We only need to check validity within domain of defined constants, not all numbers...
    {
        // Check that the 5th digit from right is the same for type and region (using integer division)
        if (newType / 10000 == region / 10000) {
            type = newType;
            standard = type + 100;
            grade = standard + 10;
        } else {
            Debug.Log("Could not change type of material \"" + userDefinedName + "\".");
        }
    }

    public void SetStandard(int newStandard)
    {
        // Check that the 5th and 4th digits from right are the same for standard and type (using integer division)
        if (newStandard / 1000 == type / 1000) {
            standard = newStandard;
            grade = standard + 10;
        } else {
            Debug.Log("Could not change standard of material \"" + userDefinedName + "\".");
        }
    }

    public void SetGrade(int newGrade)
    {
        // Check that the 5th, 4th, and 3rd digits from right are the same for grade and standard (using integer division)
        if (newGrade / 100 == standard / 100) {
            grade = newGrade;
        } else {
            Debug.Log("Could not change grade of material \"" + userDefinedName + "\".");
        }
    }

    public string GetUserDefinedName()
    {
        return userDefinedName;
    }

    public int GetRegion()
    {
        return region;
    }

    public int GetMaterialType()
    {
        return type;
    }

    public int GetStandard()
    {
        return standard;
    }

    public int GetGrade()
    {
        return grade;
    }

}
