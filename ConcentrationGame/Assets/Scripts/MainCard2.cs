using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard2 : MonoBehaviour
{
    [SerializeField] private SceneController2 controller2;
    [SerializeField] private GameObject Card_Back2;

    public void OnMouseDown()
    { 
        if (Card_Back2.activeSelf && controller2.canReveal2)
        {
            Card_Back2.SetActive(false);
            controller2.CardRevealed2(this);
        }
    }

    private int _id2;
    public int id2
    {
        get {return _id2; }
    }

    public void ChangeSprite2 (int id2, Sprite image2)
    {
        _id2 = id2;
        GetComponent<SpriteRenderer>().sprite = image2; //This gets the sprite renderer component and changes the property of its sprite.      
    }

    public void Unreveal2()
    {
        Card_Back2.SetActive(true);
    }
}
