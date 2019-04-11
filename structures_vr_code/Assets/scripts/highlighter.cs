using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class isused to highlight an object by changing its material */
public class highlighter : MonoBehaviour
{
    public Material highlightMaterial;
    public GameObject highlightMesh;
    private Material unhighlightMaterial;

    public bool isNewHighlight = false;
    public GameObject frame;

    /// <summary>
    /// Saves the meshes current material
    /// </summary>
    void Start()
    {
        unhighlightMaterial = highlightMesh.GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// Sets the mesh to the highlight material
    /// </summary>
    public void Highlight() {
        if(isNewHighlight) {
            frame.GetComponent<SplineMesh.ExamplePipe>().setMaterial(highlightMaterial);
        } else {
            MeshRenderer[] renderers = highlightMesh.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material = highlightMaterial;
            }
        }
    }

    /// <summary>
    /// Sets the mesh to the origional material
    /// </summary>
    public void Unhighlight() {
        if(isNewHighlight) {
            frame.GetComponent<SplineMesh.ExamplePipe>().setMaterial(unhighlightMaterial);
        } else {
            MeshRenderer[] renderers = highlightMesh.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material = unhighlightMaterial;
            }
        }
    }
}