using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    public DeckModelSO deck;

    // The backside image of the top card
    public SpriteRenderer _image;


    void LoadImage() {
        _image = gameObject.GetComponent<SpriteRenderer>();

        // No SpriteRenderer component exists
        if (_image == null)
        {
            // Create the SpriteRenderer
            gameObject.AddComponent<SpriteRenderer>();
            // Get the SpriteRenderer
            _image = gameObject.GetComponent<SpriteRenderer>();
        }

        if (_image.sprite == null) {
            // Load the Card Backside image
            _image.sprite = Resources.Load<Sprite>("Cards/backside");

            // Draw the sprite, keeping its natural dimensions
            _image.drawMode = SpriteDrawMode.Simple;

            // Load the proper shader for drawing the sprite
            _image.material = Resources.Load<Material>("Materials/Unlit_VectorGradient");

            Debug.Log("Loaded Sprite: " + _image.sprite);
        }

        if (_image.sprite == null) {
            Debug.LogError("Shit Hath Happened");
        }
    }

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset()
    {
        deck = ScriptableObject.CreateInstance<DeckModelSO>();
        deck.InitializeDeck();

        LoadImage();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
        LoadImage();
    }

    // Update is called once per frame
    void Update()
    {
        LoadImage();
    }
}
