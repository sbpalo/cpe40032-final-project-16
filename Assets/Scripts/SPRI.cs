using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPRI : MonoBehaviour
{
   private int time = 0;
   public Text timer;
   public Text highscore;

   void Start(){
       if (PlayerPrefs.HasKey("Highscore")== true){
           highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
       }
       else{
           highscore.text = "No highscore yet";
       }
   }

   public void StartTimer()
   {
       time= 0;
       InvokeRepeating("IncrementTime", 1,1);
   }

   public void StopTimer()
   {
       CancelInvoke();
       if (PlayerPrefs.GetInt("Highscore")< time){
           SetHighscore();
       }
   }

   public void SetHighscore()
   {
       PlayerPrefs.SetInt("Highscore", time);
       highscore.text=PlayerPrefs.GetInt("Highscore").ToString();
   }

   public void ClearHighscores()
   {
       PlayerPrefs.SetInt("Highscore", time);
       PlayerPrefs.DeleteKey("Highscores"); 
       highscore.text = ("No Highscores yet");
   }

   void IncrementTime()
   {
       time +=1;
       timer.text = "Time: "  + time;
   }

}

