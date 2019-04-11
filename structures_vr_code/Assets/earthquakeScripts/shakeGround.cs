using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shakeGround : MonoBehaviour
{
    public float minThrust = 20;
    public float maxThrust = 40;
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

    // (0+x)y = 20          x = 20/y
    // (1+x)y = 200         (1+20/y)y = 200
    //                      y+20 = 200
    //                      y = 180
    //                      x = 1/9

    // (s+1/90)*180
    // 20-40
    // 200-400
    public void changeMagnitude(Scrollbar slider)
    {
        float magnitude = (slider.value + (1f/9f))*180;
        Debug.Log("change mag invoked: " + magnitude);

        this.minThrust = 1f * magnitude;
        this.maxThrust = 2f * magnitude;
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
