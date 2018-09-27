using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constructorController : MonoBehaviour {

	public GameObject beamGameObject;
    //public GameObject floorGameObject;
    //public GameObject wallGameObject;

    Vector3 p1;
    Vector3 p2;
    bool hasPoint = false;
	List<Beam> beamList = new List<Beam>();
    //List<Floor> floorList = new List<Floor>();
    //List<Wall> wallList = new List<Wall>();

    static GameObject Grid;
    generateGrid gridScript;

    // Use this for initialization
    void Start () {
        Grid = GameObject.Find("Grid"); ;
        gridScript = Grid.GetComponent<generateGrid>();
        
        /*
		TESETING CODE
		Vector3 origin = new Vector3(0,0,0);
		Vector3 p1 = new Vector3(0,0,1);
		Vector3 p2 = new Vector3(2,2,2);
		Vector3 p3 = new Vector3(1,1,1);
		Vector3 p4 = new Vector3(1,0,1);
		Vector3 p5 = new Vector3(3,0,1);
		Vector3 p6 = new Vector3(0,2,1);
		
		createBeam(origin, p1);
		createBeam(origin, p3);
		createBeam(origin, p4);
		createBeam(origin, p5);
		createBeam(origin, p6);
		 */
    }

    public void setPoint(Vector3 point, buildingObjects type)
    {
        if (generateGrid.grid[(int)point.x, (int)point.y, (int)point.z].isActive())
        {
            if (!hasPoint)
            {
                p1 = point;
                hasPoint = true;
            }
            else
            {
                if (p1 != point)
                {
                    p2 = point;
                    createBeam(p1, p2);
                    hasPoint = false;
                }
            }
        } else
        {
          Debug.Log("Not active");
        }
    }
	
	void createBeam(Vector3 pA, Vector3 pB) {
		Beam beam = new Beam(pA, pB, beamGameObject);
		beamList.Add(beam);
		Instantiate(beamGameObject, beam.getTransform().position, beam.getTransform().rotation);
	}

    //void createWall(){}
    //void createFloor(){}
}

public class Beam { 
	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 direction;
	private float length;
	private Vector3 angle;
	private GameObject beam;
	private Transform trans;

	public Beam(Vector3 start, Vector3 end, GameObject beam) {
		startPos = start;
		endPos = end;

		Vector3 between = end - start;
    	float distance = between.magnitude;
		length = distance;

		Vector3 angletest = new Vector3(Vector3.Angle(between, beam.transform.right), Vector3.Angle(between, beam.transform.up), Vector3.Angle(between, beam.transform.forward));

		trans = beam.transform;

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

//public class Wall {}
//public class Floor {}
