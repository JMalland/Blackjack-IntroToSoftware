using UnityEditor.VersionControl;
using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "DealerModelSO", menuName = "New Dealer")]
public class DealerModelSO : PlayerModelSO
{
    // The player's name
    private DeckModelSO deck;

    public void Deal(PlayerModelSO player) {
        // Stop player from receiving cards when PlayerStand event is triggered
        player.PlayerStand += NoMoreCards;
        
        // Player is given cards when the PlayerHit event is triggered
        player.PlayerHit += GiveCard;
    }

    // Give the card to the player
    // Should run every time the player chooses Hit
    private void GiveCard(HandModelSO hand) {
        // Give the player their  card
    }

    private void NoMoreCards(PlayerModelSO player) {
        // Remove the GiveCard method from the player's event listener
        player.PlayerHit -= GiveCard;

        // Remove the NoMoreCards method from the player's event listener
        player.PlayerStand -= NoMoreCards;
    }

    public override void Initialize() {
        base.Initialize();
        this.deck = ScriptableObject.CreateInstance<DeckModelSO>();
    }
}
