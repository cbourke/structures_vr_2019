using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shakeGround : MonoBehaviour
{
    public float minThrust = 10;
    public float maxThrust = 15;
    public Rigidbody rb;
    
    public bool shake;
    public float delay = 0.2f;

    public int shakeCount = 5;
    private int dirX = 1;
    private int dirZ = 1;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //eqButton.onClick.AddListener(() => shakeOnClick(minThrust, maxThrust));

    }

    void Update()
    {
        if(shake) {
            shakeGroundOnClick();
            shake = false;
        }
    }



    IEnumerator shakeCoroutine()
    {
        for(int i=0; i<shakeCount; i++) {
            yield return new WaitForSeconds(delay);
            addForceToGround();
        }
    }


    public void shakeGroundOnClick()
    {
        StartCoroutine(shakeCoroutine());
    }

    private void addForceToGround()
    {
        Debug.Log("Shake");
        float thrust = Random.Range(minThrust, maxThrust);

        Vector3 forceNew = new Vector3(thrust*dirX, 0, thrust*dirZ);
        rb.AddForce(forceNew, ForceMode.Impulse);
        dirX *= -1;
        dirZ *= -1;
    }
}
