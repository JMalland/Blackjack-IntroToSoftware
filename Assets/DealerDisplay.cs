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
    public Boolean debug = false;

    //dealer hand
    public HandDisplay hand;
    public DeckModelSO deck = ScriptableObject.CreateInstance<DeckModelSO>();
    public CardModelSO mostRecentCard = ScriptableObject.CreateInstance<CardModelSO>();

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

    HandDisplay CreateHand() {
        // Create the Hand GameObject
        GameObject hand = new GameObject("Hand");
        // Set the Hand to be a child of Dealer
        hand.transform.SetParent(gameObject.transform);
        // Set the displayed position of the Hand
        hand.transform.localPosition = new Vector3(0, 100, 0);
        // Add the HandDisplay Component to the hand
        HandDisplay display_hand = hand.AddComponent<HandDisplay>();

        // Test the hand display
        display_hand.TestHand(false);
        
        return(display_hand);
    }

    void CreateDeck() {
        // Create the Deck GameObject
        GameObject deck = new GameObject("Deck");
        // Set the Deck to be a child of Dealer
        deck.transform.SetParent(gameObject.transform);
        // Add the DeckDisplay Component to the deck
        DeckDisplay display_deck = deck.AddComponent<DeckDisplay>();
        // Set the scale of the deck
        deck.transform.localScale = new Vector3(13, 13, 1);
        // Set the position of the deck
        deck.transform.localPosition = new Vector3(180, 150, 0);
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
        rect.localPosition = new Vector3(0, -540, 5);

        // Create the Dealer's UI GameObjects
        this.hand = CreateHand();
        CreateDeck();
        CreateRobotDealer();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
    }

    // Update is called once per frame
    void Update() {
        
    }
}
