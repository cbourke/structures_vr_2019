﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Used to scale the ground to be slightly larger than the gridnodes */
public class ScaleGround : MonoBehaviour {

	public gridController myGridController;
	float groundX, groundZ, scaleX, scaleZ;

	// Use this for initialization
	public void scale (int gridX, int gridZ, float spacing) {

        print("scaling: " + gridX + " x " + gridZ);
		
		groundX = ((gridX - 1f) / 2f) * spacing;
		groundZ = (gridZ - 1f) / 2f * spacing;

		scaleX = (gridX / 5f) * spacing + 0.6f;
		scaleZ = (gridZ / 5f) * spacing + 0.6f;

		transform.position = new Vector3(groundX, 0f, groundZ);
		transform.localScale = new Vector3 (scaleX, 1, scaleZ);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
