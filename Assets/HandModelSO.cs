using UnityEditor.VersionControl;
using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR;
using Unity.VisualScripting;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HandModelSO", menuName = "New Hand")]
public class HandModelSO : ScriptableObject
{
    // The maximum number of cards in a given hand
    public static int maxCardLimit = 12;

    // Events that can be listened for by the Hand display manager
    public event Action<CardModelSO> CardAdded;
    public event Action<HandModelSO> SplitHand;

    // The array of cards in the hand
    private List<CardModelSO> hand = new List<CardModelSO>();

    // Whether an Ace has been counted as 11.
    // Can't have more than one 11 value Ace because 2*11=22
    private bool eleven_ace = false;
    
    // The total value of the cards in the hand
    private int value = 0;

    public static void SetLimit(int count) {
        if (count > 12) {
            Debug.LogError("Cannot have more than 12 cards in a hand.");
        }

        // Set the maximum card limit
        HandModelSO.maxCardLimit = count;
    }
    
    // Whether the hand can be split.
    public bool CanSplit() {
        return(hand.Count == 2 && hand[0].rank == hand[1].rank);
    }

    // SplitHand event should take into account the position of the two cards, and animate one being split to the position of the second hand.
    public void Split() {
        if (!this.CanSplit()) {
            Debug.LogError("This hand can not be split.");
            return;
        }

        // Instantiate the split hand.
        HandModelSO split = ScriptableObject.CreateInstance<HandModelSO>();
        
        // Set the primary card.
        split.AddCard(hand[1]);
        
        // Set the second card as null.
        hand.RemoveAt(1);

        // Invoke the Split eventlistener
        SplitHand.Invoke(split);
    }


    // CardAdded event should take into account the position of the deck, and animate a card moving from the deck to the calculated destination position.
    public void AddCard(CardModelSO card) {
        // The hand card limit will be exceeded
        if (hand.Count == HandModelSO.maxCardLimit) {
            Debug.LogError("Hand Limit Exceeded By " + (hand.Count + 1 - HandModelSO.maxCardLimit) + "Cards");
            // SHOULD END THE PROGRAM WITH THIS ERROR
            // FOR TESTING SAKE, WILL NOT
        }

        // Set the card
        hand.Add(card);

        // Calculate the value of the hand
        AddValue(card);

        // Invoke the CardAdded eventlistener
        CardAdded.Invoke(card);
    }

    // The cards in the hand, as a copy
    public CardModelSO[] GetCards() {
        // Return a copy of the cards
        return((CardModelSO[]) hand.AsReadOnlyList<CardModelSO>());
    }

    // Number of cards in the hand
    public int GetCount() {
        return(hand.Count);
    }

    // Return the calculated hand value
    public int GetValue() {
        return(value);
    }

    // Add a card's value to the hand value
    private void AddValue(CardModelSO card) {
        // Add card values from 2 to 10
        if (int.TryParse(card.rank, out int cValue)) {
            // Add the card value
            this.value += cValue;
        }
        // The card is a Jack
        else if (card.rank == "J") {
            this.value += 11;
        }
        // The card is a Queen
        else if (card.rank == "Q") {
            this.value += 12;
        }
        // The card is a King
        else if (card.rank == "K") {
            this.value += 13;
        }
        // The card is an Ace
        else {
            // Can add an Ace as 11 without busting.
            if (this.value + 11 <= 21) {
                // Mark that an Ace has been counted as 11
                this.eleven_ace = true;
                // Add Ace value
                this.value += 11;
            }
            // Can't add an Ace as 11
            else {
                // Add [this] added Ace
                this.value += 1;

                // The hand is a bust, but another Ace has been counted as 11
                if (this.eleven_ace && this.value > 21) {
                    // Change the 11 value Ace to 1
                    this.eleven_ace = false;
                    this.value -= 10;
                }
            }
        }
    }
}
