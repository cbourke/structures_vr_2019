using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this class is used to controll the grid */
public class gridController : MonoBehaviour {
    public constructorController myConstructorController;
	public PointerController myPointerController;
    public LineRenderer previewLineRenderer;
	
	public GameObject node;
	public static gridNode[, ,] grid;
    public ScaleGround scaler;
	static private float gridSpacing;


	/// <summary>
    /// Generates the grid values and defines a grid object
	/// Each time we spawn a new grid we must delete the old one
    /// </summary>
	public void createGrid(int gridX, int gridY, int gridZ, float spacing) {
		gridSpacing = spacing;
		// Generates the grid
		grid = new gridNode[gridX, gridY, gridZ];
		for(int i=0; i<gridX; i++) {
			for(int j=0; j<gridY; j++) {
				for(int k=0; k<gridZ; k++) {
					Vector3 pos = new Vector3(i*spacing,j*spacing,k*spacing);
					grid[i,j,k] = new gridNode(pos);
				}
			}
		}
		destroyNodes();
		spawnNodes(grid);
        scaler.scale(gridX, gridZ, spacing);

	}

    /// <summary>
    /// Actually spawns in the grid node prefabs
	/// for each node that is spawned we have to set the line renderer and pointer controller
    /// </summary>
	void spawnNodes(gridNode[, ,] grid) {
        GameObject nodeInstance;
        foreach (gridNode item in grid) {
			nodeInstance = Instantiate(node, item.position, Quaternion.identity);
            nodeInstance.GetComponent<GridNodeBehavior>().setPreviewLineRenderer(previewLineRenderer);
			nodeInstance.GetComponent<GridNodeBehavior>().setPointerController(myPointerController);
			nodeInstance.transform.parent = gameObject.transform;
		
		}	
    }

    /// <summary>
    /// destroys all the nodes
    /// </summary>
	void destroyNodes() {
		foreach (Transform child in gameObject.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

    /// <summary>
    /// Returns the spacing of the grid
    /// </summary>
	public static float getSpacing() {
		return gridSpacing;
	}
}


/* This class defines a gridnode */
public class gridNode { 
	public Vector3 position {get; set;}

	public gridNode(Vector3 pos) {
		position = pos;
	}

	public override string ToString()
    {
        return "(" + position.x + ", " + position.y + ", " + position.z + ")";
    }
}
