using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
   
   public void LoadLevel (int sceneIndex)
   {
       StartCoroutine(LoadAsynchronously(sceneIndex));
   }

   IEnumerator LoadAsynchronously(int sceneIndex)
   {
       loadingScreen.SetActive(true);

       AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

       while (!operation.isDone)
       {
           float progress = Mathf.Clamp01(operation.progress / .9f);
           Debug.Log(progress);
           slider.value = progress;
           progressText.text = progress *100f + " %";
           yield return null;

       }

   }
}
