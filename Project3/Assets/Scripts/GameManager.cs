using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  /*  public GameObject WinMenu;
    public GameObject LoseMenu;
    public GameObject PauseMenu; */
    
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI ObjectiveText; 

    private bool isPaused;
    private bool PlayerAlive;
    private bool PlayerWon;

    private float Timer;
    private int Minutes;
    private float Seconds;
    PlayerCollisions PlayerCollisions;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAlive = true;
        isPaused = false;
        PlayerWon = false;
        Timer = 120f;
        Time.timeScale = 1;

        PlayerCollisions = GameObject.Find("Player").GetComponent<PlayerCollisions>();

        ObjectiveText.gameObject.SetActive(true);  
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "Lives:" + PlayerCollisions.Health;

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
            Minutes = (int) Timer / 60;
            Seconds = (int) Timer % 60;
            TimerText.text = "" + (int)Minutes + ":" + (int)Seconds;
            Timer = Timer - Time.deltaTime;

            if(Timer <= 0f)
            {
                Lose();
            } 

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
        ObjectiveText.gameObject.SetActive(false);
        TimerText.gameObject.SetActive(false);
        HealthText.gameObject.SetActive(false);
      //  WinMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }

    public void Lose()
    {
        Debug.Log("You lost");
        ObjectiveText.gameObject.SetActive(false);
        TimerText.gameObject.SetActive(false);
        HealthText.gameObject.SetActive(false);
      //  LoseMenu.SetActive(true);

        PlayerAlive = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Pause()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0;
        isPaused = true;
        ObjectiveText.gameObject.SetActive(false);
        TimerText.gameObject.SetActive(false);
        HealthText.gameObject.SetActive(false);
       // PauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UnPause()
    {
        Debug.Log("Game Unpaused");
        Time.timeScale = 1;
        isPaused = false;
        ObjectiveText.gameObject.SetActive(true);
        TimerText.gameObject.SetActive(true);
        HealthText.gameObject.SetActive(true);
       // PauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Restart()
    {
        if(!PlayerAlive)
        {
            //Reload level
           // SceneManager.LoadScene("");
        }

        if(PlayerWon)
        {
            //Load Menu
           // SceneManager.LoadScene("");

        }

    }


}
