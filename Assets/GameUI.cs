using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
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

    public PlayerDisplay player;
    public DealerDisplay dealer;
    public BetDisplay bet;
    
    //public ActionDisplay actions;
    Game game;

    /*
     * Jacob's SetPlayerBet Replacement (without looking at Ethan's updated code)
    */
    public void VerifyBet(int amount) {
        this.player.ClearHands();
        this.dealer.hand.ResetHand();
        game.StartRound(amount, ref this.dealer, ref this.player);
        HitUI();
        HitUI();
        DealerHitUI();
        DealerHitUI();
        if (this.player.hand.hand.GetValue() == 21 || this.dealer.hand.hand.GetValue() == 21)
        {
            EndRoundUI();
        }
        
        //[todo] display hands, both now have two cards

        //[todo] disable betting UI (text box, button) until round has ended.
    }

    public void HitUI() {
        if (!(game.isSplit)) {
            game.Hit(ref this.player, ref this.dealer);
            //[todo] display card
            int handValue = player.hand.hand.GetValue();
            if (handValue > 21)
            {
                Stand();
            }
        }
        else if (game.isSplit) {
            if (!(game.isSplitStand)) {
                game.Hit(ref this.player, ref this.dealer);
                //[todo] display card
            }
            else if (game.isSplitStand) {
                game.Hit(ref this.player, ref this.dealer);
                //[todo] display card
                int handValue = player.split.hand.GetValue();
                if (handValue > 21)
                {
                    Stand();
                }
            }
        }
    }

    public void DealerHitUI()
    {
        game.DealerHit(ref this.player, ref this.dealer);
        //[todo] display card
        int handValue = dealer.hand.hand.GetValue();
        if (handValue > 21)
        {
            Stand();
        }
    }

    // Jacob's Attempt At Coding
    public void Hit(HandDisplay hand) {
        // Have the dealer deal to the Hand (UI)
        dealer.DealCard(hand);
    }

    //trigger when DoubleDown button is clicked
    public void DoubleDownUI()
    {
        game.DoubleDown();
        //[todo] change score amount to relfect change
        HitUI();
        //ends round if hand <= 21, as Hit() ends round if hand > 21. This is just to make sure EndRound() doesn't trigger twice.
        if (game.getScore() <= 21)
        {
            EndRoundUI();
        }
        //[todo] disable rest of buttons
    }

    public void InsuranceUI()
    {

    }

    public void SplitUI()
    {
        //[todo] visually split hands
        game.Split(ref this.player);
        //[todo] disable buttons for split, double, insurance
    }

    public void Stand()
    {
        //if hand was split and first hand has been stood on OR hand was not split
        if ((game.isSplitStand && game.isSplit) || !(game.isSplit))
        {
            int handValue = player.hand.hand.GetValue();
            if (handValue > 21)
            {
                //[todo] display bust
            }
            else {
                //[todo] display stand
                //[todo] game.DealerTurn
            }
            EndRoundUI();
        }
        //if hand was split but first hand was not stood on
        else if (!(game.isSplitStand) && game.isSplit)
        {
            int handValue = player.split.hand.GetValue();
            if (handValue > 21)
            {
                //[todo] display bust
            }
            else {
                //[todo] display stand
                //[todo] game.DealerTurn
            }
            EndRoundUI();
        }
    }

    public void EndRoundUI() {
        //[todo] clear cards from screen
        game.EndRound(ref this.player.hand.hand, ref this.player.split.hand, ref this.dealer.hand.hand, ref this.dealer.deck.deck);
        //[todo] re-enable betting ui (text box, button)
    }

    // Makes sure this all happens before the first frame
    // Not sure if it would work like this, depending on how or 
    // what order Awake is called for various elements/scripts.
    void Awake() {
        this.player = UnityEngine.Object.FindFirstObjectByType<PlayerDisplay>();
        this.bet = UnityEngine.Object.FindFirstObjectByType<BetDisplay>();
        this.dealer = UnityEngine.Object.FindFirstObjectByType<DealerDisplay>();

        // Add the VerifyBet function to the BetSubmitted Event Listener
        this.bet.BetSubmitted += VerifyBet;


        /*
         * Not Certain About The Code Below
         * Primarily Because I Don't Know
         * What The Latest Commit Of This Script
         * Looks Like
        */
        // Add the Hit (UI) function to the Hit Event Listener
        this.player.PlayerHit += Hit;
        // Add the Stand (UI) function to the Stand Event Listener
        //this.player.PlayerStand += Stand;
    }

    void Reset() {
        // Create PlayerDisplay object --> Send Player to Game
        // Create DealerDisplay object --> Send DealerHand to Game; Send Deck to Game;
        
        

        //game = new Game(PlayerHand, DealerHand, Deck);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //[todo] all buttons should be disabled
    }

    // Update is called once per frame
    void Update() {
        
    }
}
