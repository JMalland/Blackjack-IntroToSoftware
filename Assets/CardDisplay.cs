using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    // The card Suit & Type
    public TextMeshProUGUI _cardSuit;
    public TextMeshProUGUI _cardType;

    // The front & back images
    public SpriteRenderer _cardImage;
    
    // Array of the Card ScriptableObjects
    public CardModelSO[] cards;

    // Each Card has an index that will be used to load the card properties
    void LoadSelectedCard(int index) {
        Debug.Log("Card Suit: " + cards[index].cardSuit);
        Debug.Log("Card Type: " + cards[index].cardType);

        // Check if _cardSuit exists
        if (_cardSuit == null) {
            Debug.LogError("_cardSuit is not assigned in " + gameObject.name);
        }
        // The child variable "_cardSuit" exists
        else {
            _cardSuit.text = cards[index].cardSuit;
        }

        // Check if _cardType exists
        if (_cardType == null) {
            Debug.LogError("_cardType is not assigned in " + gameObject.name);
        }
        // The child variable "_cardType" exists
        else {
            _cardType.text = cards[index].cardType;
        }
        
        // Check if _frontCardImage exists
        if (_cardImage == null) {
            Debug.LogError("_cardImage is not assigned in " + gameObject.name);
        }
        // The child variable "_frontCardImage" exists
        else {
            _cardImage.GetComponent<SpriteRenderer>().sprite = cards[index].frontCardImage;
        }
    }

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset()
    {
        _cardSuit = GameObject.Find("_cardSuit").GetComponent<TextMeshProUGUI>();
        _cardType = GameObject.Find("_cardType").GetComponent<TextMeshProUGUI>();
        _cardImage = GameObject.Find("_cardImage").GetComponent<SpriteRenderer>();
        
        // Load all of the CardModel objects
        this.cards = Resources.LoadAll<CardModelSO>("ScriptableObjects/PlayingCards");
        
        // Display a random card
        LoadSelectedCard(UnityEngine.Random.Range(0, cards.Length));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
        LoadSelectedCard(UnityEngine.Random.Range(0, cards.Length));
    }

    // Update is called once per frame
    void Update()
    {
        LoadSelectedCard(UnityEngine.Random.Range(0, cards.Length));
    }
}
