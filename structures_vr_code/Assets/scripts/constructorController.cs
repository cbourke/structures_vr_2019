﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constructorController : MonoBehaviour {

	public GameObject frameGameObject;
    public GameObject areaGameObject;


	List<Frame> frameList = new List<Frame>();
    List<Area> areaList = new List<Area>();

	List<Vector3> framePoints = new List<Vector3>();
	List<Vector3> areaPoints = new List<Vector3>();
	

    public void setPoint(Vector3 point, buildingObjects type)
    {
		if (type == buildingObjects.Frame) {
			if(framePoints.Count == 1) {

				createFrame(framePoints[0], point);
				framePoints.Clear();
			} else {
				framePoints.Add(point);
			}
		} else if (type == buildingObjects.Area) {
			if(framePoints[0] == point) {
				createArea(areaPoints);
				areaPoints.Clear();
			} else {
				areaPoints.Add(point);
			}
		}
    }
	
	void createFrame(Vector3 pA, Vector3 pB) {
		Frame frame = new Frame(pA, pB, frameGameObject);
		frameList.Add(frame);
		Instantiate(frameGameObject, frame.getTransform().position, frame.getTransform().rotation);
	}

	void createArea(List<Vector3> points) {
		//TODO
		Debug.Log("Create Area not implimented yet!");
	}
}

public class Frame { 
	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 direction;
	private float length;
	private Vector3 angle;
	private GameObject frame;
	private Transform trans;

	public Frame(Vector3 start, Vector3 end, GameObject frame) {
		startPos = start;
		endPos = end;

		Vector3 between = end - start;
    	float distance = between.magnitude;
		length = distance;

		Vector3 angletest = new Vector3(Vector3.Angle(between, frame.transform.right), Vector3.Angle(between, frame.transform.up), Vector3.Angle(between, frame.transform.forward));

		trans = frame.transform;

    	trans.position = start + (between / 2.0f);
		trans.Rotate(angletest);
    	trans.LookAt(end);
		trans.localScale = new Vector3(.05f, .05f, distance);
	}

	public Transform getTransform() {
		return trans;
	}
	public Vector3 getDirection() {
		return direction;
	}
	public Vector3 getStartPos() {
		return startPos;
	}
	public Vector3 getEndPos() {
		return endPos;
	}
}

public class Area {}
