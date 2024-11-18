using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class DealerDisplay : MonoBehaviour {
    // The Dealer object
    public DealerModelSO dealer;
    

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        // Delete any existing children
        foreach (Transform child in gameObject.transform) {
            // Delete the child
            GameObject.DestroyImmediate(child.gameObject);
        }

        Debug.Log("FUCK YOU");

        // Create the Dealer
        dealer = ScriptableObject.CreateInstance<DealerModelSO>();
        
        // Initialize the Dealer
        // Might cause an issue if Object is an extension of Player class (Polymorphism)
        dealer.Initialize();

        // Create the Hand GameObject
        GameObject hand = new GameObject("Hand");
        // Set the Hand to be a child of Dealer
        hand.transform.SetParent(gameObject.transform);
        // Add the HandDisplay Component to the hand
        HandDisplay display_hand = hand.AddComponent<HandDisplay>();
        // Set the Hand to be the Dealers
        display_hand.Initialize(dealer.GetHand());
        // Set the displayed position of the Hand
        hand.transform.localPosition = new Vector3(0, 135, 0);

        // Test the hand display
        display_hand.TestHand(false);

        // Create the Deck GameObject
        GameObject deck = new GameObject("Deck");
        // Set the Deck to be a child of Dealer
        deck.transform.SetParent(gameObject.transform);
        // Add the DeckDisplay Component to the deck
        DeckDisplay display_deck = deck.AddComponent<DeckDisplay>();
        // Set the scale of the deck
        deck.transform.localScale = new Vector3(13, 13, 1);

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
    }

    // Update is called once per frame
    void Update() {
        
    }
}
