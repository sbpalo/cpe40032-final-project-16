using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
  public void SceneLoader(int SceneIndex) 
  {
      SceneManager.LoadScene(SceneIndex);
  }
}
