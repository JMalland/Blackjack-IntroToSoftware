using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    // The Card object
    public CardModelSO card;

    public SpriteRenderer spriteRenderer;

    // Set a specific Card to be displayed
    public void Initialize(CardModelSO newCard) {
        // Set the new Card
        card = newCard;

        // Set the Sprite to be rendered
        spriteRenderer.sprite = Resources.Load<Sprite>("Cards/"+newCard.rank.ToLower()+"_of_"+newCard.suit.ToLower());
    }

    // Reset is called every time a component is added, or reset. This way changes appear in the editor.
    void Reset() {
        // Get or create the SpriteRenderer
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>() ?? gameObject.GetComponent<SpriteRenderer>();
        
        // Draw the sprite, keeping its natural dimensions
        spriteRenderer.drawMode = SpriteDrawMode.Simple;

        // Load the proper shader for drawing the sprite
        spriteRenderer.material = Resources.Load<Material>("Materials/Unlit_VectorGradient");

        // Set the card to be 13*13 scale.
        gameObject.transform.localScale = new Vector3(13, 13, 1);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Display a random card
    }

    // Update is called once per frame
    void Update() {
        // Don't display anything if the card is null
        if (card == null) {
            Debug.LogError("There is no Card set.");
            return;
        }

        // Set the Sprite to be rendered
        spriteRenderer.sprite = Resources.Load<Sprite>("Cards/"+card.rank.ToLower()+"_of_"+card.suit.ToLower());
    }
}
