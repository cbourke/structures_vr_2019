﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

public class constructorController : MonoBehaviour
{
    public GameObject myXmlController;
    public GameObject framePrefab;
    public GameObject areaPrefab;
    public string structureSaveFileName = "testStructure";


    //StructuralElementsLists elementsListsForXML = new StructuralElementsLists();

    public LineRenderer tempLineRenderer;

    List<Frame> frameList = new List<Frame>();
    List<Area> areaList = new List<Area>();

    List<Vector3> framePoints = new List<Vector3>();
    List<Vector3> areaPoints = new List<Vector3>();


    buildingMaterials material = buildingMaterials.Steel;

    void Awake()
    {
        // TODO we need a better way to set the tempLineRenderer because GameObject.FindGameobjectWithTag is very inefficient
        tempLineRenderer = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LineRenderer>();
        tempLineRenderer.enabled = false;
    }


    public void setPoint(Vector3 point, buildingObjects type)
    {
        // TODO we need a better way to set the tempLineRenderer because GameObject.FindGameobjectWithTag is very inefficient
        tempLineRenderer = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LineRenderer>();
        Debug.Log("Setpoint: " + point);
        if (type == buildingObjects.Frame)
        {
            if (framePoints.Count == 1)
            {
                if (point != framePoints[0])
                {
                    createFrame(framePoints[0], point);
                    framePoints.Clear();
                    tempLineRenderer.enabled = false;
                }
            }
            else
            {
                framePoints.Add(point);

                tempLineRenderer.enabled = true;
                tempLineRenderer.SetPosition(0, point);
            }
        }
        else if (type == buildingObjects.Area)
        {
            if (framePoints[0] == point)
            {
                createArea(areaPoints);
                areaPoints.Clear();
            }
            else
            {
                areaPoints.Add(point);
            }
        }
    }

    void createFrame(Vector3 pA, Vector3 pB)
    {
        Frame frame = new Frame(pA, pB, framePrefab);
        GameObject newFrame = Instantiate(framePrefab, frame.getTransform().position, frame.getTransform().rotation);
        frame.SetGameObject(newFrame);
        frameList.Add(frame);
        myXmlController.GetComponent<xmlController>().addFrameToXMLList(pA, pB);

    }

    public void deleteFrame(int frameObjectID)
    {
        foreach (Frame frameElement in frameList)
        {
            GameObject frameObject = frameElement.GetGameObject();
            if (frameObject.GetInstanceID() == frameObjectID)
            {
                Vector3 pA = frameElement.getStartPos();
                Vector3 pB = frameElement.getEndPos();
                myXmlController.GetComponent<xmlController>().deleteFrameFromXMLList(pA, pB);

                frameElement.SetGameObject(null);
                Object.Destroy(frameObject);
                frameList.Remove(frameElement);
                break;
            }
        }
    }

    public void deleteAll()
    {
        framePoints.Clear();
        tempLineRenderer.enabled = false;

        foreach (Frame frameElement in frameList)
        {
            GameObject frameObject = frameElement.GetGameObject();
            frameElement.SetGameObject(null);
            Object.Destroy(frameObject);
            frameList.Remove(frameElement);
        }
    }

    public void constructFromXmlElementsLists(StructuralElementsLists elementsListsForXML)
    {
        deleteAll();
        foreach (FrameForXML frameForXML in elementsListsForXML.frameForXMLList)
        {
            Vector3 pA = frameForXML.startPos;
            Vector3 pB = frameForXML.endPos;
            createFrame(pA, pB);
        }
    }

    void createArea(List<Vector3> points)
    {
        //TODO
        Debug.Log("Create Area not implimented yet!");
    }

    public void changeMaterial(int newMaterial)
    {
        material = (buildingMaterials)newMaterial;
        Debug.Log("new material: " + material);
    }
    public void changeDraw(bool change)
    {
        // for debugging
        Debug.Log("draw: " + change);
    }
}


public class Frame { 
	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 direction;
	private float length;
	private Vector3 angle;

	private GameObject framePrefab;
	private GameObject frameObject;

	private Transform trans;

	public Frame(Vector3 start, Vector3 end, GameObject framePrefab) {
		startPos = start;
		endPos = end;
		
		Vector3 between = end - start;
    	float distance = between.magnitude;
		length = distance;

		Vector3 angletest = new Vector3(Vector3.Angle(between, framePrefab.transform.right), Vector3.Angle(between, framePrefab.transform.up), Vector3.Angle(between, framePrefab.transform.forward));

		trans = framePrefab.transform;

    	trans.position = start + (between / 2.0f);
		trans.Rotate(angletest);
    	trans.LookAt(end);
		trans.localScale = new Vector3(.03f, .03f, distance);

        this.framePrefab = framePrefab;
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

	public GameObject GetGameObject() {
		return frameObject;
	}
	public void SetGameObject(GameObject obj) {
		frameObject = obj;
	}

}

public class Area { }




