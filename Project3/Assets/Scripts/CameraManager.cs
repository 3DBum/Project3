using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   // public Transform Rightwall;
    public GameObject Maincamera;
    public GameObject Leftcamera;
    public GameObject Rightcamera;
    public PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.wallRight && PlayerMovement.wallRunning == true)
        {
            Debug.Log("Freelook disabled");
            Maincamera.SetActive(false);
            Leftcamera.SetActive(true);
        }

        if(PlayerMovement.wallLeft && PlayerMovement.wallRunning == true)
        {
            Debug.Log("Freelook disabled");
            Maincamera.SetActive(false);
            Rightcamera.SetActive(true);
        }

        if(PlayerMovement.wallRunning == false && PlayerMovement.grounded == true)
        {
            Debug.Log("Freelook enabled");
            Leftcamera.SetActive(false);
            Rightcamera.SetActive(false);
            Maincamera.SetActive(true);

        }  
        
    }
}
