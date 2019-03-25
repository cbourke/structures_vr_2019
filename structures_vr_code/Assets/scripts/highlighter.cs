using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class isused to highlight an object by changing its material */
public class highlighter : MonoBehaviour
{
    public Material highlightMaterial;
    public GameObject highlightMesh;
    private Material unhighlightMaterial;

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
        MeshRenderer[] renderers = highlightMesh.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = highlightMaterial;
        }
    }

    /// <summary>
    /// Sets the mesh to the origional material
    /// </summary>
    public void Unhighlight() {
        MeshRenderer[] renderers = highlightMesh.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = unhighlightMaterial;
        }
    }
}
