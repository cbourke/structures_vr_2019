using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour {
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
	private frameRelease release;

	private float releaseEndPercentage = 0.1f;

	private Transform trans;
	private Transform frameTrans;
	private Transform releaseStartTrans;
	private Transform releaseEndTrans;
    
	private highlighter frameHighlighter;


	public Frame()
	{

	}

	public Frame(Vector3 start, Vector3 end, GameObject framePrefab, FrameSection section, string frameName)
    {

        sectionPropertyName = section.GetName();
		frameObject = Instantiate(framePrefab);

		trans = frameObject.transform;
		frameTrans = trans.Find("frame");
		releaseStartTrans = trans.Find("startRelease");
		releaseEndTrans = trans.Find("endRelease");
		
		startPos = start;
		endPos = end;
		
		release = new frameRelease();
		setRelease();

        frameObject.GetComponent<frameReference>().setMyFrame(this);
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

	public void setReleaseStart()
	{
		releaseStartTrans.gameObject.SetActive(true);
		releaseEndTrans.gameObject.SetActive(false);
		Vector3 between = endPos - startPos;
		float distance = between.magnitude;

		float releasePercent = (gridController.getSpacing()*(releaseEndPercentage))/(distance);

		startPosRelease = Vector3.Lerp(startPos, endPos, releasePercent);
		scaleFrame(startPosRelease, endPos);

	}

	public void setReleaseEnd()
	{
		releaseStartTrans.gameObject.SetActive(false);
		releaseEndTrans.gameObject.SetActive(true);
		Vector3 between = endPos - startPos;
		float distance = between.magnitude;

		float releasePercent = (gridController.getSpacing()*(releaseEndPercentage))/(distance);

		endPosRelease = Vector3.Lerp(endPos, startPos, releasePercent);
		scaleFrame(startPos, endPosRelease);

	}

	public void setReleaseBoth()
	{
		releaseStartTrans.gameObject.SetActive(true);
		releaseEndTrans.gameObject.SetActive(true);

		Vector3 between = endPos - startPos;
		float distance = between.magnitude;

		float releasePercent = (gridController.getSpacing()*(releaseEndPercentage))/(distance);

		startPosRelease = Vector3.Lerp(startPos, endPos, releasePercent);
		endPosRelease = Vector3.Lerp(endPos, startPos, releasePercent);
		scaleFrame(startPosRelease, endPosRelease);
	}

	public void setReleaseNeither() {
		releaseStartTrans.gameObject.SetActive(false);
		releaseEndTrans.gameObject.SetActive(false);
		scaleFrame(startPos, endPos);
	}

	private void scaleFrame(Vector3 startPoint, Vector3 endPoint)
	{
		Vector3 between = endPoint - startPoint;
		float distance = between.magnitude;

		trans = frameObject.transform;
		trans.position = startPoint;
		trans.LookAt(endPoint);
		trans.rotation *= Quaternion.Euler(90, 90, 90);
		frameTrans.localScale = new Vector3(1, distance, 1);

		float releaseDist = 0.0375f;
		releaseEndTrans.localPosition = new Vector3(0, distance+releaseDist, 0);
		releaseStartTrans.localPosition = new Vector3(0, -releaseDist, 0);
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

    private void highlightObject() {
		Debug.Log("isSelected: " + isSelected);
        if(isSelected) {
            frameHighlighter.Highlight();
        } else {
            frameHighlighter.Unhighlight();
        }
    }
}

