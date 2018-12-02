using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section {
    protected string userDefinedName = "Default_Section_Name";

    public void SetName(string newName) {
        userDefinedName = newName;
    }
    public string GetName() {
        return userDefinedName;
    }
}




public class FrameSection : Section
{
    //private BuildingMaterial material;
    // We only need a reference to the name of a BuildingMaterial, this makes serialization easier
    private string buildingMaterialName;
    private FrameSectionType type;
    private double[] dimensions = new double[6];

    public FrameSection(string name, BuildingMaterial buildingMaterial, FrameSectionType type)
    {
        userDefinedName = name;
        buildingMaterialName = buildingMaterial.GetName();
        this.type = type;

        switch (type)
        {
            case FrameSectionType.I:
                {
                    SetIDimensions(0.3048, 0.127, 0.009652, 0.006350, 0.127, 0.009652);
                    break;
                }
            case FrameSectionType.Pipe:
                {
                    SetPipeDimensions(0.1524, 0.00635);
                    break;
                }
            case FrameSectionType.Tube:
                {
                    SetTubeDimensions(0.1524, 0.1016, 0.006350, 0.006350);
                    break;
                }
        }
    }

    public void SetMaterial(BuildingMaterial newMaterial) {
        buildingMaterialName = newMaterial.GetName();
    }
    public void SetMaterial(string newMaterialName)
    {
        buildingMaterialName = newMaterialName;
    }
    public string GetMaterialName() {
        return buildingMaterialName;
    }

    public void SetIDimensions(double outsideHeight, double topFlangeWidth, double topFlangeThickness, double webThickness, double bottomFlangeWidth, double bottomFlangeThickness)
    {
        dimensions[0] = outsideHeight;
        dimensions[1] = topFlangeWidth;
        dimensions[2] = topFlangeThickness;
        dimensions[3] = webThickness;
        dimensions[4] = bottomFlangeWidth;
        dimensions[5] = bottomFlangeThickness;
    }
    public void SetPipeDimensions(double outsideDiameter, double wallThickness)
    {
        dimensions[0] = outsideDiameter;
        dimensions[1] = wallThickness;
        dimensions[2] = 0.0;
        dimensions[3] = 0.0;
        dimensions[4] = 0.0;
        dimensions[5] = 0.0;
    }
    public void SetTubeDimensions(double outsideDepth, double outsideWidth, double flangeThickness, double webThickness)
    {
        dimensions[0] = outsideDepth;
        dimensions[1] = outsideWidth;
        dimensions[2] = flangeThickness;
        dimensions[3] = webThickness;
        dimensions[4] = 0.0;
        dimensions[5] = 0.0;
    }
    public void SetRawDimensions(double dim0, double dim1, double dim2, double dim3, double dim4, double dim5)
    {
        dimensions[0] = dim0;
        dimensions[1] = dim1;
        dimensions[2] = dim2;
        dimensions[3] = dim3;
        dimensions[4] = dim4;
        dimensions[5] = dim5;
    }

    public double[] GetDimensions()
    {
        return dimensions;
    }


    public double GetIOutsideHeight()
    {
        if (type == FrameSectionType.I) return dimensions[0];
        else return 0.0;
    }
    public double GetITopFlangeWidth()
    {
        if (type == FrameSectionType.I) return dimensions[1];
        else return 0.0;
    }
    public double GetITopFlangeThickness()
    {
        if (type == FrameSectionType.I) return dimensions[2];
        else return 0.0;
    }
    public double GetIWebThickness()
    {
        if (type == FrameSectionType.I) return dimensions[3];
        else return 0.0;
    }
    public double GetIBottomFlangeWidth()
    {
        if (type == FrameSectionType.I) return dimensions[4];
        else return 0.0;
    }
    public double GetIBottomFlangeThickness()
    {
        if (type == FrameSectionType.I) return dimensions[5];
        else return 0.0;
    }


    public double GetPipeOutsideDiameter()
    {
        if (type == FrameSectionType.Pipe) return dimensions[0];
        else return 0.0;
    }
    public double GetPipeWallThickness()
    {
        if (type == FrameSectionType.Pipe) return dimensions[1];
        else return 0.0;
    }


    public double GetTubeOutsideDepth()
    {
        if (type == FrameSectionType.Tube) return dimensions[0];
        else return 0.0;
    }
    public double GetTubeOutsideWidth()
    {
        if (type == FrameSectionType.Tube) return dimensions[1];
        else return 0.0;
    }
    public double GetTubeFlangeThickness()
    {
        if (type == FrameSectionType.Tube) return dimensions[2];
        else return 0.0;
    }
    public double GetTubeWebThickness()
    {
        if (type == FrameSectionType.Tube) return dimensions[3];
        else return 0.0;
    }
}