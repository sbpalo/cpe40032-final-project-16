using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController1 : MonoBehaviour
{
    public const int gridRows1 = 2;
    public const int gridCols1 = 4;
    public const float offsetX1 = 4f;
    public const float offsetY1 = 3.5f;

    [SerializeField] private MainCard1 originalCard1;
    [SerializeField] private Sprite[] images1;  

    private void Start()
    {
        if (PlayerPrefs.HasKey("Highscore1") == true)
        {
            highscore1.text = PlayerPrefs.GetInt("Highscore1").ToString();
        }
        else
        {
            highscore1.text = "0";
        }

        //--------------------------------------------------------------------------
        Vector3 startPos1 = originalCard1.transform.position; //The position of the first card. All other cards are offset from here.

        int[] numbers1 = {0, 0, 1, 1, 2, 2, 3, 3 };
        numbers1 = ShuffleArray1(numbers1);

        for (int i = 0; i < gridCols1; i++)
        {
            for(int j = 0; j<gridRows1; j++)
            {
                MainCard1 card1;
                if( i == 0 && j == 0)
                {
                    card1 = originalCard1;
                }
                else
                {
                    card1 = Instantiate(originalCard1) as MainCard1;
                }

                int index1 = j * gridCols1 + i;
                int id1 = numbers1[index1];
                card1.ChangeSprite1(id1, images1[id1]);

                float posX1 = (offsetX1 * i) + startPos1.x;
                float posY1 = (offsetY1 * j) + startPos1.y;
                card1.transform.position = new Vector3(posX1, posY1, startPos1.z);
                
            }
        }
    }
    

    private int[] ShuffleArray1(int[] numbers1)
    {
        int[] newArray1 = numbers1.Clone() as int[];
        for( int i=0; i < newArray1.Length; i++)
        {
            int tmp1 = newArray1[i];
            int r1 = Random.Range(i, newArray1.Length);
            newArray1[i] = newArray1[r1];
            newArray1[r1] = tmp1;
        }
        return newArray1;
    }

    //------------------------------------------------------------------------------------------------------------------
    private MainCard1 _firstRevealed1;
    private MainCard1 _secondRevealed1;

    private int countCorrectGuess1;
    private const int gameGuess1 = 4;
    private int _score1 = 0;

    [SerializeField] private TextMesh scoreLabel1;
    [SerializeField] private TextMesh highscore1;
    [SerializeField] public GameObject puzzleFinished1;

    public bool canReveal1
    {
        get {return _secondRevealed1 == null; }
    }

    public void CardRevealed1(MainCard1 card1)
    {
        if(_firstRevealed1 == null)
        {
            _firstRevealed1 = card1;
        }
        else
        {
            _secondRevealed1 = card1;
            StartCoroutine(CheckMatch1());
            _score1++;
            scoreLabel1.text = "Score: " + _score1;
        }
    }

    public IEnumerator CheckMatch1()
    {
        if(_firstRevealed1.id1 == _secondRevealed1.id1)
        {
            countCorrectGuess1++;
            scoreLabel1.text = "Score: " + _score1;

            if (countCorrectGuess1 == 4)
            {
                Finish1();
                puzzleFinished1.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed1.Unreveal1();
            _secondRevealed1.Unreveal1();
        }
        _firstRevealed1 = null;
        _secondRevealed1 = null;
    }

   public void Restart1()
    {
        SceneManager.LoadScene("EasyMode");
		countCorrectGuess1 = 0;
    }

    private void SetHighscore1()
   {
       PlayerPrefs.SetInt("Highscore1", _score1 + 1);
       highscore1.text = PlayerPrefs.GetInt("Highscore1").ToString();
   }

    private void Finish1()
    {
       if (PlayerPrefs.GetInt("Highscore1") > _score1) 
       {
           SetHighscore1();
       }

        if (PlayerPrefs.GetInt("Highscore1") == 0) 
        {
            if (PlayerPrefs.GetInt("Highscore1") < _score1)
            {
                SetHighscore1();
            }
        }
    }

    public void ClearHighscores1()
   {
       _score1 = 0;
       PlayerPrefs.SetInt("Highscore1", _score1);
       highscore1.text = ("0");
   }
   public void FirstLoad1()
   {
       _score1 = 0;
       PlayerPrefs.SetInt("Highscore1", _score1);
   }
}