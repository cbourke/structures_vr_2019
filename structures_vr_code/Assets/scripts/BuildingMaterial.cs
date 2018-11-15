using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaterial : MonoBehaviour {
    public string userDefinedName;

    public int region;
    public int type;
    public int standard;
    public int grade;

    public Vector3 color;

    public BuildingMaterial(string name) // If constructed with no arguments
    {

    }


    public void setMaterial(int region, int type, int standard, int grade)
    {
        this.region = region;
        this.type = type;
        this.standard = standard;
        this.grade = grade;
    }

}
