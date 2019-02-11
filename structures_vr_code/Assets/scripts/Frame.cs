using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour {

	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 direction;
	private Vector3 angle;

	private GameObject frameObject;
    private string sectionPropertyName;
	private List<string> groupNames;
	private bool isSelected = false;


	private Transform trans;

	public Frame(Vector3 start, Vector3 end, GameObject framePrefab, FrameSection section)
    {
        sectionPropertyName = section.GetName();
		frameObject = Instantiate(framePrefab);

		startPos = start;
		endPos = end;
		
		Vector3 between = end - start;
    	float distance = between.magnitude;

		Vector3 angletest = new Vector3(Vector3.Angle(between, Vector3.right), Vector3.Angle(between, Vector3.up), Vector3.Angle(between, Vector3.forward));

		trans = frameObject.transform;
    	trans.position = start + (between / 2.0f);
		trans.Rotate(angletest);
    	trans.LookAt(end);
		trans.localScale = new Vector3(.03f, .03f, distance);
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

