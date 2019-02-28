using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlighter : MonoBehaviour
{
    public Material highlightMaterial;
    public GameObject highlightMesh;
    private Material unhighlightMaterial;

    // Start is called before the first frame update
    void Start()
    {
        unhighlightMaterial = highlightMesh.GetComponent<MeshRenderer>().material;
    }

    public void Highlight() {
        MeshRenderer[] renderers = highlightMesh.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = highlightMaterial;
        }
    }

    public void Unhighlight() {
        MeshRenderer[] renderers = highlightMesh.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = unhighlightMaterial;
        }
    }
}
