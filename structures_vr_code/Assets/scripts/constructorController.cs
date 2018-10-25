using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class constructorController : MonoBehaviour {

	public GameObject frameGameObject;
    public GameObject areaGameObject;
    public string structureSaveFileName = "testStructure";

    StructuralElementsLists elementsListsForXML = new StructuralElementsLists();
    List<Frame> frameList = new List<Frame>();
    List<Area> areaList = new List<Area>();

	List<Vector3> framePoints = new List<Vector3>();
	List<Vector3> areaPoints = new List<Vector3>();

    void Update()
    { //DEBUG CODE TO TEST FileToSAPApp.exe
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            saveToXML();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sendFileToSap();
        }

    }

    public void setPoint(Vector3 point, buildingObjects type) {
		Debug.Log("Setpoint: " + point);
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
		Debug.Log("FrameponitsS: " + pA + " 2: " + pB);
		Frame frame = new Frame(pA, pB, frameGameObject);
        frameList.Add(frame);
        elementsListsForXML.frameForXMLList.Add(new FrameForXML(pA, pB));
		Instantiate(frameGameObject, frame.getTransform().position, frame.getTransform().rotation);
        saveToXML();

    }

	void createArea(List<Vector3> points) {
		//TODO
		Debug.Log("Create Area not implimented yet!");
	}

    void saveToXML() {
        //structureSaveFileName = "testStructure";
        XmlSerializer serializer = new XmlSerializer(typeof(StructuralElementsLists));
        string filepath = Application.persistentDataPath + "/newTest.xml";
        Debug.Log(filepath);
        TextWriter writer = new StreamWriter(filepath, false);
        serializer.Serialize(writer, elementsListsForXML);
        writer.Close();
    }


    public void sendFileToSap() //If no filename supplied, default to that of the currently open structure
    {
        //structureSaveFileName = "testStructure";
        string filePath = Application.persistentDataPath + "/newTest.xml";
        string appPath = Application.streamingAssetsPath + "/FileToSAPApp.exe";
        System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
        myProcess.StartInfo.FileName = appPath;
        myProcess.StartInfo.Arguments = filePath;
        myProcess.Start();
    }
}
[XmlRoot(ElementName = "StructuralElementsLists")]
public class StructuralElementsLists
{
    [XmlArray("frameListForXML"), XmlArrayItem(typeof(FrameForXML), ElementName = "FrameForXML")]
    public List<FrameForXML> frameForXMLList = new List<FrameForXML>();
}

[XmlRoot("StructuralElementsLists")]
public class FrameForXML
{
    public Vector3 startPos { get; set; }
    public Vector3 endPos { get; set; }

    public FrameForXML()
    {

    }
    public FrameForXML(Vector3 pointA, Vector3 pointB)
    {
        startPos = pointA;
        endPos = pointB;
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

public class Area { }




