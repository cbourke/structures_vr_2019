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



	private selectionBehaviors selectionBehavior = selectionBehaviors.additive;
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
        Frame frameToDesel = new Frame();
		foreach(Frame targetFrame in targetFrameList)
		{
            bool notSel = true;
            foreach(Frame f in selectedFrames)
            {
                Debug.Log("Framename: " + f.getName());
                if(f.getName() == targetFrame.getName())
                {
                    Debug.Log("ITS A MATCH");
                    // fame is currently selected, we need to deselect
                    frameToDesel = f;
                    notSel = false;
                }
            }
            if(notSel)
            {
                Debug.Log("SELECTING...");
                targetFrame.setSelected(true);
                selectedFrames.Add(targetFrame);
            } else {
                Debug.Log("DESELECTING...");
                deselect(frameToDesel);
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
                selectedFrames.Remove(targetFrame);
                //Debug.Log("Removed from selection: " + targetFrame.name);
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
