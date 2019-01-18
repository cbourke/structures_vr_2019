using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleGround : MonoBehaviour {

	public generateGrid grid;
	float groundX, groundZ, scaleX, scaleZ;

	// Use this for initialization
	void Start () {
		/*
		groundX = (grid.gridX - 1f) / 2f;
		groundZ = (grid.gridZ - 1f) / 2f;

		scaleX = (grid.gridX / 9.5f);
		scaleZ = (grid.gridZ / 9.5f);

		transform.position = new Vector3(groundX, 0f, groundZ);
		transform.localScale = new Vector3 (scaleX, 1, scaleZ);
		 */
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
