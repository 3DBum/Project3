using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freelookcamera : MonoBehaviour
{
    public GameObject Freelook;
    public PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMovement.wallRunning == true)
        {
            //Debug.Log("Freelook disabled");
            Freelook.SetActive(false);
        }
        if(PlayerMovement.wallRunning == false && PlayerMovement.grounded == true)
        {
            //Debug.Log("Freelook enabled");
            Freelook.SetActive(true);

        }  
    }
}
