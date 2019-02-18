using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionController : MonoBehaviour {
    public xmlController myXmlController;
    public materialsController myMaterialsController;
    private FrameSection currentFrameSection;



    private List<FrameSection> frameSections = new List<FrameSection>();

	// Use this for initialization
	void Start () {
        addIFrameSection("Sec_Steel_I", "Steel01", 0.3f, 0.12f, 0.01f, 0.007f, 0.12f, 0.01f);
        currentFrameSection = frameSections[0];
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
            myXmlController.GetComponent<xmlController>().addFrameSectionToXMLList(frameSection);
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    public int addIFrameSection(string name, string buildingMaterialName, float outsideHeight, float topFlangeWidth, float topFlangeThickness, float webThickness, float bottomFlangeWidth, float bottomFlangeThickness)
    {
        //Create a new iframe-type framesection and then add it to frameSections list
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.I);
        newFrameSection.SetIDimensions(outsideHeight, topFlangeWidth, topFlangeThickness, webThickness, bottomFlangeWidth, bottomFlangeThickness);
        return addFrameSection(newFrameSection);
    }

    public int addPipeFrameSection(string name, string buildingMaterialName, float outsideDiameter, float wallThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.Pipe);
        newFrameSection.SetPipeDimensions(outsideDiameter, wallThickness);
        return addFrameSection(newFrameSection);
    }

    public int addTubeFrameSection(string name, string buildingMaterialName, float outsideDepth, float outsideWidth, float flangeThickness, float webThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.Tube);
        newFrameSection.SetTubeDimensions(outsideDepth, outsideWidth, flangeThickness, webThickness);
        Debug.Log("creaged tube");
        return addFrameSection(newFrameSection);
    }

    public void deleteFrameSection(string name)
    {
        FrameSection targetFrameSection = findFrameSection(name);
        if (targetFrameSection != null)
        {
            frameSections.Remove(targetFrameSection);
            myXmlController.GetComponent<xmlController>().deleteFrameSectionFromXMLList(name);
        }
        if (frameSections.Count == 0)
        {
            //int success = addFrameSection(defaultFrameSection);
        }
    }

    public void SetCurrentFrameSection(string name)
    {
        Debug.Log("new Frame section: " + name);
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

    public List<FrameSection> getSectionList()
    {
        return frameSections;
    }
}
