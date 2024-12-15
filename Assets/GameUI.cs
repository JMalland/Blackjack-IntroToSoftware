using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour {
    /*TODO:
     * HitUI()
     * StandUI()
     * InsuranceUI()
     * DoubleDownUI()
     * SplitUI()
     * ----Above fxns will activate visuals when the button is hit, will also activate associated fxn in GameScript.cs----
     * ----Visuals include cards being moved around (such as into two hands for a split), bet doubling, button pressing inwards, etc----
     * ----Should only be allowed to be triggered if certain criteria are met (i.e. double down if player has not hit yet)----
     * 
     * DealerUI() - Activates when it is dealer's turn. Visually will show their cards moving and being revealed. Also triggers associated GameScript fxn
    */

    
    Game currentGame = new Game();

    public void SetPlayerBet() {
        int bet = Int32.Parse("0");
        currentGame.StartRound(bet);
    }

    public void EndRoundUI() {
        //[todo] clear cards from screen
        currentGame.EndRound();
    }

    // Initialize or reset the game, however
    void Initialize() {
        return;
        // Create new GameObject("Player")
        // Create new GameObject("Dealer")
        // Create new GameObject("Bet")
        
        
        // Add Component<PlayerDisplay> to "Player"
        // Add Component<DealerDisplay> to "Dealer"
        // BetDisplay class, for less Bet GameObject retrieval and translation
        // Will not need a BetModelSO object. 
        // Bet button onclick should correspond to a BetDisplay private function "SubmitBet"
        // SubmitBet function should trigger the BetSubmitted action event, passing the Bet value to the event
        // Add Component<BetDisplay> to "Bet"
        // Set BetDisplay component BetSubmitted action to trigger the StartRound function
        // StartRound function should decrement from Player.player.score -- if not high enough, should do nothing
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //GameObject Bet = // Get GameObject with name BET
        //BetInput = // Get TMP_InputField Child from Bet

        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
