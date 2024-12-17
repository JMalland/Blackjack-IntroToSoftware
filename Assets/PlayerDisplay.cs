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

    //[Ethan] I have not deleted PlayerModelSO just in case, but I have removed it from this class, and this will act as a combination of both.
    //^the same goes for DealerModelSO

    //I have not messed with any of the functions as you know them better than I do, and, as such, I have not moved...
    //any of the functions from the ModelSO classes to the Display classes. I figured you would know better...
    //than I with how to proceed with those.

    // EventListeners to trigger Hit and Stand event responses (Not sure if has a use yet)
    public event Action<PlayerDisplay> PlayerStand;

    // player name
    //private string name;
    public HandDisplay hand;
    public HandDisplay split;

    private String active_hand = "hand";
    //public HandModelSO hand = ScriptableObject.CreateInstance<HandModelSO>();
    //public HandModelSO split = ScriptableObject.CreateInstance<HandModelSO>();

    // The array of Hand objects

    // The GameObject that stores the displayed hands
    public GameObject display_hands;
    
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

    // Get the player's active hand.
    // Can be Hand or SplitHand.
    public HandDisplay GetCurrentHand() {
        // The primary hand is active
        if (active_hand == "hand") {
            return(hand);
        }
        // The split hand is active
        else if (active_hand == "split") {
            return(split);
        }
        // There is no active hand
        return null;
    }

    public void ClearHands() {
        // Reset the primary HandModelSO
        this.hand.ResetHand();

        // Check if a split hand exists
        if (this.split) {
            // Reset the split HandModelSO
            this.split.ResetHand();
        }
    }

        // Send out the PlayerHit event, with the hand that was hit on
        // GameUI.HitUI(HandDisplay)
        //      DealerDisplay.DealCard(HandDisplay);
        //          // Draws the card
        //          card = DeckDisplay.DrawCard();
        //              // Creates & returns CardDisplay component 
        //          // Sends the card to the Hand
        //          HandDisplay.AddCard(card);
        /*              // Animate card movement to middle of hand
                        // Add card to UI Hand
                        // ??Update UI Hand Value Display??
                    // Adds the CardModelSO object to the HandModelSO (stored in HandDisplay)
                    HandModelSO.AddCard(CardModelSO)
        */

    public void Stand() {
        // If no other hand to choose from, set the active hand to null (There is no active hand)
        // Otherwise, set the active hand to "split"
        this.active_hand = this.active_hand == "hand" && this.split ? "split" : null;

        // Send out the PlayerStand event, with the player that was stood on
        PlayerStand.Invoke(this);
        // ||      ||
        // \/      \/
        // Prevent Action Buttons From Being Enabled
    }

    // The hand was split
    void Split() {
        // Cannot split the hand
        if (!this.hand.hand.CanSplit()) {
            Debug.Log("PlayerDisplay.Split(): Cannot Split Hand");
            return;
        }

        Debug.Log("PlayerDisplay.Split(): Can Split Hand");

        this.split = CreateHand("Hand 2");
        
        // Add the split card to the hand
        //this.split.AddCard(card);

        //hand.TestHand(false);
    }


    HandDisplay CreateHand(String objectName) {
        // Create a HandModelSO object
        HandModelSO hand = ScriptableObject.CreateInstance<HandModelSO>();

        // Create the Hand GameObject
        GameObject handObject = new GameObject(objectName);

        // Add the HandDisplay to PlayerDisplay's GameObject
        handObject.transform.SetParent(gameObject.transform);
        
        // Add the HandDisplay Component to the hand
        return(handObject.AddComponent<HandDisplay>());
    }


    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        Debug.Log("Resetting");
        
        // Delete any existing children
        KillChildren();

        // Get the RectTransform component (to act as a UI.Panel component)
        RectTransform rect = gameObject.AddComponent<RectTransform>() ?? gameObject.GetComponent<RectTransform>();
        
        // Set the width and height
        rect.sizeDelta = new Vector2(2*(52 + 12*5) + 30, 82 + 12*5 + 24);
        // Set the anchored position
        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 0);
        rect.pivot = new Vector2(0.5f, 0.5f);
        // Set the relative position
        // WEIRD: For whatever reason, when Y value is set to 312.5, it displays as 800 someodd
        //        When set to -227.5f, it displays as 312.5. ... IDK why.
        //        312.5 is the desired Y value for the player (One Player)
        rect.localPosition = new Vector2(0, -227.5f);

        // Set the display layout of the Player object.
        HorizontalLayoutGroup self_layout = gameObject.AddComponent<HorizontalLayoutGroup>() ?? gameObject.GetComponent<HorizontalLayoutGroup>();
        // Set the child alignment
        self_layout.childAlignment = TextAnchor.MiddleCenter;

        // Create the first hand
        this.hand = CreateHand("Hand 1");
        
        // Add the Split function to the HandModel's SplitHand event
        //this.hand.SplitHand += Split;
        
        // Test the hand display, and splitting
        //this.hand.TestHand(false);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
    }

    // Update is called once per frame
    void Update() {
        
    }
}
