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
        currentFrameSection = defaultFrameSection;
	}

    public void addFrameSection(FrameSection frameSection)
    {
        frameSections.Add(frameSection);
    }

    public void addIFrameSection(/* I frame dimension parameters */)
    {
        //TODO: Create a new iframe-type framesection and then add it to frameSections list
    }

    
	
}
