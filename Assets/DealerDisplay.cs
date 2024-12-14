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
    
    // The Dealer object
    public DealerModelSO dealer;
    

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

    void CreateHand() {
        // Create the Hand GameObject
        GameObject hand = new GameObject("Hand");
        // Set the Hand to be a child of Dealer
        hand.transform.SetParent(gameObject.transform);
        // Add the HandDisplay Component to the hand
        HandDisplay display_hand = hand.AddComponent<HandDisplay>();
        // Set the Hand to be the Dealers
        display_hand.Initialize(dealer.GetHand());
        // Set the displayed position of the Hand
        hand.transform.localPosition = new Vector3(0, 100, 0);

        // Test the hand display
        display_hand.TestHand(false);
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
        KillChildren();

        // Create the Dealer
        dealer = ScriptableObject.CreateInstance<DealerModelSO>();
        
        // Initialize the Dealer
        // Might cause an issue if Object is an extension of Player class (Polymorphism)
        dealer.Initialize();

        CreateHand();
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
