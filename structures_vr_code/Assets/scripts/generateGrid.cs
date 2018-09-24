﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateGrid : MonoBehaviour {

	public int gridX, gridY, gridZ;
	public GameObject node;
	Cell[, ,] grid;

	void Start () {
		
		grid = new Cell[gridX, gridY, gridZ];

		for(int i=0; i<gridX; i++) {
			for(int j=0; j<gridY; j++) {
				for(int k=0; k<gridZ; k++) {
					Vector3 pos = new Vector3(i,j,k);
					grid[i,j,k] = new Cell(pos);
				}
			}
		}
		
		foreach (Cell item in grid) {
			//Debug.Log(item.ToString());
		}
		Debug.Log("drawing now");
		spawnNodes(grid);
	}

	void spawnNodes(Cell[, ,] grid) {
		foreach(Cell item in grid) {
			Instantiate(node, item.position, Quaternion.identity);
		}


			
    }
	
	// Update is called once per frame
	void Update () {
	}
}	
public class Cell { 
	public Vector3 position {get; set;}

	public Cell(Vector3 pos) {
		position = pos;
	}

	public override string ToString()
    {
        return "(" + position.x + ", " + position.y + ", " + position.z + ")";
    }
}