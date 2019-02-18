using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionController : MonoBehaviour {
    public SapTranslatorIpcHandler sapTranslatorIpcHandler;
    public xmlController myXmlController;
    public materialsController myMaterialsController;
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

        string sapTranslatorCommand = "VRE to SAPTranslator: propFrameSetISection(" +
            name + ", " + buildingMaterialName + ", " + outsideHeight + ", " + topFlangeWidth + ", "
            + topFlangeThickness + ", " + webThickness + ", " + bottomFlangeWidth + ", " + bottomFlangeThickness +")";
        // arguments: (name, matProp, t3, t2, tf, tw, t2b, tfb, [color], [notes], [guid])
        sapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);

        return addFrameSection(newFrameSection);
    }

    public int addPipeFrameSection(string name, string buildingMaterialName, float outsideDiameter, float wallThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.Pipe);
        newFrameSection.SetPipeDimensions(outsideDiameter, wallThickness);

        string sapTranslatorCommand = "VRE to SAPTranslator: propFrameSetPipe(" +
            name + ", " + buildingMaterialName + ", " + outsideDiameter + ", " + wallThickness + ")";
        // arguments: (name, matProp, t3, tw, [color], [notes], [guid])
        sapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);

        return addFrameSection(newFrameSection);
    }

    public int addTubeFrameSection(string name, string buildingMaterialName, float outsideDepth, float outsideWidth, float flangeThickness, float webThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.Tube);
        newFrameSection.SetTubeDimensions(outsideDepth, outsideWidth, flangeThickness, webThickness);
        Debug.Log("created tube");

        string sapTranslatorCommand = "VRE to SAPTranslator: propFrameSetTube(" +
            name + ", " + buildingMaterialName + ", " + outsideDepth + ", " + outsideWidth + ", "
            + flangeThickness + ", " + webThickness + ")";
        // arguments: (name, matProp, t3, t2, tf, tw, [color], [notes], [guid])
        sapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);

        return addFrameSection(newFrameSection);
    }

    public void deleteFrameSection(string name)
    {
        FrameSection targetFrameSection = findFrameSection(name);
        if (targetFrameSection != null)
        {
            frameSections.Remove(targetFrameSection);
            myXmlController.GetComponent<xmlController>().deleteFrameSectionFromXMLList(name);

            string sapTranslatorCommand = "VRE to SAPTranslator: propFrameSetTube(" + targetFrameSection.GetName() + ")";
            // arguments: (name)
            sapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);
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
