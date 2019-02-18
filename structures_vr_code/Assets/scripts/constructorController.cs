using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

public class constructorController : MonoBehaviour
{
    public sectionController mySectionController;
    public xmlController myXmlController;
    public LineRenderer tempLineRenderer;
    
    public GameObject framePrefabIBeam;
    public GameObject framePrefabTube;
    public GameObject framePrefabPipe;
    
    private GameObject areaPrefab;
    public GameObject jointRestraintPrefab;


    public string structureSaveFileName = "testStructure";

    List<Frame> frameList = new List<Frame>();
    List<Area> areaList = new List<Area>();
    List<jointRestraint> jointRestraintList = new List<jointRestraint>();

    List<Vector3> framePoints = new List<Vector3>();
    List<Vector3> areaPoints = new List<Vector3>();

    public void setPoint(Vector3 point, buildingObjects type)
    {
        if (type == buildingObjects.Frame)
        {
            if (framePoints.Count == 1)
            {
                if (point != framePoints[0])
                {
                    createFrame(framePoints[0], point);
                    framePoints.Clear();
                    tempLineRenderer.enabled = false;
                } else
                {
                    framePoints.Clear();
                    tempLineRenderer.enabled = false; 
                }
            }
            else
            {
                framePoints.Add(point);

                tempLineRenderer.enabled = true;
                tempLineRenderer.SetPosition(0, point);
                tempLineRenderer.SetPosition(1, point);
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
        FrameSectionType type = mySectionController.GetCurrentFrameSection().type;
        Frame frame = new Frame();
        if(type == FrameSectionType.I)
        {
            frame = new Frame(pA, pB, framePrefabIBeam, mySectionController.GetCurrentFrameSection());
        } else if(type == FrameSectionType.Pipe)
        {
            frame = new Frame(pA, pB, framePrefabPipe, mySectionController.GetCurrentFrameSection());
        } else if(type == FrameSectionType.Tube)
        {
            frame = new Frame(pA, pB, framePrefabTube, mySectionController.GetCurrentFrameSection());
        } else {
            Debug.LogError("Invalid frame section type passed to createFrame in constructorController");
        }
        frameList.Add(frame);
        myXmlController.GetComponent<xmlController>().addFrameToXMLList(pA, pB, mySectionController.GetCurrentFrameSection().GetName());

    }

    public void deleteFrame(int frameObjectID)
    {
        foreach (Frame frameElement in frameList)
        {
            GameObject frameObject = frameElement.GetGameObject();
            if (frameObject.GetInstanceID() == frameObjectID)
            {
                //Deletion code
                Vector3 pA = frameElement.getStartPos();
                Vector3 pB = frameElement.getEndPos();





                myXmlController.GetComponent<xmlController>().deleteFrameFromXMLList(pA, pB);

                Object.Destroy(frameObject);
                frameList.Remove(frameElement);


                // Delete any orphaned joint restraints
                bool deleteJointRestraintAtA = true;
                bool deleteJointRestraintAtB = true;
                foreach (Frame f in frameList)
                {
                    if (deleteJointRestraintAtA && (f.getStartPos() == pA || f.getEndPos() == pA)) //If any frame remaining still ends on pA
                    {
                        deleteJointRestraintAtA = false; //Protect the joint restraint at pA, if it exists
                    }

                    if (deleteJointRestraintAtB && (f.getStartPos() == pB || f.getEndPos() == pB)) //If any frame remaining still ends on pB
                    {
                        deleteJointRestraintAtB = false; //Protect the joint restraint at pB, if it exists
                    }

                    if (!deleteJointRestraintAtA && !deleteJointRestraintAtB)
                    {
                        break; //If we protected both points then don't waste more time looping
                    }
                }

                if (deleteJointRestraintAtA)
                {
                    deleteJointRestraint(pA);
                }

                if (deleteJointRestraintAtB)
                {
                    deleteJointRestraint(pB);
                }


                break;
            }
        }
    }


    public void createJointRestraint(Vector3 position, char type)
    {
        deleteJointRestraint(position); // Overwrite any joint restraint already at the target point

        //Check to make sure a frame endpoint is at "position"
        bool frameEndPoint = false;
        foreach (Frame f in frameList)
        {
            if ((f.getStartPos() == position || f.getEndPos() == position)) //If any frame remaining still ends on pA
            {
                frameEndPoint = true; //Protect the joint restraint at pA, if it exists
                break;
            }
        }

        if (frameEndPoint) // only create a joint restraint if the target position is an endpoint of at least one frame
        {
            jointRestraint newRestraint = new jointRestraint(position, type, jointRestraintPrefab);
            jointRestraintList.Add(newRestraint);
            myXmlController.GetComponent<xmlController>().addJointRestraintToXMLList(newRestraint.GetPosition(), newRestraint.GetTransX(), newRestraint.GetTransY(), newRestraint.GetTransZ(), newRestraint.GetRotX(), newRestraint.GetRotY(), newRestraint.GetRotZ());
        }
    }

    public void deleteJointRestraint(int jointRestraintObjectID)
    {
        foreach (jointRestraint jointRestraintElement in jointRestraintList)
        {
            GameObject jointRestraintObject = jointRestraintElement.GetGameObject();
            if (jointRestraintObject.GetInstanceID() == jointRestraintObjectID)
            {
                Vector3 position = jointRestraintElement.GetPosition();
                myXmlController.GetComponent<xmlController>().deleteJointRestraintFromXMLList(position);

                jointRestraintElement.SetGameObject(null);
                Object.Destroy(jointRestraintObject);
                jointRestraintList.Remove(jointRestraintElement);
                break;
            }
        }
    }

    public void deleteJointRestraint(Vector3 targetPosition)
    {
        foreach (jointRestraint jointRestraintElement in jointRestraintList)
        {
            GameObject jointRestraintObject = jointRestraintElement.GetGameObject();
            Vector3 checkPosition = jointRestraintElement.GetPosition();
            if (checkPosition == targetPosition)
            {
                myXmlController.GetComponent<xmlController>().deleteJointRestraintFromXMLList(targetPosition);

                jointRestraintElement.SetGameObject(null);
                Object.Destroy(jointRestraintObject);
                jointRestraintList.Remove(jointRestraintElement);
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
            Object.Destroy(frameObject);
        }
        frameList.Clear();
        foreach (jointRestraint jointRestraintElement in jointRestraintList)
        {
            GameObject jointRestraintObject = jointRestraintElement.GetGameObject();
            jointRestraintElement.SetGameObject(null);
            Object.Destroy(jointRestraintObject);
            jointRestraintList.Remove(jointRestraintElement);
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
        //TODO: This function will be replaced entirely soon
        Debug.Log("Change Material not implimented!");
    }
    public void changeDraw(bool change)
    {
        // for debugging
        Debug.Log("draw: " + change);
    }
}

public class Area { }

public class jointRestraint {

    Vector3 position;
    bool transX;
    bool transY;
    bool transZ;
    bool rotX;
    bool rotY;
    bool rotZ;

    GameObject jointRestraintGameObject;


    public jointRestraint(Vector3 position, char type, GameObject jointRestraintPrefab)
    {
        this.position = position;
        jointRestraintGameObject = GameObject.Instantiate(jointRestraintPrefab, position, new Quaternion());
        setType(type);
    }

    public void setType(char type)
    {
        switch (type)
        {
            case 'r': //roller restraint
                {
                    jointRestraintGameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(1, 1, 1);
                    jointRestraintGameObject.transform.GetChild(1).gameObject.transform.localScale = new Vector3(0, 0, 0);
                    jointRestraintGameObject.transform.GetChild(2).gameObject.transform.localScale = new Vector3(0, 0, 0);

                    
                    transX = true;
                    transY = false;
                    transZ = true;
                    rotX = true;
                    rotY = true;
                    rotZ = true;
                    break;
                }
            case 'p': //pin restraint
                {
                    jointRestraintGameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(0, 0, 0);
                    jointRestraintGameObject.transform.GetChild(1).gameObject.transform.localScale = new Vector3(1, 1, 1);
                    jointRestraintGameObject.transform.GetChild(2).gameObject.transform.localScale = new Vector3(0, 0, 0);

                    transX = false;
                    transY = false;
                    transZ = false;
                    rotX = true;
                    rotY = true;
                    rotZ = true;
                    break;
                }
            default: //default to fixed restraint
                {
                    jointRestraintGameObject.transform.GetChild(0).gameObject.transform.localScale = new Vector3(0, 0, 0);
                    jointRestraintGameObject.transform.GetChild(1).gameObject.transform.localScale = new Vector3(0, 0, 0);
                    jointRestraintGameObject.transform.GetChild(2).gameObject.transform.localScale = new Vector3(1, 1, 1);

                    transX = false;
                    transY = false;
                    transZ = false;
                    rotX = false;
                    rotY = false;
                    rotZ = false;
                    break;
                }
        }
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public bool GetTransX()
    {
        return transX;
    }

    public bool GetTransY()
    {
        return transY;
    }

    public bool GetTransZ()
    {
        return transZ;
    }

    public bool GetRotX()
    {
        return rotX;
    }

    public bool GetRotY()
    {
        return rotY;
    }

    public bool GetRotZ()
    {
        return rotZ;
    }

    public GameObject GetGameObject()
    {
        return jointRestraintGameObject;
    }

    public void SetGameObject(GameObject newObject)
    {
        jointRestraintGameObject = newObject;
    }
}


