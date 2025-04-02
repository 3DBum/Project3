using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    private static PlayerCollisions Player;
    public GameObject Level1;
    public GameObject ElectricLevel;
    public GameObject StorageLevel;
    public GameObject MapLevel;
    public bool key1;
    public bool ElectricalKey;
    public bool StorageKey;
    public bool MapKey;

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
        ElectricalKey = false;
        StorageKey = false;
        MapKey = false;
        
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

        if(collision.gameObject.tag == "ElectricLevel" && ElectricalKey == false)
        {
            transform.position = new Vector3(2.780f, 0.796f, -3.94f);
            SceneManager.LoadScene("ElectricityRoom");
        }

        if(collision.gameObject.tag == "ElectricalKey")
        {
            Destroy(collision.gameObject);
            ElectricalKey = true;
            transform.position = new Vector3(0.01f, 0.965f, -1.90f);
            SceneManager.LoadScene("Hub");
        }

        if(collision.gameObject.tag == "StorageLevel" && StorageKey == false)
        {
            transform.position = new Vector3(0.01f, 0.965f, -1.90f);
            SceneManager.LoadScene("GamePlayTest");
        }

        if(collision.gameObject.tag == "StorageKey")
        {
            Destroy(collision.gameObject);
            StorageKey = true;
            transform.position = new Vector3(0.01f, 0.965f, -1.90f);
            SceneManager.LoadScene("Hub");
        }

        if(collision.gameObject.tag == "MapLevel" && MapKey == false)
        {
            transform.position = new Vector3(0.01f, 0.965f, -1.90f);
            SceneManager.LoadScene("GamePlayTest");
        }

        if(collision.gameObject.tag == "MapKey")
        {
            Destroy(collision.gameObject);
            MapKey = true;
            transform.position = new Vector3(0.01f, 0.965f, -1.90f);
            SceneManager.LoadScene("Hub");
        }
        
    }
}
