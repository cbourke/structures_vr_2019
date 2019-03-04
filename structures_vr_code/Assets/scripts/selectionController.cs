using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum selectionBehaviors {
	reset,
	additive
}

/* Selection tool functions */
/* NOTE currently only select single frame has been tested to work */
public class selectionController : MonoBehaviour {
    public Material unityMaterialForSelectedFrames;
    public Material unityMaterialForUnselectedFrames;



	private selectionBehaviors selectionBehavior = selectionBehaviors.additive;
	private List<Frame> selectedFrames = new List<Frame>();
	private List<GridNodeBehavior> selectedNodes = new List<GridNodeBehavior>();

    /// <summary>
    /// Select a single frame
    /// Used by the selection tool
    /// </summary>
	public void select(Frame targetFrame)
	{
        switch(selectionBehavior)
		{
			case selectionBehaviors.reset:
			{
                // we need to deselect the current frame
                if(selectedFrames.Count != 0) {
                    Frame deselFrame = selectedFrames[0];
                    deselect(deselFrame);
                }
				selectedFrames.Clear();
				addListToSelection(targetFrame);
				break;
			}
			case selectionBehaviors.additive:
			{
				addListToSelection(targetFrame);
				break;
			}
		}
	}

    /// <summary>
    /// Select a single gridNode
    /// Used by the selection tool
    /// </summary>
	public void select(GridNodeBehavior targetNode)
	{
		List<GridNodeBehavior> singletonNodeList = new List<GridNodeBehavior>();
		singletonNodeList.Add(targetNode);
		select(singletonNodeList);
	}

    /// <summary>
    /// Select a list of frames
    /// Used when selecting a specific frame group
    /// </summary>
    public void select(List<Frame> targetFrameList)
	{
		switch(selectionBehavior)
		{
			case selectionBehaviors.reset:
			{
                // we need to deselect the current frame
                if(selectedFrames.Count != 0) {
                    Frame deselFrame = selectedFrames[0];
                    deselect(deselFrame);
                }
				selectedFrames.Clear();
				addListToSelection(targetFrameList);
				break;
			}
			case selectionBehaviors.additive:
			{
				addListToSelection(targetFrameList);
				break;
			}
		}
	}

    /// <summary>
    /// Select a list of grid nodes
    /// @TODO needs to be rewritten
    /// Not sure the application for this, but currently 
    /// it is called by the select single gridnode function
    /// Doesn't actually have a use in the VR enviroment currently
    /// </summary>
	public void select(List<GridNodeBehavior> targetNodeList)
	{
		switch(selectionBehavior)
		{
			case selectionBehaviors.reset:
			{
				selectedNodes.Clear();
				addListToSelection(targetNodeList);
				break;
			}
			case selectionBehaviors.additive:
			{
				addListToSelection(targetNodeList);
				break;
			}
		}
	}

    /// <summary>
    /// adds a single frame to the list of currently selected frames
    /// </summary>
	private void addListToSelection(Frame targetFrame)
	{
        Frame frameToDesel = new Frame();
        bool isSelected = false;
        // first check to see if the target frame is selected already
        foreach(Frame frame in selectedFrames)
        {
            if(frame.getName() == targetFrame.getName())
            {
                // fame is currently selected, we need to deselect
                frameToDesel = frame;
                isSelected = true;
                break;
            }
        }
        if(isSelected)
        {
            deselect(frameToDesel);
        } else {
            targetFrame.setSelected(true);
            selectedFrames.Add(targetFrame);
        }

        printSelectedFrames();
	}

    /// <summary>
    /// Adds a list of frames to the selection
    /// This is used when selecting a specific group of frames
    /// Deselects all the current frames first, then selects the frames in the list
    /// @TODO needs to be finished
    /// </summary>
    private void addListToSelection(List<Frame> targetFrameList)
	{
        Frame frameToDesel = new Frame();
		foreach(Frame targetFrame in targetFrameList)
		{
            bool notSel = true;
            foreach(Frame f in selectedFrames)
            {
                //Debug.Log("Framename: " + f.getName());
                if(f.getName() == targetFrame.getName())
                {
                    //Debug.Log("ITS A MATCH");
                    // fame is currently selected, we need to deselect
                    frameToDesel = f;
                    notSel = false;
                }
            }
            if(notSel)
            {
                //Debug.Log("SELECTING...");
                targetFrame.setSelected(true);
                selectedFrames.Add(targetFrame);
            } else {
                //Debug.Log("DESELECTING...");
                deselect(frameToDesel);
            }
            
		}
	}


    /// <summary>
    /// Adds a list of nodes to the selection
    /// Not sure where this would be used
    /// @TODO probably needs to be fixed and tested if we need to use this
    /// </summary>
	private void addListToSelection(List<GridNodeBehavior> targetNodeList)
	{
		foreach(GridNodeBehavior targetNode in targetNodeList)
		{
            if (!selectedNodes.Contains(targetNode))
            {
                targetNode.setSelected(false);
                selectedNodes.Add(targetNode);
            }
		}
	}

    /// <summary>
    /// Deselect a single frame
    /// Used by the selection tool
    /// </summary>
    public void deselect(Frame targetFrame)
    {
        List<Frame> singletonFrameList = new List<Frame>();
        singletonFrameList.Add(targetFrame);
        deselect(singletonFrameList);
    }
    
    /// <summary>
    /// Deselect a single gridnode
    /// Used by the selection tool
    /// </summary>
    public void deselect(GridNodeBehavior targetNode)
    {
        List<GridNodeBehavior> singletonNodeList = new List<GridNodeBehavior>();
        singletonNodeList.Add(targetNode);
        deselect(singletonNodeList);
    }

    /// <summary>
    /// Deselect a list of frames
    /// </summary>
    public void deselect(List<Frame> targetFrameList)
    {
        switch (selectionBehavior)
        {
            case selectionBehaviors.reset:
            case selectionBehaviors.additive:
                {
                   removeListFromSelection(targetFrameList);
                    break;
                }
        }
    }
    
    /// <summary>
    /// Deselect a list of grid nodes
    /// </summary>
    public void deselect(List<GridNodeBehavior> targetNodeList)
    {
        switch (selectionBehavior)
        {
            case selectionBehaviors.reset:
            case selectionBehaviors.additive:
                {
                    removeListFromSelection(targetNodeList);
                    break;
                }
        }
    }

    /// <summary>
    /// Removes a list of frames
    /// This is the function that contains the actual logic for removing frames from a selection
    /// </summary>
    private void removeListFromSelection(List<Frame> targetFrameList)
    {
        foreach (Frame targetFrame in targetFrameList)
        {
            for(int i=0; i<selectedFrames.Count; i++)
            {
                if(selectedFrames[i].getName() == targetFrame.getName())
                {
                    targetFrame.setSelected(false);
                    selectedFrames.RemoveAt(i);
                    Debug.Log("Removed from selection: " + targetFrame.getName());
                }
            }
        }
    }
    
    /// <summary>
    /// Removes a list of nodes
    /// This is the function that acontains the actual logic for removing frames from a selection
    /// @TODO this probably needs to be rewritten and tested
    /// </summary>
    private void removeListFromSelection(List<GridNodeBehavior> targetNodeList)
    {
        foreach (GridNodeBehavior targetNode in targetNodeList)
        {
            if (selectedNodes.Contains(targetNode))
            {
                targetNode.setSelected(false);
                selectedNodes.Remove(targetNode);
            }
        }
    }

    /// <summary>
    /// Clears the entire selection
    /// </summary>
    void clearSelections()
	{
		selectedFrames.Clear();
		selectedNodes.Clear();
	}

    /// <summary>
    /// Returns the selected frames
    /// </summary>
	public List<Frame> GetSelectedFrames()
	{
		return selectedFrames;
	}

    /// <summary>
    /// Returns the selected grid nodes
    /// </summary>
	public List<GridNodeBehavior> GetSelectedNodes()
	{
		return selectedNodes;
	}

    /// <summary>
    /// Prints out the current selection of frames
    /// </summary>
    public void printSelectedFrames()
    {
        Debug.Log("Selected Frames:");
        foreach(Frame f in selectedFrames)
        {
            Debug.Log("Frame: " + f.getName());
        }
        
    }
}
