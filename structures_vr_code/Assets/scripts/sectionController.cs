using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionController : MonoBehaviour {
    public GameObject xmlController;
    public FrameSection currentFrameSection;

    private static FrameSection defaultFrameSection = new FrameSection("FSEC1", materialsController.defaultBuildingMaterial, FrameSectionType.I);

    private List<FrameSection> frameSections;

	// Use this for initialization
	void Start () {
        addFrameSection(defaultFrameSection);
        currentFrameSection = defaultFrameSection;
	}

    public FrameSection findFrameSection(string name)
    {
        FrameSection output = null;
        foreach (FrameSection fs in frameSections)
        {
            if (fs.GetName() == name)
            {
                output = fs;
            }
        }
        return output;
    }

    public int addFrameSection(FrameSection frameSection)
    {
        if (findFrameSection(frameSection.GetName()) == null)
        {
            frameSections.Add(frameSection);
            xmlController.GetComponent<xmlController>().addFrameSectionToXMLList(frameSection);
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    public int addIFrameSection(string name, BuildingMaterial buildingMaterial, double outsideHeight, double topFlangeWidth, double topFlangeThickness, double webThickness, double bottomFlangeWidth, double bottomFlangeThickness)
    {
        //Create a new iframe-type framesection and then add it to frameSections list
        FrameSection newFrameSection = new FrameSection(name, buildingMaterial, FrameSectionType.I);
        newFrameSection.SetIDimensions(outsideHeight, topFlangeWidth, topFlangeThickness, webThickness, bottomFlangeWidth, bottomFlangeThickness);
        return addFrameSection(newFrameSection);
    }

    public int addPipeFrameSection(string name, BuildingMaterial buildingMaterial, double outsideDiameter, double wallThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterial, FrameSectionType.Pipe);
        newFrameSection.SetPipeDimensions(outsideDiameter, wallThickness);
        return addFrameSection(newFrameSection);
    }

    public int addTubeFrameSection(string name, BuildingMaterial buildingMaterial, double outsideDepth, double outsideWidth, double flangeThickness, double webThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterial, FrameSectionType.Tube);
        newFrameSection.SetTubeDimensions(outsideDepth, outsideWidth, flangeThickness, webThickness);
        return addFrameSection(newFrameSection);
    }

    public void deleteFrameSection(string name)
    {
        FrameSection targetFrameSection = findFrameSection(name);
        if (targetFrameSection != null)
        {
            frameSections.Remove(targetFrameSection);
            xmlController.GetComponent<xmlController>().deleteFrameSectionFromXMLList(name);
        }
        if (frameSections.Count == 0)
        {
            int success = addFrameSection(defaultFrameSection);
        }
    }


}
