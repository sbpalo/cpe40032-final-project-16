using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController3 : MonoBehaviour
{
    public const int gridRows3 = 4;
    public const int gridCols3 = 8;
    public const float offsetX3 = 2.2f;
    public const float offsetY3 = 1.9f;

    [SerializeField] private MainCard3 originalCard3;
    [SerializeField] private Sprite[] images3;  

    private void Start()
    {
        if (PlayerPrefs.HasKey("Highscore3") == true)
        {
            highscore3.text = PlayerPrefs.GetInt("Highscore3").ToString();
        }
        else
        {
            highscore3.text = "0";
        }

        //--------------------------------------------------------------------------
        Vector3 startPos3 = originalCard3.transform.position; //The position of the first card. All other cards are offset from here.

        int[] numbers3 =  {0, 0, 1, 1, 2, 2, 3, 3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13,14,14,15,15};
        numbers3 = ShuffleArray3(numbers3);

        for (int i = 0; i < gridCols3; i++)
        {
            for(int j = 0; j<gridRows3; j++)
            {
                MainCard3 card3;
                if( i == 0 && j == 0)
                {
                    card3 = originalCard3;
                }
                else
                {
                    card3 = Instantiate(originalCard3) as MainCard3;
                }

                int index3 = j * gridCols3 + i;
                int id3 = numbers3[index3];
                card3.ChangeSprite3(id3, images3[id3]);

                float posX3 = (offsetX3 * i) + startPos3.x;
                float posY3 = (offsetY3 * j) + startPos3.y;
                card3.transform.position = new Vector3(posX3, posY3, startPos3.z);
                
            }
        }
    }
    

    private int[] ShuffleArray3(int[] numbers3)
    {
        int[] newArray3 = numbers3.Clone() as int[];
        for( int i=0; i < newArray3.Length; i++)
        {
            int tmp3 = newArray3[i];
            int r3 = Random.Range(i, newArray3.Length);
            newArray3[i] = newArray3[r3];
            newArray3[r3] = tmp3;
        }
        return newArray3;
    }

    //------------------------------------------------------------------------------------------------------------------
    private MainCard3 _firstRevealed3;
    private MainCard3 _secondRevealed3;

    private int countCorrectGuess3;
    private const int gameGuess3 = 16;
    private int _score3 = 0;

    [SerializeField] private TextMesh scoreLabel3;
    [SerializeField] private TextMesh highscore3;
    [SerializeField] public GameObject puzzleFinished3;

    public bool canReveal3
    {
        get {return _secondRevealed3 == null; }
    }

    public void CardRevealed3(MainCard3 card3)
    {
        if(_firstRevealed3 == null)
        {
            _firstRevealed3 = card3;
        }
        else
        {
            _secondRevealed3 = card3;
            StartCoroutine(CheckMatch3());
            _score3++;
            scoreLabel3.text = "Score: " + _score3;
        }
    }

    public IEnumerator CheckMatch3()
    {
        if(_firstRevealed3.id3 == _secondRevealed3.id3)
        {
            countCorrectGuess3++;
            scoreLabel3.text = "Score: " + _score3;

            if (countCorrectGuess3 == gameGuess3)
            {
                Finish3();
                puzzleFinished3.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed3.Unreveal3();
            _secondRevealed3.Unreveal3();
        }
        _firstRevealed3 = null;
        _secondRevealed3 = null;
    }

   public void Restart3()
    {
        SceneManager.LoadScene("HardMode");
		countCorrectGuess3 = 0;
    }

    private void SetHighscore3()
   {
       PlayerPrefs.SetInt("Highscore3", _score3 + 1);
       highscore3.text = PlayerPrefs.GetInt("Highscore3").ToString();
   }

    private void Finish3()
    {
       if (PlayerPrefs.GetInt("Highscore3") > _score3) 
       {
           SetHighscore3();
       }

        if (PlayerPrefs.GetInt("Highscore3") == 0) 
        {
            if (PlayerPrefs.GetInt("Highscore3") < _score3)
            {
                SetHighscore3();
            }
        }
    }

    public void ClearHighscores3()
   {
       _score3 = 0;
       PlayerPrefs.SetInt("Highscore3", _score3);
       highscore3.text = ("0");
   }
   public void FirstLoad3()
   {
       _score3 = 0;
       PlayerPrefs.SetInt("Highscore3", _score3);
   }
}