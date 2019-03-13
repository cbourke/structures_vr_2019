using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum selectionBehaviors {
	reset,
	additive
}

/* Selection tool functions */
public class selectionController : MonoBehaviour {
    public Material unityMaterialForSelectedFrames;
    public Material unityMaterialForUnselectedFrames;

	private selectionBehaviors selectionBehavior = selectionBehaviors.additive;
	private List<Frame> selectedFrames = new List<Frame>();
	private List<GridNode> selectedNodes = new List<GridNode>();
    private GridNode drawNode;

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
                    if(deselFrame.getName() != targetFrame.getName())
                    {
				        addListToSelection(targetFrame);
                    }
                }
                else
                {
                    addListToSelection(targetFrame);
                }
				break;
			}
			case selectionBehaviors.additive:
			{
                Debug.Log("ADD FRAME: " + targetFrame.getName());
				addListToSelection(targetFrame);
                printSelectedFrames();
				break;
			}
		}
	}

    /// <summary>
    /// Select a single gridNode
    /// Used by the selection tool
    /// </summary>
	public void select(GridNode targetNode)
	{
        Debug.Log("Select Gridnode");
		switch(selectionBehavior)
		{
			case selectionBehaviors.reset:
			{
                // we need to deselect the current frame
                if(selectedNodes.Count != 0) {
                    GridNode deselNode = selectedNodes[0];
                    deselect(deselNode);
                    if(deselNode != targetNode)
                    {
				        addListToSelection(targetNode);
                    }
                }
                else
                {
                    addListToSelection(targetNode);
                }
				break;
			}
			case selectionBehaviors.additive:
			{
				addListToSelection(targetNode);
				break;
			}
		}
	}

    /// <summary>
    /// Select a list of frames
    /// Used when selecting a specific frame group
    /// </summary>
    public void select(List<Frame> targetFrameList)
	{
        addListToSelection(targetFrameList);
	}

    /// <summary>
    /// Select a list of grid nodes
    /// Not sure the application for this, but currently 
    /// it is called by the select single gridnode function
    /// Doesn't actually have a use in the VR enviroment currently
    /// </summary>
	public void select(List<GridNode> targetNodeList)
	{
		addListToSelection(targetNodeList);
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
	}

    /// <summary>
    /// Adds a list of frames to the selection
    /// This is used when selecting a specific group of frames
    /// Deselects all the current frames first, then selects the frames in the list
    /// </summary>
    private void addListToSelection(List<Frame> targetFrameList)
	{
        Frame frameToDesel = new Frame();
		foreach(Frame targetFrame in targetFrameList)
		{
            bool notSel = true;
            foreach(Frame f in selectedFrames)
            {
                if(f.getName() == targetFrame.getName())
                {
                    // fame is currently selected, we need to deselect
                    frameToDesel = f;
                    notSel = false;
                }
            }
            if(notSel)
            {
                targetFrame.setSelected(true);
                selectedFrames.Add(targetFrame);
            } else {
                deselect(frameToDesel);
            }
            
		}
	}


    /// <summary>
    /// adds a single node to the list of currently selected frames
    /// </summary>
	private void addListToSelection(GridNode targetNode)
	{
		GridNode nodeToDesel = new GridNode();
        bool isSelected = false;
        // first check to see if the target node is selected already
        foreach(GridNode node in selectedNodes)
        {
            if(node == targetNode)
            {
                // node is currently selected, we need to deselect
                nodeToDesel = node;
                isSelected = true;
                break;
            }
        }
        if(isSelected)
        {
            deselect(nodeToDesel);
        } else {
            targetNode.setSelected(true);
            selectedNodes.Add(targetNode);
        }
	}
    
    /// <summary>
    /// Adds a list of nodes to the selection
    /// Not sure where this would be used
    /// </summary>
	private void addListToSelection(List<GridNode> targetGridNodeList)
	{
        GridNode gridNodeToDesel = new GridNode();
		foreach(GridNode targetGridNode in targetGridNodeList)
		{
            bool notSel = true;
            foreach(GridNode g in selectedNodes)
            {
                if(g.getName() == targetGridNode.getName())
                {
                    // fame is currently selected, we need to deselect
                    gridNodeToDesel = g;
                    notSel = false;
                }
            }
            if(notSel)
            {
                targetGridNode.setSelected(true);
                selectedNodes.Add(targetGridNode);
            } else {
                deselect(gridNodeToDesel);
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
    public void deselect(GridNode targetNode)
    {
        List<GridNode> singletonNodeList = new List<GridNode>();
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
    public void deselect(List<GridNode> targetNodeList)
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
        List<int> removeIndicies = new List<int>();
        Frame targetFrame;
        int numTarget = targetFrameList.Count;

        for(int i=0; i<numTarget; i++)
        {
            targetFrame = targetFrameList[i];
            int numSelected = selectedFrames.Count;
            for(int j=0; j<numSelected; j++)
            {
                if(selectedFrames[j].getName() == targetFrame.getName())
                {
                    removeIndicies.Add(j);
                    break;
                }
            }
        }
        int count = 0;
        foreach (int removeIndex in removeIndicies)
        {
            selectedFrames[removeIndex-count].setSelected(false);
            selectedFrames.RemoveAt(removeIndex-count);
            count++;
        }
    }
    
    /// <summary>
    /// Removes a list of nodes
    /// This is the function that acontains the actual logic for removing frames from a selection
    /// </summary>
    private void removeListFromSelection(List<GridNode> targetNodeList)
    {
        List<int> removeIndicies = new List<int>();
        GridNode targetGridNode;
        int numTarget = targetNodeList.Count;

        for(int i=0; i<numTarget; i++)
        {
            targetGridNode = targetNodeList[i];
            int numSelected = selectedNodes.Count;
            for(int j=0; j<numSelected; j++)
            {
                if(selectedNodes[j].getName() == targetGridNode.getName())
                {
                    removeIndicies.Add(j);
                    break;
                }
            }
        }
        int count = 0;
        foreach (int removeIndex in removeIndicies)
        {
            selectedNodes[removeIndex-count].setSelected(false);
            selectedNodes.RemoveAt(removeIndex-count);
            count++;
        }
    }

    /// <summary>
    /// Clears the frame selection
    /// @TODO impliment
    /// </summary>
    void clearFrameSelection()
	{
	}
    
    /// <summary>
    /// Clears all selected nodes
    /// @TODO impliment
    /// </summary>
    public void clearNodeSelection()
	{
        //removeListFromSelection(selectedNodes);
	}

    public void setDrawNode(GridNode node) {
        drawNode = node;
        drawNode.setSelected(true);
    }

    public void clearDrawNode() {
        drawNode.setSelected(false);
    }



    /// <summary>
    /// Returns the selected frames
    /// </summary>
	public List<Frame> GetSelectedFrames()
	{
        printSelectedFrames();
		return selectedFrames;
	}

    /// <summary>
    /// Returns the selected grid nodes
    /// </summary>
	public List<GridNode> GetSelectedNodes()
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

    /// <summary>
    /// Toggles between single and multi select
    /// </summary>
    public void toggleSelectionType() {
        if(selectionBehavior == selectionBehaviors.additive)
        {
            // deselect all frames but the last selected one
            List<Frame> deselList = new List<Frame>();
            for(int i=0; i<selectedFrames.Count-1; i++)
            {
                deselList.Add(selectedFrames[i]);
            }

            deselect(deselList);
            selectionBehavior = selectionBehaviors.reset;
        } 
        else 
        {
            selectionBehavior = selectionBehaviors.additive;
        }
    }
}
