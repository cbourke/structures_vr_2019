using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This class defines a joint restraint */
public class jointRestraint {

    Vector3 position;
    bool transX;
    bool transY;
    bool transZ;
    bool rotX;
    bool rotY;
    bool rotZ;

    GameObject jointRestraintGameObject;


    /// <summary>
    /// Defines a joint restraint
    /// Uses the GameObject passed in to spawn a new restraint
    /// </summary>
    public jointRestraint(Vector3 position, char type, GameObject jointRestraintPrefab)
    {
        this.position = position;
        jointRestraintGameObject = GameObject.Instantiate(jointRestraintPrefab, position, new Quaternion());
        setType(type);
    }

    /// <summary>
    /// Sets the joint restraints type
    /// Available types are: roller, pin, fixed
    /// </summary>
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
