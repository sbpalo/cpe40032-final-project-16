 using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuBehavior : MonoBehaviour {
    public void triggerMenuBehavior (int i) {
        switch (i) {
        default:
        case(0):
            SceneManager.LoadScene("Level");
            break;
        case(1):
            Application.Quit();
            break;

        }
    }

     public void QuitGame()
    {
        Application.Quit();
    }
}
