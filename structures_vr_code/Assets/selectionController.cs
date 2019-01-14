using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum selectionBehaviors {
	reset,
	additive
}

public class selectionController : MonoBehaviour {
	private selectionBehaviors selectionBehavior = selectionBehaviors.reset;
	private List<Frame> selectedFrames = new List<Frame>();
	private List<gridNode> selectedNodes = new List<gridNode>();

	// Selection tool functions

	public void select(Frame targetFrame)
	{
		List<Frame> singletonFrameList = new List<Frame>();
		singletonFrameList.Add(targetFrame);
		select(singletonFrameList);
	}
	public void select(gridNode targetNode)
	{
		List<gridNode> singletonNodeList = new List<gridNode>();
		singletonNodeList.Add(targetNode);
		select(singletonNodeList);
	}

    public void select(List<Frame> targetFrameList)
	{
		switch(selectionBehavior)
		{
			case selectionBehaviors.reset:
			{
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
	public void select(List<gridNode> targetNodeList)
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

	private void addListToSelection(List<Frame> targetFrameList)
	{
		foreach(Frame targetFrame in targetFrameList)
		{
            if(!selectedFrames.Contains(targetFrame))
            {
                targetFrame.GetComponent<Frame>().setSelected(true);
                selectedFrames.Add(targetFrame);
            }
            
		}
	}
	private void addListToSelection(List<gridNode> targetNodeList)
	{
		foreach(gridNode targetNode in targetNodeList)
		{
            if (!selectedNodes.Contains(targetNode))
            {
                targetNode.GetComponent<GridNodeBehavior>().setSelected(false);
                selectedNodes.Add(targetNode);
            }
		}
	}

    public void deselect(Frame targetFrame)
    {
        List<Frame> singletonFrameList = new List<Frame>();
        singletonFrameList.Add(targetFrame);
        deselect(singletonFrameList);
    }
    public void deselect(gridNode targetNode)
    {
        List<gridNode> singletonNodeList = new List<gridNode>();
        singletonNodeList.Add(targetNode);
        deselect(singletonNodeList);
    }

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
    public void deselect(List<gridNode> targetNodeList)
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

    private void removeListFromSelection(List<Frame> targetFrameList)
    {
        foreach (Frame targetFrame in targetFrameList)
        {
            if (selectedFrames.Contains(targetFrame))
            {
                targetFrame.GetComponent<Frame>().setSelected(false);
                selectedFrames.Remove(targetFrame);
            }

        }
    }
    private void removeListFromSelection(List<gridNode> targetNodeList)
    {
        foreach (gridNode targetNode in targetNodeList)
        {
            if (selectedNodes.Contains(targetNode))
            {
                targetNode.GetComponent<GridNodeBehavior>().setSelected(false);
                selectedNodes.Remove(targetNode);
            }
        }
    }

    void clearSelections()
	{
		selectedFrames.Clear();
		selectedNodes.Clear();
	}


	List<Frame> GetSelectedFrames()
	{
		return selectedFrames;
	}

	List<gridNode> GetSelectedNodes()
	{
		return selectedNodes;
	}
}
