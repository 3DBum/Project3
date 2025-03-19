using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    private static PlayerCollisions Player;
    public GameObject Level1;
    public bool key1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        if(Player == null)
        {
            Player = this;
        }
        else
        {
            Destroy(gameObject);
        }

        key1 = false;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Level1" && key1 == false)
        {
            transform.position = new Vector3(0.01f, 0.965f, -1.90f);
            SceneManager.LoadScene("GamePlayTest");
        }

        if(collision.gameObject.tag == "key1")
        {
            Destroy(collision.gameObject);
            key1 = true;
            transform.position = new Vector3(0.01f, 0.965f, -1.90f);
            SceneManager.LoadScene("Hub");
        }
        
    }
}
