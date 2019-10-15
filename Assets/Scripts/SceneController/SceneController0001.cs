using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //to restart the scene

public class SceneController0001  : MonoBehaviour
{
    public const int gridRows = 4;
    public const int gridCols = 6;
    public const float offsetX = 3f;
    public const float offsetY = 2.2f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    private void Start()
    {  
        //----------------------
        if (PlayerPrefs.HasKey("Highscore")== true){
           highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
       }
       else{
           highscore.text = "No highscore yet";
       }
       //--------------------
        Vector3 startPos = originalCard.transform.position; //The position of the first card. All otehr cards are offset from here.

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11};
        numbers = ShuffleArray(numbers); //This is a function we will create in a minute!

        for(int i = 0; i < gridCols; i++)
        {
            for(int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                //to set the positions
                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    //__________________________________________________________________________

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int countTryGuess;

    private int countCorrectGuess;
     
    private int _score = 0;
    [SerializeField] private TextMesh scoreLabel;
    [SerializeField] private TextMesh highscore;
    [SerializeField] public GameObject puzzleFinished;

    public bool canReveal
    {
        get {return _secondRevealed == null;}
    }

    public void CardRevealed(MainCard card)
    {
        if(_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
             _score ++;
        }
    }

    public IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            countCorrectGuess ++;
            scoreLabel.text = "Score:" + _score;

             if (countCorrectGuess == 12)
            {
                Finish();
                puzzleFinished.SetActive(true);
                Debug.Log("FINISHED");
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;  
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_0001");
         countCorrectGuess = 0;
    }   

    private void SetHighscore()
   {
       PlayerPrefs.SetInt("Highscore", _score);
       highscore.text=PlayerPrefs.GetInt("Highscore").ToString();
   }

   private void Finish()
   {
       if (PlayerPrefs.GetInt("Highscore") > _score){
           SetHighscore();
       }
   }

    public void ClearHighscores()
   {
       _score = 0;
       PlayerPrefs.SetInt("Highscore", _score);
       highscore.text = ("0");
   }
}

