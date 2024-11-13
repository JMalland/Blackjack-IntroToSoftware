using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    // The card Suit & Type
    public TextMeshProUGUI _suit;
    public TextMeshProUGUI _rank;

    // The front & back images
    public SpriteRenderer _cardImage;
    
    // Array of the Card ScriptableObjects
    public CardModelSO[] cards;

    // Each Card has an index that will be used to load the card properties
    void LoadSelectedCard(int index) {
        Debug.Log("Card Suit: " + cards[index].suit);
        Debug.Log("Card Type: " + cards[index].rank);

        // Check if _suit exists
        if (_suit == null) {
            Debug.LogError("_suit is not assigned in " + gameObject.name);
        }
        // The child variable "_suit" exists
        else {
            _suit.text = cards[index].suit;
        }

        // Check if _rank exists
        if (_rank == null) {
            Debug.LogError("_rank is not assigned in " + gameObject.name);
        }
        // The child variable "_rank" exists
        else {
            _rank.text = cards[index].rank;
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
        _suit = GameObject.Find("_suit").GetComponent<TextMeshProUGUI>();
        _rank = GameObject.Find("_rank").GetComponent<TextMeshProUGUI>();
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
