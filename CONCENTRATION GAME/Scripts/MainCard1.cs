using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard1 : MonoBehaviour
{
    [SerializeField] private SceneController1 controller1;
    [SerializeField] private GameObject Card_Back1;

    public void OnMouseDown()
    { 
        if (Card_Back1.activeSelf && controller1.canReveal1)
        {
            Card_Back1.SetActive(false);
            controller1.CardRevealed1(this);
        }
    }

    private int _id1;
    public int id1
    {
        get {return _id1; }
    }

    public void ChangeSprite1 (int id1, Sprite image1)
    {
        _id1 = id1;
        GetComponent<SpriteRenderer>().sprite = image1; //This gets the sprite renderer component and changes the property of its sprite.      
    }

    public void Unreveal1()
    {
        Card_Back1.SetActive(true);
    }
}
