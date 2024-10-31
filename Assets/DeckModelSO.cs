using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckModelSO", menuName = "New Deck")]
public class DeckModelSO : ScriptableObject
{
    /*
    ##################
    ## KEEP IN MIND ##
    ##################
        Multiplayer:    Game has no knowledge of next card(s) in deck. Card information must be received from server
        Singleplayer:   All the cards must be stored in a shuffled deck
    */

    // The array of all cards
    public CardModelSO[] deck;

    // The deck display image
    public Sprite image;

    // Whether the game is online or offline
    public bool isOnline;

    // Constructor for the Deck object. 
    // Online mode is false, on default.
    public void InitializeDeck(bool online = false) {
        Debug.Log("new DeckModelSO()");
        
        // Save whether the game is single/multiplayer
        this.isOnline = online;

        Debug.Log("Online: " + online);

        // The game is in singleplayer mode
        if (!this.isOnline) {
            // Create the array of all cards
            deck = Resources.LoadAll<CardModelSO>("ScriptableObjects/PlayingCards");

            // Shuffle the cards
            this.Shuffle();

            Debug.Log("Cards: [" + this.deck[0] + ", ..., " + this.deck[this.deck.Length/2 - 1] + ", ..., " + this.deck[this.deck.Length - 1] + "]");
        }
        // The game is in multiplayer mode
        else {
            // Create an empty array with no cards
            deck = new CardModelSO[0];

            Debug.Log("Cards: []");
        }
    }

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
        if (index + 1 < this.deck.Length) {
            // Shuffle the next card
            Shuffle(++ index);
        }
    }
}
