using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 
using UnityEngine.SceneManagement; // To manipulate differents scenes

public class GameManager : MonoBehaviour {

    public Sprite[] cardFaces;
    public Sprite cardBack;
    public GameObject[] cards; // To have a reference of the cards objects
    public Text matchText;

    private bool _init = false;
    private int _matches = 13;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!_init)
        {
            initializeCards();
        }

        if(Input.GetMouseButtonUp(0))   // If someone makes a click with the mouse
        {
            checkCards();   // Check the cards
        }
	}

    void initializeCards()
    {
        for (int id = 0; id < 2; id++)
        {
            for (int i = 1; i < 14; i++)
            {
                bool test = false;
                int choice = 0;

                while (!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<Card>().initialized);
                }
                cards[choice].GetComponent<Card>().cardValue = i;
                cards[choice].GetComponent<Card>().initialized = true;
            }

            foreach (GameObject c in cards)
            {
                c.GetComponent<Card>().setupGraphics(); // Now each card has a value, so you can setup the graphic
            }

            if (!_init)
            {
                _init = true;   // Now the initialization is complete
            }
        }
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }

    public Sprite getCardFace(int i)
    {
        return cardFaces[i - 1];
    }

    void checkCards()
    {
        List<int> faceUpCards = new List<int>();

        for (int i=0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<Card>().state == 1) // If the card is face up
            {
                faceUpCards.Add(i);   // Add the card to this list
            }

            if (faceUpCards.Count == 2)   // If there are two face up cards
            {
                cardComparison(faceUpCards);  //Make a comparison of the two face up cards
            }
        }
    }

    void cardComparison(List<int> faceUpCards)
    {
        Card.DO_NOT = true;    // Make the cards stop doing anything

        int x = 0;

        if (cards[faceUpCards[0]].GetComponent<Card>().cardValue == cards[faceUpCards[1]].GetComponent<Card>().cardValue)
        {
            x = 2;
            _matches--;

            matchText.text = "Number of matches: " + _matches;

            if (_matches == 0)  // If the number of pairs remaining is 0
            {
                SceneManager.LoadScene("Menu");    // Load the Menu scene
            }

            for (int i = 0; i < faceUpCards.Count; i++)
            {
                cards[faceUpCards[i]].GetComponent<Card>().state = x;
                cards[faceUpCards[i]].GetComponent<Card>().falseCheck(); // Pause the game and wait for a second
            }
        }

    }

}
