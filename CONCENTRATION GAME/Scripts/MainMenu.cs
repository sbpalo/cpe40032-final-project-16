using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameEasy()
    { 
        SceneManager.LoadScene("EasyMode");
    }

    public void PlayGameMedium()
    { 
        SceneManager.LoadScene("MediumMode");
    }

    public void PlayGameHard()
    { 
        SceneManager.LoadScene("HardMode");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT.");
        Application.Quit();
    }
}
