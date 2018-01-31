using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public static bool DO_NOT = false;  //Indicates when none of the cards should be flipped.

    [SerializeField]
    private int _state;
    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private bool _initialized = false;

    // Sprites properties to hold the images of the cards
    private Sprite _cardBack;
    private Sprite _cardFace;

    private GameObject _manager;

    /*
     * Method to be call on the Start of the game
     */
    private void Start()
    {
        _state = 0;
        _manager = GameObject.FindGameObjectWithTag("Manager"); //Not recommended with tags in a large game, because of the processing time
    }

    public void setupGraphics()
    {
        _cardBack = _manager.GetComponent<GameManager>().getCardBack();
        _cardFace = _manager.GetComponent<GameManager>().getCardFace(_cardValue);

        flipCard();
    }

    public void flipCard()
    {
        if(_state == 0 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _cardBack;   //Set the sprite attribute of the component Image with the cardBack value
        }
        else if (_state == 1 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _cardFace;
        }
    }

    // Getters and setters for private attributes
    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public int state
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }

    public void falseCheck()
    {
        StartCoroutine ( pause() );
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
        if (_state == 0)
        {
            GetComponent<Image>().sprite = _cardBack;
        } else if (_state == 1)
        {
            GetComponent<Image>().sprite = _cardFace;
        }
        DO_NOT = false;
    }
}
