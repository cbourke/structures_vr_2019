using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class handles the frame sections */
public class sectionController : MonoBehaviour {
    public SapTranslatorIpcHandler sapTranslatorIpcHandler;
    public xmlController myXmlController;
    public materialsController myMaterialsController;
    private FrameSection currentFrameSection;



    private List<FrameSection> frameSections = new List<FrameSection>();

    /// <summary>
    /// Creates a default frame section
    /// </summary>
	void Awake () {
        //addIFrameSection("Sec_Steel_I", "Steel01", 0.3f, 0.12f, 0.01f, 0.007f, 0.12f, 0.01f);
        //currentFrameSection = frameSections[0];
	}

    /// <summary>
    /// finds a frame section given its name and returns the section
    /// </summary>
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

    /// <summary>
    /// Add a new FrameSection to the list of FrameSections
    /// Also tells SAP Translator to set the FrameSection's "SectProps" property (currently commented-out)
    /// </summary>
    public int addFrameSection(FrameSection frameSection)
    {
        if (findFrameSection(frameSection.GetName()) == null)
        {
            frameSections.Add(frameSection);
            if (frameSections.Count == 1)
            {
                SetCurrentFrameSection(frameSection.GetName());
            }
            //sapTranslatorIpcHandler.propFrameGetSectProps(frameSection);
            myXmlController.GetComponent<xmlController>().addFrameSectionToXMLList(frameSection);
            return 1;
        }
        else
        {
            return 0;
        }
        
    }

    /// <summary>
    /// Create a new iframe-type framesection and then add it to frameSections list
    /// </summary>
    public int addIFrameSection(string name, string buildingMaterialName, float outsideHeight, float topFlangeWidth, float topFlangeThickness, float webThickness, float bottomFlangeWidth, float bottomFlangeThickness)
    {
        FrameSection newFrameSection = new FrameSection(name, buildingMaterialName, FrameSectionType.I);
        newFrameSection.SetIDimensions(outsideHeight, topFlangeWidth, topFlangeThickness, webThickness, bottomFlangeWidth, bottomFlangeThickness);

        string sapTranslatorCommand = "VRE to SAPTranslator: propFrameSetISection(" +
            name + ", " + buildingMaterialName + ", " + outsideHeight + ", " + topFlangeWidth + ", "
            + topFlangeThickness + ", " + webThickness + ", " + bottomFlangeWidth + ", " + bottomFlangeThickness +")";
        // arguments: (name, matProp, t3, t2, tf, tw, t2b, tfb, [color], [notes], [guid])
        sapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);

        return addFrameSection(newFrameSection);
    }

    /// <summary>
    /// Create a new pipe-type framesection and then add it to frameSections list
    /// </summary>
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

    /// <summary>
    /// Create a new tube-type framesection and then add it to frameSections list
    /// </summary>
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

    /// <summary>
    /// Deletes a frame section
    /// </summary>
    public void deleteFrameSection(string name)
    {
        FrameSection targetFrameSection = findFrameSection(name);
        if (targetFrameSection != null)
        {
            frameSections.Remove(targetFrameSection);
            myXmlController.GetComponent<xmlController>().deleteFrameSectionFromXMLList(name);

            string sapTranslatorCommand = "VRE to SAPTranslator: propFrameDelete(" + targetFrameSection.GetName() + ")";
            // arguments: (name)
            sapTranslatorIpcHandler.enqueueToOutputBuffer(sapTranslatorCommand);
        }
        if (frameSections.Count == 0)
        {
            //int success = addFrameSection(defaultFrameSection);
        }
    }

    /// <summary>
    /// Sets the current frame section being used
    /// </summary>
    public void SetCurrentFrameSection(string name)
    {
        FrameSection fs = findFrameSection(name);
        if (fs != null)
        {
            currentFrameSection = fs;
        }
    }

    /// <summary>
    /// Returns the current frame section
    /// </summary>
    public FrameSection GetCurrentFrameSection()
    {
        return currentFrameSection;
    }

    /// <summary>
    /// Returns a list of all the frame sections
    /// </summary>
    public List<FrameSection> getSectionList()
    {
        return frameSections;
    }
}
