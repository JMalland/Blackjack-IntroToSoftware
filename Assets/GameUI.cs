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

    public TMP_InputField BetInput;
    Game currentGame = new Game();
    HandDisplay uiHand = new HandDisplay();
    HandDisplay uiSplitHand = new HandDisplay();

    public void SetPlayerBet()
    {
        string stringBet = BetInput.text;
        int bet = Int32.Parse(stringBet);
        currentGame.StartRound(bet);

        //[todo] disable betting UI (text box, button) until round has ended.
    }

    public void HitUI()
    {
        if (!(currentGame.isSplit))
        {
            currentGame.Hit(currentGame.currentHand);
            uiHand.hand.AddCard(currentGame.GetMostRecentCard());
            int handValue = currentGame.currentHand.GetValue();
            if (handValue > 21)
            {
                Stand();
            }
        }
        else if (currentGame.isSplit)
        {
            if (!(currentGame.isSplitStand))
            {
                currentGame.Hit(currentGame.currentHand);
                uiHand.hand.AddCard(currentGame.GetMostRecentCard());
            }
            else if (currentGame.isSplitStand)
            {
                currentGame.Hit(currentGame.splitHand);
                uiHand.hand.AddCard(currentGame.GetMostRecentCard());
                int handValue = currentGame.currentHand.GetValue();
                if (handValue > 21)
                {
                    Stand();
                }
            }
        }
    }

    public void Stand()
    {
        int handValue = currentGame.currentHand.GetValue();
        if (handValue > 21)
        {
            //[todo] display bust
        }
        else
        {
            //[todo] display stand
            //[todo] currentGame.DealerTurn
        }
        EndRoundUI();
    }

    public void EndRoundUI()
    {
        //[todo] clear cards from screen
        currentGame.EndRound();
        uiHand.hand.ResetHand();
        uiSplitHand.hand.ResetHand();
        //[todo] re-enable betting ui (text box, button)
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //[todo] all buttons should be disabled
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
