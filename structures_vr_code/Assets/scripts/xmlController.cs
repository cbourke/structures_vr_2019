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

    public void addBuildingMaterialToXMLList(BuildingMaterial buildingMaterial)
    {
        elementsListsForXML.buildingMaterialForXMLList.Add(buildingMaterial);
        saveToXML();
    }

    public void deletebuildingMaterialFromXMLList(BuildingMaterial buildingMaterial)
    {
        foreach (BuildingMaterial existingbuildingMaterial in elementsListsForXML.buildingMaterialForXMLList)
        {
            if (existingbuildingMaterial.GetName() == buildingMaterial.GetName())
            {
                elementsListsForXML.buildingMaterialForXMLList.Remove(buildingMaterial);
                break;
            }

        }
        saveToXML();
    }

    public void addFrameSectionToXMLList(FrameSection frameSection)
    {
        elementsListsForXML.frameSectionForXMLList.Add(frameSection);
        saveToXML();
    }

    public void deleteFrameFromXMLList(FrameSection frameSection)
    {
        foreach (FrameSection existingFrameSection in elementsListsForXML.frameSectionForXMLList)
        {
            if (existingFrameSection.GetName() == frameSection.GetName())
            {
                elementsListsForXML.frameSectionForXMLList.Remove(existingFrameSection);
                break;
            }

        }
        saveToXML();
    }

    public void addFrameToXMLList(Vector3 pA, Vector3 pB, string sectionName)
    {
        elementsListsForXML.frameForXMLList.Add(new FrameForXML(pA, pB, sectionName));
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

    public void addJointRestraintToXMLList(Vector3 pos, bool tX, bool tY, bool tZ, bool rX, bool rY, bool rZ)
    {
        elementsListsForXML.jointRestraintForXMLList.Add(new jointRestraintForXML(pos, tX, tY, tZ, rX, rY, rZ));
        saveToXML();
    }

    public void deleteJointRestraintFromXMLList(Vector3 pos)
    {
        foreach (jointRestraintForXML jointRestraintForXMLElement in elementsListsForXML.jointRestraintForXMLList)
        {
            if (jointRestraintForXMLElement.position == pos)
            {
                elementsListsForXML.jointRestraintForXMLList.Remove(jointRestraintForXMLElement);
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
    [XmlArray("buildingMaterialForXMLArray"), XmlArrayItem(typeof(FrameForXML), ElementName = "FrameForXML")]
    public List<BuildingMaterial> buildingMaterialForXMLList { get; set; }

    [XmlArray("frameSectionForXMLArray"), XmlArrayItem(typeof(FrameForXML), ElementName = "FrameForXML")]
    public List<FrameSection> frameSectionForXMLList { get; set; }

    [XmlArray("frameForXMLArray"), XmlArrayItem(typeof(FrameForXML), ElementName = "FrameForXML")]
    public List<FrameForXML> frameForXMLList { get; set; }

    [XmlArray("jointRestraintForXMLArray"), XmlArrayItem(typeof(jointRestraintForXML), ElementName = "jointRestraintForXML")]
    public List<jointRestraintForXML> jointRestraintForXMLList { get; set; }

    public StructuralElementsLists()
    {
        frameForXMLList = new List<FrameForXML>();
        jointRestraintForXMLList = new List<jointRestraintForXML>();
    }
}

[XmlType("FrameForXML")]
public class FrameForXML
{
    public Vector3 startPos { get; set; }
    public Vector3 endPos { get; set; }
    public string sectionPropertyName { get; set; }

    public FrameForXML()
    {
        // Empty constructor needed for XML serialization
    }
    public FrameForXML(Vector3 pointA, Vector3 pointB, string sectionName)
    {
        startPos = pointA;
        endPos = pointB;
    }
}

[XmlType("jointRestraintForXML")]
public class jointRestraintForXML
{
    public Vector3 position { get; set; }
    public bool transX { get; set; }
    public bool transY { get; set; }
    public bool transZ { get; set; }
    public bool rotX { get; set; }
    public bool rotY { get; set; }
    public bool rotZ { get; set; }

    public jointRestraintForXML()
    {
        // Empty constructor needed for XML serialization
    }

    public jointRestraintForXML(Vector3 pos, bool tX, bool tY, bool tZ, bool rX, bool rY, bool rZ)
    {
        position = pos;
        transX = tX;
        transY = tY;
        transZ = tZ;
        rotX = rX;
        rotY = rY;
        rotZ = rZ;
    }
}