using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckModelSO", menuName = "New Deck")]
public class DeckModelSO : ScriptableObject {
     public Boolean debug = false;
    
    // Events that can be listened for by the Deck display manager
    public event Action<CardModelSO> CardDrawn;

    // The array of all cards
    public List<CardModelSO> deck;

    // Method to shuffle the deck
    // If not specified, default index value is -1
    public void Shuffle(int index = 0) {
        // Choose a random card between 'index' and the end of the array
        int random = UnityEngine.Random.Range(index, 52);

        // Store the initial card
        CardModelSO temp_card = this.deck[index];
        // Swap the initial card for the random card
        this.deck[index] = this.deck[random];
        // Swap the random card for the initial card
        this.deck[random] = temp_card;

        // There are more cards to be shuffled
        if (index + 1 < this.deck.Count) {
            // Shuffle the next card
            Shuffle(++ index);
        }
    }

    public CardModelSO NextCard() {
        // Get the top card from the deck
        CardModelSO top_card = deck[0];

        // Remove the top card from the deck
        deck.RemoveAt(0);

        // Return the top card.
        return(top_card);
    }

    // Constructor for the Deck object. 
    public void Initialize() {
        if (debug) Debug.Log("new DeckModelSO()");
        
        // Create the array of all cards
        deck = Resources.LoadAll<CardModelSO>("ScriptableObjects/PlayingCards").ToList();

        // Shuffle the cards
        this.Shuffle();

        if (debug) Debug.Log("Cards: [" + this.deck[0] + ", ..., " + this.deck[this.deck.Count/2 - 1] + ", ..., " + this.deck[this.deck.Count - 1] + "]");
    }
}
