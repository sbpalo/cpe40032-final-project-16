using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameEasy()
    { 
        SceneManager.LoadScene("Scene_001");
    }

    public void PlayGameMedium()
    { 
        SceneManager.LoadScene("Scene_0001");
    }

    public void PlayGameHard()
    { 
        SceneManager.LoadScene("Scene_000");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT.");
        Application.Quit();
    }
}
