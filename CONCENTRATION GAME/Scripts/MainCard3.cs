using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard3 : MonoBehaviour
{
    [SerializeField] private SceneController3 controller3;
    [SerializeField] private GameObject Card_Back3;

    public void OnMouseDown()
    { 
        if (Card_Back3.activeSelf && controller3.canReveal3)
        {
            Card_Back3.SetActive(false);
            controller3.CardRevealed3(this);
        }
    }

    private int _id3;
    public int id3
    {
        get {return _id3; }
    }

    public void ChangeSprite3 (int id3, Sprite image3)
    {
        _id3 = id3;
        GetComponent<SpriteRenderer>().sprite = image3; //This gets the sprite renderer component and changes the property of its sprite.      
    }

    public void Unreveal3()
    {
        Card_Back3.SetActive(true);
    }
}
