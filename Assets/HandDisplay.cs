using System;
using System.Collections.Generic;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    public Boolean debug = false;
    
    public HandModelSO hand;

    // The object that holds all the cards.
    public GameObject cardStack;

    // Set a specific Hand to be displayed
    public void Initialize(HandModelSO newHand) {
        // Remove the events
        if (hand != null) {
            hand.CardAdded -= CardAdded;
            hand.SplitHand -= SplitHand;
        }
        
        // Assign the new hand
        hand = newHand;

        // Add the events
        hand.CardAdded += CardAdded;
        hand.SplitHand += SplitHand;
    }

    // A card was added to the hand
    void CardAdded(CardModelSO card) {
        if (debug) Debug.Log("HandDisplay: CardAdded");

        // Number of cards in the hand
        int count = hand.GetCount();

        // Create the Card object
        GameObject cardObject = new GameObject($"Card {count}");

        // Add cardObject to array (CardStack)
        cardObject.transform.SetParent(cardStack.transform);

        // Set the card to be 10x10 scale.
        cardObject.transform.localScale = new Vector3(10, 10, 1);
    
        // Calculate the relative X position
        float x_pos = (count - 1) * 8;
        float y_pos = (count >= 7) ? -24 : 0;

        // Set the position of the card
        cardObject.transform.localPosition = new Vector3(x_pos, y_pos, -count);

        // Adjust the card rotation
        for (int i = 0; i < count; i++) {
            float angle = 33f * (((count - 1) / 2f - i) / (count / 2f));
            if (debug) Debug.Log($"Card #{i}: Z={angle}");
            Transform child = cardStack.transform.GetChild(i);
            child.localRotation = Quaternion.Euler(0, 0, angle);
            child.localPosition = new Vector3(child.localPosition.x, Mathf.Abs(Mathf.Tan(Mathf.Deg2Rad * angle)) * -13, child.localPosition.z);
        }

        // Create the CardDisplay component
        cardObject.AddComponent<CardDisplay>().Initialize(card);
    }

    // The hand was split
    void SplitHand(CardModelSO card) {
        // Destroy the card
        GameObject cardToDestroy = cardStack.transform.Find("Card 2")?.gameObject;
        if (cardToDestroy != null) {
            GameObject.DestroyImmediate(cardToDestroy);
        }
    }

    // A Delete function that deletes each child, outside 
    // the scope of the gameObject iterative list
    void KillChildren() {
        // Create a temporary list to cage the children
        var children = new List<Transform>();
        foreach (Transform child in gameObject.transform) {
            // Add the child to the list
            children.Add(child);
        }

        // Now destroy each child in the cage
        foreach (Transform child in children) {
            Debug.Log($"Destroying: {child.name}");
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    public void TestHand(bool split = false) {
        CardModelSO first = ScriptableObject.CreateInstance<CardModelSO>();
        CardModelSO second = ScriptableObject.CreateInstance<CardModelSO>();

        if (split) {
            first.rank = "2";
            first.suit = "SPADES";
            hand.AddCard(first);

            second.rank = "2";
            second.suit = "HEARTS";
            hand.AddCard(second);

            if (debug) Debug.Log("Added both 2s to Hand");
            if (debug) Debug.Log("Can Split? " + hand.CanSplit());
            hand.Split();
        }

        for (int i = 0; i < 10; i++) {
            // Create a new card
            CardModelSO card = ScriptableObject.CreateInstance<CardModelSO>();
            card.rank = UnityEngine.Random.Range(2, 11).ToString();
            card.suit = GetRandomSuit();

            this.hand.AddCard(card);
        }
    }

    private string GetRandomSuit() {
        return UnityEngine.Random.Range(0, 2) % 2 == 0 
            ? (UnityEngine.Random.Range(0, 2) % 2 == 0 ? "HEARTS" : "DIAMONDS") 
            : (UnityEngine.Random.Range(0, 2) % 2 == 0 ? "CLUBS" : "SPADES");
    }

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        // Delete any existing children
        KillChildren();

        // Initialize a new Hand object
        Initialize(ScriptableObject.CreateInstance<HandModelSO>());

        // Create the RectTransform component (to act as a UI.Panel component)
        RectTransform rect = gameObject.AddComponent<RectTransform>() ?? gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(52 + 12 * 5, 82 + 12 * 5 + 24);
        rect.localPosition = Vector3.zero;

        // Create the CardStack object
        cardStack = new GameObject("CardStack");
        
        // Add the CardStack object to the gameObject
        cardStack.transform.SetParent(transform);
        cardStack.transform.localPosition = new Vector3(0, 0, -1);
        cardStack.transform.localScale = Vector3.one;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Initialization logic can be added here if needed
    }

    // Update is called once per frame
    void Update() {
        // The cards will be updated themselves.
    }
}
