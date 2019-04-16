using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Used to make the UI panel follow the players contrtoller */
/* Also scales the UI panel in according to the players scale */
public class followController : MonoBehaviour
{
    public float positionInterpolateFactor;
    public float rotationInterpolateFactor;
    public GameObject followingTarget;
    public GameObject scaleTarget;

    // Update is called once per frame

    private void Update()
    {
        if (followingTarget.activeInHierarchy)
        {

            float newX = Mathf.Lerp(transform.position.x, followingTarget.transform.position.x, positionInterpolateFactor);
            float newY = Mathf.Lerp(transform.position.y, followingTarget.transform.position.y, positionInterpolateFactor);
            float newZ = Mathf.Lerp(transform.position.z, followingTarget.transform.position.z, positionInterpolateFactor);
            Vector3 newPosition = new Vector3(newX, newY, newZ);

            

            //float newXRot = Mathf.Lerp(transform.rotation.eulerAngles.x, followingTarget.transform.eulerAngles.x, rotationInterpolateFactor);
            //float newYRot = Mathf.Lerp(transform.rotation.eulerAngles.y, followingTarget.transform.eulerAngles.y, rotationInterpolateFactor);
            //float newZRot = Mathf.Lerp(transform.rotation.eulerAngles.z, followingTarget.transform.eulerAngles.z, rotationInterpolateFactor);
            //Vector3 newRotationVector = new Vector3(newXRot, newYRot, newZRot);
            Quaternion newRotationQuaternion = new Quaternion();
            //newRotationQuaternion.eulerAngles = newRotationVector;
            newRotationQuaternion = Quaternion.Lerp(transform.rotation, followingTarget.transform.rotation, rotationInterpolateFactor);
            //transform.SetPositionAndRotation(newPosition, newRotationQuaternion);
            transform.SetPositionAndRotation(newPosition, newRotationQuaternion);

        }

        if(scaleTarget != null) {
            transform.localScale = scaleTarget.transform.localScale;
        }
    }
}
