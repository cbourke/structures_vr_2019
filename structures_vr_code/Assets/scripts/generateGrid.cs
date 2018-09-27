using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateGrid : MonoBehaviour {

	public int gridX, gridY, gridZ;
	public GameObject node;
	public static gridNode[, ,] grid;

	void Start () {
		
		// Generates the grid
		grid = new gridNode[gridX, gridY, gridZ];
		for(int i=0; i<gridX; i++) {
			for(int j=0; j<gridY; j++) {
				for(int k=0; k<gridZ; k++) {
					Vector3 pos = new Vector3(i,j,k);
					grid[i,j,k] = new gridNode(pos);
				}
			}
		}
		spawnNodes(grid);
	}

	void spawnNodes(gridNode[, ,] grid) {
		foreach(gridNode item in grid) {
			Instantiate(node, item.position, Quaternion.identity);
		}	
    }
	
	// Update is called once per frame
	void Update () {
	}
}	

public class gridNode { 
	public Vector3 position {get; set;}
	private bool active;

	public gridNode(Vector3 pos) {
		position = pos;
        active = false;
	}

	public void disable() {
		active = false;
	}

	public void enable() {
		active = true;
	}

    public bool isActive()
    {
        return active;
    }
	public override string ToString()
    {
        return "(" + position.x + ", " + position.y + ", " + position.z + ")";
    }
}
