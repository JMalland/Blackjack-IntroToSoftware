using UnityEditor.VersionControl;
using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;

[CreateAssetMenu(fileName = "HandModelSO", menuName = "New Hand")]
public class HandModelSO : ScriptableObject
{
    // The maximum number of cards in a given hand
    public readonly int maxCardLimit;

    // Events that can be listened for by the Hand display manager
    public event Action<CardModelSO> CardAdded;
    public event Action<HandModelSO> SplitHand;

    // The array of cards in the hand
    public CardModelSO[] hand;
    // The number of cards in the hand
    public int hand_count = 0;

    // Whether the hand can be split.
    public bool CanSplit() {
        return(hand.Length == 2 && hand[0].cardType == hand[1].cardType);
    }

    // SplitHand event should take into account the position of the two cards, and animate one being split to the position of the second hand.
    public void Split() {
        if (!this.CanSplit()) {
            Debug.LogError("This hand can not be split.");
            return;
        }

        // Instantiate the split hand.
        HandModelSO split = new HandModelSO();
        
        // Set the primary card.
        split.SetCard(0, hand[1]);

        // Decrement the hand count
        hand_count -= 1;
        
        // Set the second card as null.
        hand.SetValue(null, 1);

        // Invoke the Split eventlistener
        SplitHand.Invoke(split);
    }

    public void SetCard(int index, CardModelSO card) {
        // Set the card at the index
        hand.SetValue(card, index);    
    }


    // CardAdded event should take into account the position of the deck, and animate a card moving from the deck to the calculated destination position.
    public void AddCard(CardModelSO card) {
        // Set the card
        hand.SetValue(card, hand_count);

        // Increment the hand count
        hand_count += 1;

        // Invoke the CardAdded eventlistener
        CardAdded.Invoke(card);
    }

    public int CalcValue() {
        // Count the aces to be added at the end
        int aces = 0;
        // The total sum of the hand
        int total = 0;

        foreach (CardModelSO card in hand) {
            // Add card values from 2 to 10
            if (int.TryParse(card.cardType, out int value)) {
                // Add the card value
                total += value;
            }
            // The card is a Jack
            else if (card.cardType == "J") {
                total += 11;
            }
            // The card is a Queen
            else if (card.cardType == "Q") {
                total += 12;
            }
            // The card is a King
            else if (card.cardType == "K") {
                total += 13;
            }
            // The card is an Ace
            else {
                // Add to the aces counter
                aces += 1;

                // Keep going
                continue;
            }
        }

        // The total hand value is less than if aces were 
        if (aces > 0) {
            // The hand value can include an ace as 11.
            // Only use one ace as 11 because can't have two aces as 11 (22). 
            if (total + 11 + aces - 1 <= 21) {
                total += 11 + aces - 1;
            }
            // Aces can only be counted as 1.
            else {
                total += aces;
            }
        }

        // Return the hand value
        return(total);
    }

    // Constructor for the Deck object. 
    // Online mode is false, on default.
    public void InitializeHand() {
        Debug.Log("new HandModelSO()");

        // Instantiate the main hand.
        hand = new CardModelSO[maxCardLimit];
    }
}
