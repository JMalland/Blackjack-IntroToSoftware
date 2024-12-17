using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    public Boolean debug = false;
    
    // The Deck object
    public DeckModelSO deck;

    // The image that represents the deck
    private SpriteRenderer image;

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

    public CardDisplay DrawCard() {
        // Create a GameObject to be the card (UI)
        GameObject cardObject = new GameObject("Drawn Card");
        
        // Create the CardDisplay component
        CardDisplay card = cardObject.AddComponent<CardDisplay>();
        // Initialize it with the drawn card
        card.Initialize(deck.DrawCard());

        // Make the card a child of (this) DeckDisplay component
        cardObject.transform.SetParent(gameObject.transform);

        // Set the position of the drawn card
        Transform transform = cardObject.transform;
        transform.localPosition = new Vector3(180, 150, 1);

        // Return the CardDisplay object
        return(card);
    }

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        KillChildren();

        deck = ScriptableObject.CreateInstance<DeckModelSO>();
        deck.Initialize();
    
        // Make the Deck GameObject act as a UI Panel
        SpriteRenderer image = gameObject.AddComponent<SpriteRenderer>() ?? gameObject.GetComponent<SpriteRenderer>();

        // Draw the sprite, keeping its natural dimensions
        image.drawMode = SpriteDrawMode.Simple;

        // Load the proper shader for drawing the sprite
        image.material = Resources.Load<Material>("Materials/Unlit_VectorGradient");

        image.sprite = Resources.Load<Sprite>("Cards/backside");

        // WILL HAVE TO SET THE DEFAULT DECK TRANSFORM PROPERTIES
        // FOR MANAGING LOCATION WITHIN AN OBJECT
        // gameObject.transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        // Load the Deck Sprite
    }
}
