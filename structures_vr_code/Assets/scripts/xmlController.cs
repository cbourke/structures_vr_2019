using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

public class xmlController : MonoBehaviour
{
    public string structureSaveFileName = "defaultStructure";
    public StructuralElementsLists elementsListsForXML = new StructuralElementsLists();
    public GameObject constructorController;

    // Use this for initialization
    private void Awake()
    {
        //constructorController = transform.parent.gameObject;
    }

    public void addFrameToXMLList(Vector3 pA, Vector3 pB)
    {
        elementsListsForXML.frameForXMLList.Add(new FrameForXML(pA, pB));
        saveToXML();
    }

    public void deleteFrameFromXMLList(Vector3 pA, Vector3 pB)
    {
        foreach (FrameForXML frameForXMLElement in elementsListsForXML.frameForXMLList)
        {
            if (frameForXMLElement.startPos == pA && frameForXMLElement.endPos == pB)
            {
                elementsListsForXML.frameForXMLList.Remove(frameForXMLElement);
                break;
            }

        }
        saveToXML();
    }

    public void saveToXML()
    {
        //structureSaveFileName = "testStructure";
        XmlSerializer serializer = new XmlSerializer(typeof(StructuralElementsLists));
        string filePath = Application.persistentDataPath + "/" + structureSaveFileName + ".xml";
        Debug.Log("Structure was serialized to " + filePath);
        TextWriter writer = new StreamWriter(filePath, false);
        serializer.Serialize(writer, elementsListsForXML);
        writer.Close();
    }


    public void loadFromXML(string filePath)
    {
        saveToXML();
        XmlSerializer serializer = new XmlSerializer(typeof(StructuralElementsLists));
        StreamReader reader = new StreamReader(filePath);
        this.elementsListsForXML = (StructuralElementsLists)serializer.Deserialize(reader);
        reader.Close();
        Debug.Log("Structure was deserialized from = " + filePath);

        structureSaveFileName = Regex.Match(filePath, "[^<>:\"/|? *\\]*.xml$").ToString();
        structureSaveFileName = structureSaveFileName.Substring(0, structureSaveFileName.Length - 4);

        constructorController.GetComponent<constructorController>().constructFromXmlElementsLists(elementsListsForXML);
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