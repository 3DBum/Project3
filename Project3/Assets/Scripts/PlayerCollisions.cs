using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    private static PlayerCollisions Player;
    public int Health;
    public bool ElectricalKey;
    public bool StorageKey;
    public bool MapKey;
    public bool Damage;

    [SerializeField] private AudioClip damageSound; 

    GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(Player == null)
        {
            Player = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Health = 3;
        ElectricalKey = false;
        StorageKey = false;
        MapKey = false;
        Damage = false;
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ElectricLevel" && ElectricalKey == false)
        {
            transform.position = new Vector3(2.780f, 0.796f, -3.94f);
            SceneManager.LoadScene("ElectricityRoom");
        }

        if(collision.gameObject.tag == "ElectricalKey")
        {
            Destroy(collision.gameObject);
            ElectricalKey = true;
           // transform.position = new Vector3(0.01f, 0.965f, -1.90f);
           // SceneManager.LoadScene("Hub");
            GameManager.Win();

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

        if(collision.gameObject.tag == "Damage")
        {
            CheckHealth();
            transform.position = new Vector3(2.780f, 0.796f, -3.94f);
        }
        
    }

    public void CheckHealth()
    {
        Health--;
        Debug.Log("Player lost a life");
        SoundManager.instance.PlaySoundClip(damageSound, transform, 0.8f);
        if(Health == 0)
        {
            GameManager.Lose();
        }
    }
}
