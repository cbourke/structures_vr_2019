using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

public class constructorController : MonoBehaviour {
	public GameObject frameGameObject;
    public GameObject areaGameObject;
    public string structureSaveFileName = "testStructure";


    StructuralElementsLists elementsListsForXML = new StructuralElementsLists();

    public LineRenderer tempLineRenderer;

    List<Frame> frameList = new List<Frame>();
    List<Area> areaList = new List<Area>();

	List<Vector3> framePoints = new List<Vector3>();
	List<Vector3> areaPoints = new List<Vector3>();


    //void Update()
    //{ //DEBUG CODE TO TEST FileToSAPApp.exe
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    saveToXML();
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    sendFileToSap();
        //}

    //}

	buildingMaterials material = buildingMaterials.Steel;

	void Awake() {
		// TODO we need a better way to set the tempLineRenderer because GameObject.FindGameobjectWithTag is very inefficient
        tempLineRenderer = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LineRenderer>();
		tempLineRenderer.enabled = false;
	}


    public void setPoint(Vector3 point, buildingObjects type) {
		// TODO we need a better way to set the tempLineRenderer because GameObject.FindGameobjectWithTag is very inefficient
        tempLineRenderer = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LineRenderer>();
        Debug.Log("Setpoint: " + point);
		if (type == buildingObjects.Frame) {
			if(framePoints.Count == 1) {
				if(point != framePoints[0]) {
					createFrame(framePoints[0], point);
					framePoints.Clear();
					tempLineRenderer.enabled = false;
				}
            }
            else {
				framePoints.Add(point);

                tempLineRenderer.enabled = true;
                tempLineRenderer.SetPosition(0, point);
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
		GameObject newFrame = Instantiate(frameGameObject, frame.getTransform().position, frame.getTransform().rotation);
		frame.SetGameObject(newFrame);
		frameList.Add(frame);
		elementsListsForXML.frameForXMLList.Add(new FrameForXML(pA, pB));
		saveToXML();

	}

    public void deleteFrame(int frameObjectID)
    {
        foreach (Frame frameElement in frameList)
        {
            GameObject frameObject = frameElement.GetGameObject();
            if (frameObject.GetInstanceID() == frameObjectID)
            {
                Vector3 deletionStartPos = frameElement.getStartPos();
                Vector3 deletionEndPos = frameElement.getEndPos();
                foreach(FrameForXML frameForXMLElement in elementsListsForXML.frameForXMLList) {
                    if (frameForXMLElement.startPos == deletionStartPos && frameForXMLElement.endPos == deletionEndPos)
                    {
                        elementsListsForXML.frameForXMLList.Remove(frameForXMLElement);
                        break;
                    }
                    
                }

                frameElement.SetGameObject(null);
                Object.Destroy(frameObject);
                frameList.Remove(frameElement);
                break;
            }
        }
        saveToXML();
    }

    void createArea(List<Vector3> points) {
		//TODO
		Debug.Log("Create Area not implimented yet!");
	}
	
	public void changeMaterial(int newMaterial) {
		material = (buildingMaterials) newMaterial;
		Debug.Log("new material: " + material);
	}
	public void changeDraw(bool change) {
		// for debugging
		Debug.Log("draw: " + change);
	}

    void saveToXML() {
        //structureSaveFileName = "testStructure";
        XmlSerializer serializer = new XmlSerializer(typeof(StructuralElementsLists));
        string filePath = Application.persistentDataPath + "/" + structureSaveFileName + ".xml";
        Debug.Log("Structure was serialized to " + filePath);
        TextWriter writer = new StreamWriter(filePath, false);
        serializer.Serialize(writer, elementsListsForXML);
        writer.Close();
    }

    void loadFromXML(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(StructuralElementsLists));
        StreamReader reader = new StreamReader(filePath);
        this.elementsListsForXML = (StructuralElementsLists)serializer.Deserialize(reader);
        reader.Close();
        Debug.Log("Structure was deserialized from = " + filePath);

        structureSaveFileName = Regex.Match(filePath, "[^<>:\"/|? *\\]*.xml$").ToString();
        structureSaveFileName = structureSaveFileName.Substring(0, structureSaveFileName.Length - 4);

        this.frameList = new List<Frame>();
        this.areaList = new List<Area>();
        this.framePoints = new List<Vector3>();
        this.areaPoints = new List<Vector3>();

        foreach (FrameForXML frameFromXML in elementsListsForXML.frameForXMLList)
        {
            Frame frame = new Frame(frameFromXML.startPos, frameFromXML.endPos, frameGameObject);
            GameObject newFrame = Instantiate(frameGameObject, frame.getTransform().position, frame.getTransform().rotation);
            frame.SetGameObject(newFrame);
            frameList.Add(frame);
        }


    }

    public void sendFileToSap() //If no filename supplied, default to that of the currently open structure
    {
        //structureSaveFileName = "testStructure";
        string filePath = Application.persistentDataPath + "/" + structureSaveFileName + ".xml";
        string appPath = Application.streamingAssetsPath + "/SapTranslator.exe";
        System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
        myProcess.StartInfo.FileName = appPath;
        myProcess.StartInfo.Arguments = "\"" + filePath + "\"";
        myProcess.Start();
    }
}
//[XmlRoot(ElementName = "StructuralElementsLists")]
[XmlRoot("StructuralElementsLists")]
public class StructuralElementsLists
{
    [XmlArray("frameListForXML"), XmlArrayItem(typeof(FrameForXML), ElementName = "FrameForXML")]
    public List<FrameForXML> frameForXMLList { get; set; }
    
    public StructuralElementsLists()
    {
        frameForXMLList = new List<FrameForXML>();
    }
    
}


[XmlType("FrameForXML")]
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
	private GameObject frameObject;

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
		trans.localScale = new Vector3(.03f, .03f, distance);

        this.frame = frame;
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




