using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class defines a frame section */
public class FrameSection
{
    // We only need a reference to the name of a BuildingMaterial, this makes serialization easier
    public string name;
    public string buildingMaterialName;
    public FrameSectionType type;
    public float[] dimensions = new float[6];

    /// <summary>
    /// Empty constructor
    /// </summary>
    public FrameSection()
    {

    }

    /// <summary>
    /// Used to construct a Frame Section object
    /// Gets passed the name, building material name, and FrameSectionType type
    /// Default values are passed into the SetDimensions function, these are changed
    /// depending on the users input
    /// </summary>
    public FrameSection(string name, string buildingMaterialName, FrameSectionType type)
    {
        this.name = name;
        this.buildingMaterialName = buildingMaterialName;
        this.type = type;

        switch (type)
        {
            case FrameSectionType.I:
                {
                    SetIDimensions(0.3048f, 0.127f, 0.009652f, 0.006350f, 0.127f, 0.009652f);
                    break;
                }
            case FrameSectionType.Pipe:
                {
                    SetPipeDimensions(0.1524f, 0.00635f);
                    break;
                }
            case FrameSectionType.Tube:
                {
                    SetTubeDimensions(0.1524f, 0.1016f, 0.006350f, 0.006350f);
                    break;
                }
        }
    }

    /// <summary>
    /// Sets the name of the section
    /// </summary>
    public void SetName(string newName)
    {
        name = newName;
    }

    /// <summary>
    /// Returns the name of the section 
    /// </summary>
    public string GetName()
    {
        return name;
    }

    /// <summary>
    /// Sets the material given a BuildingMaterial type
    /// </summary>
    public void SetMaterial(BuildingMaterial newMaterial) {
        buildingMaterialName = newMaterial.GetName();
    }

    /// <summary>
    /// Sets the material given the material name
    /// </summary>
    public void SetMaterial(string newMaterialName)
    {
        buildingMaterialName = newMaterialName;
    }

    /// <summary>
    /// Returns the material name 
    /// </summary>
    public string GetMaterialName() {
        return buildingMaterialName;
    }

    /// <summary>
    /// Sets the dimensions for an Ibeam
    /// </summary>
    public void SetIDimensions(float outsideHeight, float topFlangeWidth, float topFlangeThickness, float webThickness, float bottomFlangeWidth, float bottomFlangeThickness)
    {
        dimensions[0] = outsideHeight;
        dimensions[1] = topFlangeWidth;
        dimensions[2] = topFlangeThickness;
        dimensions[3] = webThickness;
        dimensions[4] = bottomFlangeWidth;
        dimensions[5] = bottomFlangeThickness;
    }

    /// <summary>
    /// Sets the dimensions for a Pipe
    /// </summary>
    public void SetPipeDimensions(float outsideDiameter, float wallThickness)
    {
        dimensions[0] = outsideDiameter;
        dimensions[1] = wallThickness;
        dimensions[2] = 0.0f;
        dimensions[3] = 0.0f;
        dimensions[4] = 0.0f;
        dimensions[5] = 0.0f;
    }

    /// <summary>
    /// Sets the dimensions for a Tube 
    /// </summary>
    public void SetTubeDimensions(float outsideDepth, float outsideWidth, float flangeThickness, float webThickness)
    {
        dimensions[0] = outsideDepth;
        dimensions[1] = outsideWidth;
        dimensions[2] = flangeThickness;
        dimensions[3] = webThickness;
        dimensions[4] = 0.0f;
        dimensions[5] = 0.0f;
    }

    /// <summary>
    /// generic set dimension. Not sure where this is being used 
    /// </summary>
    public void SetRawDimensions(float dim0, float dim1, float dim2, float dim3, float dim4, float dim5)
    {
        dimensions[0] = dim0;
        dimensions[1] = dim1;
        dimensions[2] = dim2;
        dimensions[3] = dim3;
        dimensions[4] = dim4;
        dimensions[5] = dim5;
    }

    /// <summary>
    /// Returns the array of dimensions 
    /// </summary>
    public float[] GetDimensions()
    {
        return dimensions;
    }

    /* various getters */
    public float GetIOutsideHeight()
    {
        if (type == FrameSectionType.I) return dimensions[0];
        else return 0.0f;
    }
    public float GetITopFlangeWidth()
    {
        if (type == FrameSectionType.I) return dimensions[1];
        else return 0.0f;
    }
    public float GetITopFlangeThickness()
    {
        if (type == FrameSectionType.I) return dimensions[2];
        else return 0.0f;
    }
    public float GetIWebThickness()
    {
        if (type == FrameSectionType.I) return dimensions[3];
        else return 0.0f;
    }
    public float GetIBottomFlangeWidth()
    {
        if (type == FrameSectionType.I) return dimensions[4];
        else return 0.0f;
    }
    public float GetIBottomFlangeThickness()
    {
        if (type == FrameSectionType.I) return dimensions[5];
        else return 0.0f;
    }


    public float GetPipeOutsideDiameter()
    {
        if (type == FrameSectionType.Pipe) return dimensions[0];
        else return 0.0f;
    }
    public float GetPipeWallThickness()
    {
        if (type == FrameSectionType.Pipe) return dimensions[1];
        else return 0.0f;
    }


    public float GetTubeOutsideDepth()
    {
        if (type == FrameSectionType.Tube) return dimensions[0];
        else return 0.0f;
    }
    public float GetTubeOutsideWidth()
    {
        if (type == FrameSectionType.Tube) return dimensions[1];
        else return 0.0f;
    }
    public float GetTubeFlangeThickness()
    {
        if (type == FrameSectionType.Tube) return dimensions[2];
        else return 0.0f;
    }
    public float GetTubeWebThickness()
    {
        if (type == FrameSectionType.Tube) return dimensions[3];
        else return 0.0f;
    }

    public void SetFrameSectionType(FrameSectionType type)
    {
        this.type = type;
    }

    public FrameSectionType GetFrameSectionType()
    {
        return type;
    }
}