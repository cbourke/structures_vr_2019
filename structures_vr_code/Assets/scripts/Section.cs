using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section {
    private string userDefinedName = "Default_Section_Name";

    public void SetName(string newName) {
        userDefinedName = newName;
    }
    public string GetName() {
        return userDefinedName;
    }
}




public class FrameSection : Section
{
    private BuildingMaterial material;

    public void SetMaterial(BuildingMaterial newMaterial) {
        material = newMaterial;
    }
    public BuildingMaterial GetMaterial() {
        return material;
    }
}




public class IFrameSection : FrameSection
{
    private double outsideHeight = 0.3048; //SAP's default value in meters
    private double topFlangeWidth = 0.127; //SAP's default value in meters
    private double topFlangeThickness = 0.009652; //SAP's default value in meters
    private double webThickness = 0.006350; //SAP's default value in meters
    private double bottomFlangeWidth = 0.127; //SAP's default value in meters
    private double bottomFlangeThickness = 0.009652; //SAP's default value in meters

    public IFrameSection()
    {

    }

    public IFrameSection(string name)
    {
        SetName(name);
    }

    public IFrameSection(string name, BuildingMaterial material, double outsideHeight, double topFlangeWidth, double topFlangeThickness, double webThickness, double bottomFlangeWidth, double bottomFlangeThickness)
    {
        SetName(name);
        SetMaterial(material);
        SetDimensions(outsideHeight, topFlangeWidth, topFlangeThickness, webThickness, bottomFlangeWidth, bottomFlangeThickness);
    }


    public void SetDimensions(double outsideHeight, double topFlangeWidth, double topFlangeThickness, double webThickness, double bottomFlangeWidth, double bottomFlangeThickness)
    {
        SetOutsideHeight(outsideHeight);
        SetTopFlangeWidth(topFlangeWidth);
        SetTopFlangeThickness(topFlangeThickness);
        SetWebThickness(webThickness);
        SetBottomFlangeWidth(bottomFlangeWidth);
        SetBottomFlangeThickness(bottomFlangeThickness);
    }


    public void SetOutsideHeight(double newOutsideHeight)
    {
        outsideHeight = newOutsideHeight;
    }
    public void SetTopFlangeWidth(double newTopFlangeWidth)
    {
        topFlangeWidth = newTopFlangeWidth;
    }
    public void SetTopFlangeThickness(double newTopFlangeThickness)
    {
        topFlangeThickness = newTopFlangeThickness;
    }
    public void SetWebThickness(double newWebThickness)
    {
        webThickness = newWebThickness;
    }
    public void SetBottomFlangeWidth(double newBottomFlangeWidth)
    {
        bottomFlangeWidth = newBottomFlangeWidth;
    }
    public void SetBottomFlangeThickness(double newBottomFlangeThickness)
    {
        bottomFlangeThickness = newBottomFlangeThickness;
    }

    public double GetOutsideHeight()
    {
        return outsideHeight;
    }
    public double GetTopFlangeWidth()
    {
        return topFlangeWidth;
    }
    public double GetTopFlangeThickness()
    {
        return topFlangeThickness;
    }
    public double GetWebThickness()
    {
        return webThickness;
    }
    public double GetBottomFlangeWidth()
    {
        return bottomFlangeWidth;
    }
    public double GetBottomFlangeThickness()
    {
        return bottomFlangeThickness;
    }
}




public class PipeFrameSection : FrameSection
{
    private double outsideDiameter = 0.1524; //SAP's default value in meters
    private double wallThickness = 0.00635; //SAP's default value in meters

    public PipeFrameSection()
    {

    }

    public PipeFrameSection(string name)
    {
        SetName(name);
    }

    public PipeFrameSection(string name, double outsideDiameter, double wallThickness)
    {
        SetName(name);
        SetDimensions(outsideDiameter, wallThickness);
    }


    public void SetDimensions(double outsideDiameter, double wallThickness)
    {
        SetOutsideDiameter(outsideDiameter);
        SetWallThickness(wallThickness);
    }


    public void SetOutsideDiameter(double newOutsideDiameter)
    {
        outsideDiameter = newOutsideDiameter;
    }
    public void SetWallThickness(double newWallThickness)
    {
        wallThickness = newWallThickness;
    }

    public double GetOutsideDiameter()
    {
        return outsideDiameter;
    }
    public double GetWallThickness()
    {
        return wallThickness;
    }

}




public class TubeFrameSection : FrameSection
{
    private double outsideDepth = 0.1524; //SAP's default value in meters
    private double outsideWidth = 0.1016; //SAP's default value in meters
    private double flangeThickness = 0.006350; //SAP's default value in meters
    private double webThickness = 0.006350; //SAP's default value in meters

    public TubeFrameSection()
    {

    }

    public TubeFrameSection(string name)
    {
        SetName(name);
    }

    public TubeFrameSection(string name, double outsideDepth, double outsideWidth, double flangeThickness, double webThickness)
    {
        SetName(name);
        SetDimensions(outsideDepth, outsideWidth, flangeThickness, webThickness);
    }


    public void SetDimensions(double outsideDepth, double outsideWidth, double flangeThickness, double webThickness)
    {
        SetOutsideDepth(outsideDepth);
        SetOutsideWidth(outsideWidth);
        SetFlangeThickness(flangeThickness);
        SetWebThickness(webThickness);
    }


    public void SetOutsideDepth(double newOutsideDepth)
    {
        outsideDepth = newOutsideDepth;
    }
    public void SetOutsideWidth(double newOutsideWidth)
    {
        outsideWidth = newOutsideWidth;
    }
    public void SetFlangeThickness(double newFlangeThickness)
    {
        flangeThickness = newFlangeThickness;
    }
    public void SetWebThickness(double newWebThickness)
    {
        webThickness = newWebThickness;
    }

    public double GetOutsideDepth()
    {
        return outsideDepth;
    }
    public double GetOutsideWidth()
    {
        return outsideWidth;
    }
    public double GetFlangeThickness()
    {
        return flangeThickness;
    }
    public double GetWebThickness()
    {
        return webThickness;
    }
}