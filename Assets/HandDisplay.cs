using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class HandDisplay : MonoBehaviour
{
    public HandModelSO hand;

    // The object that holds all the cards.
    public GameObject cardStack;

    // Set a specific Hand to be displayed
    public void Initialize(HandModelSO newHand) {
        // Remove the events
        hand.CardAdded -= CardAdded;
        hand.SplitHand -= SplitHand;
        // Assign the new hand
        hand = newHand;
        // Add the events
        hand.CardAdded += CardAdded;
        hand.SplitHand += SplitHand;
    }

    // A card was added to the hand
    /*
    * NEED to get the gameObject of the "dealt" card
    * AND animate movement accordingly
    */
    void CardAdded(CardModelSO card) {
        Debug.Log("HandDisplay: CardAdded");

        // Number of cards in the hand
        int count = this.hand.GetCount();

        // Create the Card object
        GameObject cardObject = new GameObject("Card " + hand.GetCount());

        // Add cardObject to arrray (CardStack)
        cardObject.transform.SetParent(cardStack.transform);

        // Set the card to be 10x10 scale.
        cardObject.transform.localScale = new Vector3(10, 10, 1);
    
        // Calculate the relative X position
        float x_pos = Math.Max(0, (count - 1)%6) * 12;
        float y_pos = Math.Max(0, (count - 1)%6) * 12;

        // There should be a second diagonal list of cards
        if (count >= 7) {
            // Place the second diagonal row down by 24
            y_pos -= 24;
        }

        // Set the position of the card
        cardObject.transform.localPosition = new Vector3(x_pos, y_pos, -1 * count);

        // Create the CardDisplay component
        cardObject.AddComponent<CardDisplay>().Initialize(card);
    }

    // The hand was split
    void SplitHand(CardModelSO card) {
        // Destroy the card
        GameObject.DestroyImmediate(cardStack.transform.Find("Card 2").gameObject);
    }

    public void TestHand(bool split=false) {
        CardModelSO first = ScriptableObject.CreateInstance<CardModelSO>();
        CardModelSO second = ScriptableObject.CreateInstance<CardModelSO>();

        if (split) {
            first.rank = "2";
            first.suit = "SPADES";
            hand.AddCard(first);

            second.rank = "2";
            second.suit = "HEARTS";
            hand.AddCard(second);

            Debug.Log("Added both 2s to Hand");

            Debug.Log("Can Split? " + hand.CanSplit());

            hand.Split();
        }

        for (int i=0; i<UnityEngine.Random.Range(1,11); i++) {
            // Create a new card
            CardModelSO card = ScriptableObject.CreateInstance<CardModelSO>();

            card.rank = "" + UnityEngine.Random.Range(2, 11);

            // Generate a random Suit
            if (UnityEngine.Random.Range(0, 2)%2 == 0) {
                card.suit = UnityEngine.Random.Range(0, 2)%2 == 0 ? "HEARTS" : "DIAMONDS";
            }
            else {
                card.suit = UnityEngine.Random.Range(0, 2)%2 == 0 ? "CLUBS" : "SPADES";
            }

            this.hand.AddCard(card);
        }
    }

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {        
        // Delete any existing children
        foreach (Transform child in gameObject.transform) {
            // Delete the child
            GameObject.DestroyImmediate(child.gameObject);
        }

        // Create a generic Hand
        hand = ScriptableObject.CreateInstance<HandModelSO>();

        // Create the CardStack object
        cardStack = new GameObject("CardStack");
        
        // There is no CardStack GameObject
        if (cardStack == null) {
            // Create a new CardStack GameObject
            cardStack = new GameObject("CardStack");
        }
        
        // Add the CardStack object to the gameObject
        cardStack.transform.SetParent(gameObject.transform);

        // Set the CardStack display location
        cardStack.transform.localPosition = new Vector3(0, 0, -1);
        cardStack.transform.localScale = new Vector3(1, 1, 1);

        // Set the CardAdded event to be triggered
        hand.CardAdded += CardAdded;

        // Set the SplitHand event to be triggered
        hand.SplitHand += SplitHand;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // Shouldn't have to do anything
        // The cards will be updated themselves.
    }
}
