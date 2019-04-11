using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class meshCombiner : MonoBehaviour
{
    // Start is called before the first frame update
    public void combineMeshes()
    {
        Quaternion oldRotation = transform.rotation;
        Vector3 oldPosition = transform.position;

        Quaternion temporaryRotation = Quaternion.identity;
        Vector3 temporaryPosition = Vector3.zero;

        transform.SetPositionAndRotation(temporaryPosition, temporaryRotation);


        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        Debug.Log("combining " + meshFilters.Length + " meshes.");


        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i] != GetComponent<MeshFilter>())
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                //meshFilters[i].gameObject.SetActive(false);
                meshFilters[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);

        transform.SetPositionAndRotation(oldPosition, oldRotation);

        GetComponent<SplineMesh.ExamplePipe>().mesh = GetComponent<MeshFilter>().mesh;
        GetComponent<SplineMesh.ExamplePipe>().enabled = true;
    }

}