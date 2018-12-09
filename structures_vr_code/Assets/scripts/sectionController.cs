using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionController : MonoBehaviour {
    public GameObject xmlController;
    public GameObject materialsController;
    private FrameSection currentFrameSection;


    private static FrameSection defaultFrameSection = new FrameSection("FSEC1", "A992fy50", FrameSectionType.I);

    private List<FrameSection> frameSections = new List<FrameSection>();

	// Use this for initialization
	void Start () {
        //addFrameSection(defaultFrameSection);
        currentFrameSection = defaultFrameSection;
	}

    public FrameSection findFrameSection(string name)
    {
        FrameSection output = null;
        if (frameSections.Count > 0)
        {
            foreach (FrameSection fs in frameSections)
            {
                if (fs.GetName().Equals(name))
                {
                    output = fs;
                    break;
                }
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

    public int addIFrameSection(string name, string buildingMaterialName, double outsideHeight, double topFlangeWidth, double topFlangeThickness, double webThickness, double bottomFlangeWidth, double bottomFlangeThickness)
    {
        //Create a new iframe-type framesection and then add it to frameSections list
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.I);
        newFrameSection.SetIDimensions(outsideHeight, topFlangeWidth, topFlangeThickness, webThickness, bottomFlangeWidth, bottomFlangeThickness);
        return addFrameSection(newFrameSection);
    }

    public int addPipeFrameSection(string name, string buildingMaterialName, double outsideDiameter, double wallThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.Pipe);
        newFrameSection.SetPipeDimensions(outsideDiameter, wallThickness);
        return addFrameSection(newFrameSection);
    }

    public int addTubeFrameSection(string name, string buildingMaterialName, double outsideDepth, double outsideWidth, double flangeThickness, double webThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.Tube);
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
            //int success = addFrameSection(defaultFrameSection);
        }
    }

    public void SetCurrentFrameSection(string name)
    {
        FrameSection fs = findFrameSection(name);
        if (fs != null)
        {
            currentFrameSection = fs;
        }
    }

    public FrameSection GetCurrentFrameSection()
    {
        return currentFrameSection;
    }
}
