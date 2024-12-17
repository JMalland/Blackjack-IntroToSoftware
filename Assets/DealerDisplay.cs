using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class DealerDisplay : MonoBehaviour {
    public Boolean debug = true;

    //dealer hand
    public HandDisplay hand;
    public DeckDisplay deck;
    public CardDisplay mostRecentCard;
    
    // Just Not Sure About This Yet -- Rn, just trying to push the changes
    //public HandModelSO hand = ScriptableObject.CreateInstance<HandModelSO>();
    //public DeckModelSO deck = ScriptableObject.CreateInstance<DeckModelSO>();
    //public CardModelSO mostRecentCard = ScriptableObject.CreateInstance<CardModelSO>();

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
            if (debug) Debug.Log($"Destroying: {child.name}");
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    // Deal a card to a HandDisplay hand
    // Ensures cards are dealt sequentially by waiting for animations to complete
    public IEnumerator DealCard(HandDisplay hand) {
        Debug.Log("Deal Card");

        // Draw a card
        CardDisplay card = deck.DrawCard();

        if (hand.transform.parent == gameObject.transform) {
            Debug.Log("This Hand Is The Dealer's");
            Debug.Log(hand.hand.GetCount());
        }

        // Dealing to Dealer's hand
        if (hand.transform.parent == gameObject.transform && hand.hand.GetCount() == 1) {
            Debug.Log("Hiding Dealer Card");
            // Hide the second card
            card.HideCard();
        }

        // Add the card to the given hand and wait for the animation to complete
        yield return StartCoroutine(hand.AddCard(card));
    }


    HandDisplay CreateHand() {
        // Create the Hand GameObject
        GameObject hand = new GameObject("Hand");
        // Set the Hand to be a child of Dealer
        hand.transform.SetParent(gameObject.transform);
        // Set the displayed position of the Hand
        hand.transform.localPosition = new Vector3(0, 100, 0);
        // Add the HandDisplay Component to the hand
        HandDisplay display_hand = hand.AddComponent<HandDisplay>();
        
        return(display_hand);
    }

    DeckDisplay CreateDeck() {
        // Create the Deck GameObject
        GameObject deck = new GameObject("Deck");
        // Set the Deck to be a child of Dealer
        deck.transform.SetParent(gameObject.transform);
        // Add the DeckDisplay Component to the deck
        DeckDisplay display_deck = deck.AddComponent<DeckDisplay>();
        // Set the scale of the deck
        deck.transform.localScale = new Vector3(13, 13, 1);
        // Set the position of the deck
        deck.transform.localPosition = new Vector3(180, 150, 2);

        return(display_deck);
    }

    void CreateRobotDealer() {
        // Create the robo-dealer icon
        GameObject robot = new GameObject("Robot");
        // Set the Robot to be a child of Dealer
        robot.transform.SetParent(gameObject.transform);
        // Set the scale of the Robot
        robot.transform.localScale = new Vector2(50, 50);
        // Set the position of the robot
        robot.transform.localPosition = new Vector3(0, 370, 5);
        
        // Create the robot-dealer SpriteRenderer
        SpriteRenderer robot_sprite = robot.AddComponent<SpriteRenderer>();
        robot_sprite.sprite = Resources.Load<Sprite>("Icons/RobotCheer");
        robot_sprite.drawMode = SpriteDrawMode.Simple;
        robot_sprite.material = Resources.Load<Material>("Materials/Unlit_VectorGradient");
    }

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        // Delete any existing children
        KillChildren();

        // Get the RectTransform component (to act as a UI.Panel component)
        RectTransform rect = gameObject.AddComponent<RectTransform>() ?? gameObject.GetComponent<RectTransform>();
        // Set the anchored position
        rect.sizeDelta = new Vector2(0, 0);
        rect.anchorMin = new Vector2(0.5f, 1);
        rect.anchorMax = new Vector2(0.5f, 1);
        rect.pivot = new Vector2(0.5f, 0.5f);
        // Set the position
        // WEIRD: For some reason, if the Y value is set to 0, it displays as -540
        //        When set to -540f, it displays as -1080. ... IDK
        //        -540 is the desired Y value for the dealer.
        rect.localPosition = new Vector2(0, 0);

        // Create the Dealer's UI GameObjects
        this.hand = CreateHand();
        this.deck = CreateDeck();
        CreateRobotDealer();
    }

    // Triggers before Start() (prior to first frame)
    void Awake() {
        // Reset the DealerDisplay
        // Also Resets the Deck
        Reset();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
    }

    // Update is called once per frame
    void Update() {
        
    }
}
