using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Used to scale the ground to be slightly larger than the gridnodes */
/* @TODO this feature got broken when we changed how the grid spawning works, it needs to be reimplimented */
public class ScaleGround : MonoBehaviour {

	public gridController myGridController;
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
