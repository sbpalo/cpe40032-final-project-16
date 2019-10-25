using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController2 : MonoBehaviour
{
    public const int gridRows2 = 4;
    public const int gridCols2 = 6;
    public const float offsetX2 = 3f;
    public const float offsetY2 = 2.1f;

    [SerializeField] private MainCard2 originalCard2;
    [SerializeField] private Sprite[] images2;  

    private void Start()
    {
        if (PlayerPrefs.HasKey("Highscore2") == true)
        {
            highscore2.text = PlayerPrefs.GetInt("Highscore2").ToString();
        }
        else
        {
            highscore2.text = "0";
        }

        //--------------------------------------------------------------------------
        Vector3 startPos2 = originalCard2.transform.position; //The position of the first card. All other cards are offset from here.

        int[] numbers2 = {0, 0, 1, 1, 2, 2, 3, 3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11};
        numbers2 = ShuffleArray2(numbers2);

        for (int i = 0; i < gridCols2; i++)
        {
            for(int j = 0; j<gridRows2; j++)
            {
                MainCard2 card2;
                if( i == 0 && j == 0)
                {
                    card2 = originalCard2;
                }
                else
                {
                    card2 = Instantiate(originalCard2) as MainCard2;
                }

                int index2 = j * gridCols2 + i;
                int id2 = numbers2[index2];
                card2.ChangeSprite2(id2, images2[id2]);

                float posX2 = (offsetX2 * i) + startPos2.x;
                float posY2 = (offsetY2 * j) + startPos2.y;
                card2.transform.position = new Vector3(posX2, posY2, startPos2.z);
                
            }
        }
    }
    

    private int[] ShuffleArray2(int[] numbers2)
    {
        int[] newArray2 = numbers2.Clone() as int[];
        for( int i=0; i < newArray2.Length; i++)
        {
            int tmp2 = newArray2[i];
            int r2 = Random.Range(i, newArray2.Length);
            newArray2[i] = newArray2[r2];
            newArray2[r2] = tmp2;
        }
        return newArray2;
    }

    //------------------------------------------------------------------------------------------------------------------
    private MainCard2 _firstRevealed2;
    private MainCard2 _secondRevealed2;

    private int countCorrectGuess2;
    private const int gameGuess2 = 12;
    private int _score2 = 0;

    [SerializeField] private TextMesh scoreLabel2;
    [SerializeField] private TextMesh highscore2;
    [SerializeField] public GameObject puzzleFinished2;

    public bool canReveal2
    {
        get {return _secondRevealed2 == null; }
    }

    public void CardRevealed2(MainCard2 card2)
    {
        if(_firstRevealed2 == null)
        {
            _firstRevealed2 = card2;
        }
        else
        {
            _secondRevealed2 = card2;
            StartCoroutine(CheckMatch2());
            _score2++;
            scoreLabel2.text = "Score: " + _score2;
        }
    }

    public IEnumerator CheckMatch2()
    {
        if(_firstRevealed2.id2 == _secondRevealed2.id2)
        {
            countCorrectGuess2++;
            scoreLabel2.text = "Score: " + _score2;

            if (countCorrectGuess2 == gameGuess2)
            {
                Finish2();
                puzzleFinished2.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed2.Unreveal2();
            _secondRevealed2.Unreveal2();
        }
        _firstRevealed2 = null;
        _secondRevealed2 = null;
    }

   public void Restart2()
    {
        SceneManager.LoadScene("MediumMode");
		countCorrectGuess2 = 0;
    }

    private void SetHighscore2()
   {
       PlayerPrefs.SetInt("Highscore2", _score2 + 1);
       highscore2.text = PlayerPrefs.GetInt("Highscore2").ToString();
   }

    private void Finish2()
    {
       if (PlayerPrefs.GetInt("Highscore2") > _score2) 
       {
           SetHighscore2();
       }

        if (PlayerPrefs.GetInt("Highscore2") == 0) 
        {
            if (PlayerPrefs.GetInt("Highscore2") < _score2)
            {
                SetHighscore2();
            }
        }
    }

    public void ClearHighscores2()
   {
       _score2 = 0;
       PlayerPrefs.SetInt("Highscore2", _score2);
       highscore2.text = ("0");
   }
   public void FirstLoad2()
   {
       _score2 = 0;
       PlayerPrefs.SetInt("Highscore2", _score2);
   }
}