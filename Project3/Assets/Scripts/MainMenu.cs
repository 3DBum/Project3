using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Application has quit!");
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("ElectricityRoom");
        Cursor.visible = false;
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }


    public void Main()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
