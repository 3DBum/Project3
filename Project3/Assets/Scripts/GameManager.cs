using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  /*  public GameObject WinMenu;
    public GameObject LoseMenu;
    public GameObject PauseMenu;
    
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI ObjectiveText; */

    private bool isPaused;
    private bool PlayerWon;
    private float Timer;
    PlayerCollisions PlayerCollisions;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        PlayerWon = false;
       // Timer = 5f;
        Time.timeScale = 1;

      //  PlayerCollisions = GameObject.Find("Player").GetComponent<PlayerCollisions>();

       // ObjectiveText.gameObject.SetActive(true);  
    }

    // Update is called once per frame
    void Update()
    {
       // HealthText.text = "x" + PlayerCollisions.Health;

        if(Input.GetKeyDown(KeyCode.P) && PlayerWon == false)
        {
            Pause();
        }

        if(isPaused == true && Input.GetKeyDown(KeyCode.U))
        {
            UnPause();
        }

        if(!isPaused)
        {
           // timerText.text = "Time:" + (int)Timer;
           // Timer -= Time.deltaTime;

           /* if(Timer <= 0f)
            {
                Lose();
            } */

        }  
    }

    /* public void CompleteLevel()
     {


     } */

    public void Win()
    {
        Debug.Log("You won");
        PlayerWon = true;
        Time.timeScale = 0;
       // ObjectiveText.gameObject.SetActive(false);
      //  WinMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }

    public void Lose()
    {
        Debug.Log("You lost");
       // ObjectiveText.gameObject.SetActive(false);
      //  LoseMenu.SetActive(true);

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Pause()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0;
        isPaused = true;
       // ObjectiveText.gameObject.SetActive(false);
       // PauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UnPause()
    {
        Debug.Log("Game Unpaused");
        Time.timeScale = 1;
        isPaused = false;
       // ObjectiveText.gameObject.SetActive(true);
       // PauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
