using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour {

	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 startPosRelease;
	private Vector3 endPosRelease;
	private Vector3 direction;
	private Vector3 angle;

	private GameObject frameObject;
    private string sectionPropertyName;
	private List<string> groupNames;
	private bool isSelected = false;


	private Transform trans;

	public Frame()
	{

	}

	public Frame(Vector3 start, Vector3 end, GameObject framePrefab, FrameSection section)
    {
        sectionPropertyName = section.GetName();
		frameObject = Instantiate(framePrefab);

		startPos = start;
		endPos = end;
		
		Vector3 between = end - start;
		float distance = between.magnitude;
		Debug.Log("distance: " + distance);
		Vector3 midPoint = Vector3.Lerp(start, end, 0.5f);

		float dist = (distance / gridController.getSpacing());
		Debug.Log("DIST: " + dist);
		startPosRelease = Vector3.Lerp(start, end, gridController.getSpacing());

		trans = frameObject.transform;
    	trans.position = midPoint;
		trans.LookAt(end);
        trans.rotation *= Quaternion.Euler(90, 90, 90);
		trans.localScale = new Vector3(1, distance, 1);

		// scale the frame depending on the section type
		if(section.type == FrameSectionType.I)
        {
			frame_iBeamController controller = frameObject.GetComponent<frame_iBeamController>();
			float[] arr = section.GetDimensions();
			controller.SetDimensions(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        } else if(section.type == FrameSectionType.Pipe)
        {
			frame_PipeController controller = frameObject.GetComponent<frame_PipeController>();
			float[] arr = section.GetDimensions();
			controller.SetDimensions(arr[0], arr[1]);
        } else if(section.type == FrameSectionType.Tube)
        {
			frame_TubeController controller = frameObject.GetComponent<frame_TubeController>();
			float[] arr = section.GetDimensions();
			controller.SetDimensions(arr[0], arr[1], arr[2], arr[3]);
        } else {
            Debug.LogError("Invalid frame section type passed to createFrame in Frame Class");
        }
	}

	public Transform getTransform()
    {
		return trans;
	}
	public Vector3 getDirection()
    {
		return direction;
	}
	public Vector3 getStartPos()
    {
		return startPos;
	}
	public Vector3 getEndPos()
    {
		return endPos;
	}
	public GameObject GetGameObject()
    {
		return frameObject;
	}
    public string getSectionPropertyName()
    {
        return sectionPropertyName;
    }

    public void setSectionProperty(FrameSection section)
    {
        sectionPropertyName = section.GetName();
    }

	public List<string> getGroupNames()
    {
        return groupNames;
    }

    public void addGroupName(string newGroupName)
    {
        groupNames.Add(newGroupName);
    }

	public void removeGroupName(string targetGroupName){
		groupNames.Remove(targetGroupName);
	}

	public bool getSelected(){
		return isSelected;
	}

	public void setSelected(bool selected) {
		isSelected = selected;
	}
}

