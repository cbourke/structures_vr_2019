﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class defines a frame and its various properties */
/* Also used for highlighting frames */
public class Frame {
    private string name;
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
	private FrameRelease release;

	private float releaseEndPercentage = 0.1f;
    private float defaultReleaseSize = 0.05f;

	private Transform trans;
	private Transform frameTrans;
	private Transform releaseStartTrans;
	private Transform releaseEndTrans;
    
	private highlighter frameHighlighter;

    /// <summary>
    /// Empty constructor
    /// </summary>
	public Frame()
	{

	}

    /// <summary>
    /// Main constructor for defining a frame object
	/// Takes in the frames endpoints, the correct frame prefab for the section, the frame section, and the frames name
    /// </summary>
	public Frame(Vector3 start, Vector3 end, GameObject framePrefab, FrameSection section, string frameName)
    {

        sectionPropertyName = section.GetName();
		frameObject = MonoBehaviour.Instantiate(framePrefab);

		trans = frameObject.transform;
		frameTrans = trans.Find("frame");
		releaseStartTrans = trans.Find("startRelease");
		releaseEndTrans = trans.Find("endRelease");
		
		startPos = start;
		endPos = end;
		
		release = new FrameRelease();
		setRelease();

        frameObject.GetComponent<FrameBehavior>().setMyFrame(this);
		frameHighlighter = frameObject.GetComponent<highlighter>();
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

		setName(frameName);
	}

	
    /// <summary>
    /// Sets the frames visual appearence based on its release state
    /// </summary>
	public void setRelease()
	{
		if(release.isReleaseStart() && release.isReleaseEnd()) {
			setReleaseBoth();
		} else if(release.isReleaseStart()) {
			setReleaseStart();
		} else if(release.isReleaseEnd()) {
			setReleaseEnd();
		} else {
			setReleaseNeither();
		}
	}

    /// <summary>
    /// Sets the frames visual appearence to be released at the start point
    /// </summary>
	public void setReleaseStart()
	{
		releaseStartTrans.gameObject.SetActive(true);
		releaseEndTrans.gameObject.SetActive(false);
		Vector3 between = endPos - startPos;
		float distance = between.magnitude;

		float releasePercent = (gridController.getSpacing()*(releaseEndPercentage))/(distance);

		startPosRelease = Vector3.Lerp(startPos, endPos, releasePercent);
		scaleFrame(startPosRelease, endPos, distance);

	}

    /// <summary>
    /// Sets the frames visual appearence to be released at the end point
    /// </summary>
	public void setReleaseEnd()
	{
		releaseStartTrans.gameObject.SetActive(false);
		releaseEndTrans.gameObject.SetActive(true);
		Vector3 between = endPos - startPos;
		float distance = between.magnitude;

		float releasePercent = (gridController.getSpacing()*(releaseEndPercentage))/(distance);

		endPosRelease = Vector3.Lerp(endPos, startPos, releasePercent);
		scaleFrame(startPos, endPosRelease, distance);

	}

    /// <summary>
    /// Sets the frames visual appearence to be released at both the start and end point
    /// </summary>
	public void setReleaseBoth()
	{
		releaseStartTrans.gameObject.SetActive(true);
		releaseEndTrans.gameObject.SetActive(true);

		Vector3 between = endPos - startPos;
		float distance = between.magnitude;

		float releasePercent = (gridController.getSpacing()*(releaseEndPercentage))/(distance);
		startPosRelease = Vector3.Lerp(startPos, endPos, releasePercent);
		endPosRelease = Vector3.Lerp(endPos, startPos, releasePercent);
		scaleFrame(startPosRelease, endPosRelease, distance);
	}

    /// <summary>
    /// Sets the frames visual appearence to not be released at either point
    /// </summary>
	public void setReleaseNeither() {
		releaseStartTrans.gameObject.SetActive(false);
		releaseEndTrans.gameObject.SetActive(false);
		Vector3 between = endPos - startPos;
		float distance = between.magnitude;
		scaleFrame(startPos, endPos, distance);
	}

    /// <summary>
    /// This function actually scales the frame according to the release state
	/// should only be called by one of the setRelease functions in this class
    /// </summary>
	private void scaleFrame(Vector3 startPoint, Vector3 endPoint, float length)
	{
		Vector3 between = endPoint - startPoint;
		float distance = between.magnitude;

		trans = frameObject.transform;
		trans.position = startPoint;
		trans.LookAt(endPoint);
		trans.rotation *= Quaternion.Euler(90, 90, 90);
		frameTrans.localScale = new Vector3(1, distance, 1);

		float releaseDist = 0.0375f;
		float releaseDistNew = (length - distance)/5f;
		releaseEndTrans.localPosition = new Vector3(0, distance+releaseDistNew, 0);
		releaseStartTrans.localPosition = new Vector3(0, -releaseDistNew, 0);
		
		float scaleSize = defaultReleaseSize * gridController.getSpacing();
		releaseStartTrans.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
		releaseEndTrans.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
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

    /// <summary>
    /// Returns the selection state of the frame
    /// </summary>
	public bool getSelected(){
		return isSelected;
	}

    /// <summary>
    /// Sets whether the frame is selected or not
    /// </summary>
	public void setSelected(bool selected) {
		isSelected = selected;
		highlightObject();
	}

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    /// <summary>
    /// Highlights the frame based on the selection status
    /// </summary>
    private void highlightObject() {
        if(isSelected) {
            frameHighlighter.Highlight();
        } else {
            frameHighlighter.Unhighlight();
        }
    }

    public double getLength()
    {
        return Vector3.Distance(startPos, endPos);
    }
}

