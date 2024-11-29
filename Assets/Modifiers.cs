using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Modifiers : MonoBehaviour
{
    //Hand triggers this fxn, pays out to player like normal.
    public int FiveCardCharlie(int playerBet)
    {
        return (playerBet * 2);
    }

    //Hand triggers this fxn, pays out blackjack to player.
    public int JokerBlackjack(List<CardModelSO> hand, int playerBet)
    {
        int numOfJokers = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            string card = hand[i].ToString();
            if (card == "JOKER-0")
            {
                numOfJokers++;
            }
        }
        if (numOfJokers == 1)
        {
            int score = 0;
            score += (playerBet * 2);
            score += (playerBet / 2);
            return score;
        }
        if (numOfJokers == 2)
        {
            return (playerBet * 5);
        }
        else return playerBet;
    }

    //If active, player chooses if their ace is 1 or 11. Cannot change that card afterwards.
    public int LockedAces()
    {
        //query the player on what their ace should be
        return 11;
    }

    public void LockedBets()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
