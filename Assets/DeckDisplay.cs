using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    // The Deck object
    public DeckModelSO deck;

    // The image that represents the deck
    private SpriteRenderer image;

    /**
            // Load the Card Backside image
            _image.sprite = Resources.Load<Sprite>("Cards/backside");

            // Draw the sprite, keeping its natural dimensions
            _image.drawMode = SpriteDrawMode.Simple;

            // Load the proper shader for drawing the sprite
            _image.material = Resources.Load<Material>("Materials/Unlit_VectorGradient");
    */

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        // Delete any existing children
        foreach (Transform child in gameObject.transform) {
            // Delete the child
            GameObject.DestroyImmediate(child.gameObject);
        }

        deck = ScriptableObject.CreateInstance<DeckModelSO>();
        deck.Initialize();
    
        // Make the Deck GameObject act as a UI Panel
        SpriteRenderer image = gameObject.AddComponent<SpriteRenderer>();

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
        image.sprite = Resources.Load<Sprite>("Cards/backside");
        
        // Draw the sprite, keeping its natural dimensions
        image.drawMode = SpriteDrawMode.Simple;

        // Load the proper shader for drawing the sprite
        image.material = Resources.Load<Material>("Materials/Unlit_VectorGradient");
    }
}
