using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class defines a gridnode */
public class GridNode
{
	private string name;
	public gridController myGridController;
	public Vector3 position {get; set;}
	private highlighter frameHighlighter;
	private GameObject gridNodeObject;
	private bool isSelected = false;

	/// <summary>
    /// Empty constructor
    /// </summary>
	public GridNode()
	{

	}

	/// <summary>
    /// Main constructor for gridnodes
	/// Takes in the gridnodes location and gridnode prefab object
    /// </summary>
	public GridNode(Vector3 pos, GameObject gridNodePrefab) {
        myGridController = GameObject.FindWithTag("gameControllers").GetComponent<gridController>();
		position = pos;
		gridNodeObject = MonoBehaviour.Instantiate(gridNodePrefab, position, Quaternion.identity);
		gridNodeObject.transform.parent = myGridController.transform;
		
		frameHighlighter = gridNodeObject.GetComponent<highlighter>();
		gridNodeObject.GetComponent<GridNodeBehavior>().setMyGridNode(this);
	}


	/// <summary>
    /// Prints the gridnodes location
    /// </summary>
	public override string ToString()
    {
        return "(" + position.x + ", " + position.y + ", " + position.z + ")";
    }

	/// <summary>
    /// Returns the gridnodes location
    /// </summary>
	public Vector3 getLocation()
    {
        return position;
    }


	/// <summary>
    /// Returns the selection state of the gridNode
    /// </summary>
	public bool getSelected(){
		return isSelected;
	}

    /// <summary>
    /// Sets whether the gridNode is selected or not
    /// </summary>
	public void setSelected(bool selected) {
		isSelected = selected;
		highlightObject();
	}

	/// <summary>
    /// Highlights the frame based on the selection status
    /// </summary>
    private void highlightObject() {
        if(isSelected) {
            frameHighlighter.Highlight();
        } else {
            frameHighlighter.Unhighlight();
        }
    }

	public GameObject GetGameObject()
    {
		return gridNodeObject;
	}

	public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }
}