using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class defines a gridnode */
public class GridNode : MonoBehaviour
{
	public Vector3 position {get; set;}

	public GridNode(Vector3 pos) {
		position = pos;
	}

	public override string ToString()
    {
        return "(" + position.x + ", " + position.y + ", " + position.z + ")";
    }
}