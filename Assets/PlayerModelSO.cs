using UnityEditor.VersionControl;
using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "PlayerModelSO", menuName = "New Player")]
public class PlayerModelSO : ScriptableObject
{
    // Events that can be listened for by the Player display manager
    public event Action<HandModelSO> PlayerHit;
    public event Action<PlayerModelSO> PlayerStand;
    
    // The player's name
    private string name;

    // The player's Hand
    private HandModelSO hand;

    // Handle the PlayerHit event
    public void Hit() {
        // Invoke the PlayerHit event
        PlayerHit.Invoke(this.hand);
    }

    // Handle the PlayerStand event
    public void Stand() {
        // Invoke the PlayerStand event
        PlayerStand.Invoke(this);
    }

    // Get the player's name
    public string GetName() {
        return name;
    }

    // Initialize the player's Hand
    public virtual void Initialize() {
        hand = ScriptableObject.CreateInstance<HandModelSO>();
    }
}
