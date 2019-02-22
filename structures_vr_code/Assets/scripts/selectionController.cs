using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum selectionBehaviors {
	reset,
	additive
}

public class selectionController : MonoBehaviour {
    public Material unityMaterialForSelectedFrames;
    public Material unityMaterialForUnselectedFrames;



	private selectionBehaviors selectionBehavior = selectionBehaviors.reset;
	private List<Frame> selectedFrames = new List<Frame>();
	private List<GridNodeBehavior> selectedNodes = new List<GridNodeBehavior>();

	// Selection tool functions

	public void select(Frame targetFrame)
	{
		List<Frame> singletonFrameList = new List<Frame>();
		singletonFrameList.Add(targetFrame);
		select(singletonFrameList);
	}
	public void select(GridNodeBehavior targetNode)
	{
		List<GridNodeBehavior> singletonNodeList = new List<GridNodeBehavior>();
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

	private void addListToSelection(List<Frame> targetFrameList)
	{
		foreach(Frame targetFrame in targetFrameList)
		{
            if(!selectedFrames.Contains(targetFrame))
            {
                targetFrame.setSelected(true);
                MeshRenderer[] renderers = targetFrame.GetGameObject().GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.material = unityMaterialForSelectedFrames;
                }
                selectedFrames.Add(targetFrame);
            }
            
		}
	}
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
		//Debug.Log("Grid Node(s) added to selection.");
	}

    public void deselect(Frame targetFrame)
    {
        List<Frame> singletonFrameList = new List<Frame>();
        singletonFrameList.Add(targetFrame);
        deselect(singletonFrameList);
    }
    public void deselect(GridNodeBehavior targetNode)
    {
        List<GridNodeBehavior> singletonNodeList = new List<GridNodeBehavior>();
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

    private void removeListFromSelection(List<Frame> targetFrameList)
    {
        foreach (Frame targetFrame in targetFrameList)
        {
            if (selectedFrames.Contains(targetFrame))
            {
                targetFrame.setSelected(false);
                MeshRenderer[] renderers = targetFrame.GetGameObject().GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.material = unityMaterialForUnselectedFrames;
                }
                selectedFrames.Remove(targetFrame);
                Debug.Log("Removed from selection: " + targetFrame.name);
            }

        }
    }
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

    void clearSelections()
	{
		selectedFrames.Clear();
		selectedNodes.Clear();
	}


	List<Frame> GetSelectedFrames()
	{
		return selectedFrames;
	}

	List<GridNodeBehavior> GetSelectedNodes()
	{
		return selectedNodes;
	}

}
