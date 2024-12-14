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

public class PlayerDisplay : MonoBehaviour {
    // The Player object
    public PlayerModelSO player;

    // The array of Hand objects
    public HandModelSO[] player_hands;

    // The GameObject that stores the displayed hands
    public GameObject display_hands;

    // The player chose Hit
    void PlayerHit(HandModelSO hand) {

    }

    // The hand was split
    void SplitHand(CardModelSO card) {
        Debug.Log("PlayerDisplay: SplitHand");

        // Create the second hand GameObject
        GameObject hand_2 = new GameObject("Hand 2");
        
        // Add the HandDisplay to the GameObjects
        hand_2.transform.SetParent(display_hands.transform);

        // Get the HandDisplay object
        HandDisplay hand = hand_2.AddComponent<HandDisplay>();
        
        // Add the split card to the hand
        hand.hand.AddCard(card);

        hand.TestHand(false);
        
        // Set the second hand
        player_hands[1] = hand.hand;
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

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        // Delete any existing children
        KillChildren();

        // Create the Player 
        player = ScriptableObject.CreateInstance<PlayerModelSO>();
        
        // Initialize the Player
        // Might cause an issue if Object is an extension of Player class (Polymorphism)
        player.Initialize();

        // Get the RectTransform component (to act as a UI.Panel component)
        RectTransform rect = gameObject.AddComponent<RectTransform>() ?? gameObject.GetComponent<RectTransform>();
        
        // Set the width and height
        rect.sizeDelta = new Vector2(2*(52 + 12*5) + 30, 82 + 12*5 + 24);
        // Set the relative position
        rect.localPosition = Vector3.zero;

        // Set the display layout of the Player object.
        HorizontalLayoutGroup self_layout = gameObject.AddComponent<HorizontalLayoutGroup>() ?? gameObject.GetComponent<HorizontalLayoutGroup>();
        // Set the child alignment
        self_layout.childAlignment = TextAnchor.LowerLeft;


        // Only able to hold 2 Hands, maximum
        player_hands = new HandModelSO[2];

        // Create the GameObject to hold Player Hands
        display_hands = new GameObject("Hands");

        // Set the display layout of the two hands.
        HorizontalLayoutGroup hands_layout = display_hands.AddComponent<HorizontalLayoutGroup>();
        // Set the spacing between elements
        hands_layout.spacing = 130f;
        // Set the child alignment
        hands_layout.childAlignment = TextAnchor.MiddleLeft;

        // Create the first hand (Game Object)
        GameObject hand_1 = new GameObject("Hand 1");
        
        // Add the HandDisplay to the GameObjects
        hand_1.transform.SetParent(display_hands.transform);
        
        // Add the HandDisplay Component to the hand
        HandDisplay hand = hand_1.AddComponent<HandDisplay>();
        // The function to be called if the SplitHand event is triggered
        hand.hand.SplitHand += SplitHand;

        // Set the primary hand
        player_hands[0] = hand.hand;

        // Test the hand display, and splitting
        hand.TestHand(true);


        // Add the Hands object to the gameObject
        display_hands.transform.SetParent(gameObject.transform);

        // Set the array (Hands) display location
        display_hands.transform.localPosition = new Vector3(0, 0, -1);
        display_hands.transform.localScale = new Vector3(1, 1, 1);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
    }

    // Update is called once per frame
    void Update() {
        
    }
}
