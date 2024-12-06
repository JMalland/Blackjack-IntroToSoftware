using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Game : MonoBehaviour
{
    private int score;
    private int currentBet;
    HandModelSO currentHand = new HandModelSO();
    HandModelSO dealerHand = new HandModelSO();

    private int modifier;
    private bool fccEnabled;
    private bool jokerEnabled;
    private bool lockedacesEnabled;
    private bool lockedbetEnabled;

    //Triggered at beginning of each round. Removes bet from player score. 
    public void StartRound(int playerBet) 
    {
        this.currentBet = playerBet;
        this.score -= playerBet;
    }

    //Triggered at end of each round if player won. Updates their score with their winnings.
    public void UpdatePoints(bool blackjack, int playerBet)
    {
        if (blackjack)
        {
            this.score += (playerBet * 2);
            this.score += (playerBet / 2);
        }
        else
        {
            this.score += (playerBet * 2);
        }
    }

    //EndRound: Resets hand, bet, and adds winnings if applicable.
    public void EndRound()
    {
        bool blackjack = isBlackJack();
        int result = roundResult();

        if (result == 1)
        {
            UpdatePoints(blackjack, this.currentBet);
        }
        else if (result == 2)
        {
            this.score += this.currentBet;
        }
        // [todo] clear currentHand, reset deck
        currentBet = 0;

    }

    //isBlackJack: Determines if hand is a valid blackjack. Valid blackjacks are 21 off deal. Determines if a hand has 1 Ace and 1 Face card/10 card
    public bool isBlackJack()
    {
        CardModelSO[] playerHand = currentHand.GetCards();
        bool ace = false;
        bool face = false;

        for (int i = 0; i < playerHand.Count(); i++)
        {
            string card = playerHand[i].ToString();
            if (card.EndsWith("A"))
            {
                if (!ace)
                {
                    ace = true;
                }
                else if (ace)
                {
                    ace = false;
                    i = playerHand.Count();
                }
            }
        }

        for (int i = 0; i < playerHand.Count(); i++)
        {
            string card = playerHand[i].ToString();
            if (card.EndsWith("J") || card.EndsWith("Q") || card.EndsWith("K") || card.Substring(card.Length - 2) == "10")
            {
                if (!face)
                {
                    face = true;
                }
                else if (face)
                {
                    face = false;
                    i = playerHand.Count();
                }
            }
        }

        if (ace && face)
        {
            return true;
        }
        else return false;
    }

    //roundResult: Determines if player has won or not. 1 = win, 0 = loss, 2 = push
    public int roundResult()
    {
        CardModelSO[] playersHand = currentHand.GetCards();
        CardModelSO[] dealersHand = dealerHand.GetCards();
        int playerScore = 0;
        int playerAces = 0;
        int dealerAces = 0;
        int dealerScore = 0;

        //Evaluates Player's hand
        for (int i = 0; i < playersHand.Count(); i++)
        {
            int cardValue = playersHand[i].RankToInt();
            playerScore += cardValue;
            if (cardValue == 11)
            {
                playerAces += 1; 
            }
            while (playerScore > 21 && playerAces > 0)
            {
                playerScore -= 10;
                playerAces -= 1;
            }
        }
        //Evaluates Dealer's hand
        for (int i = 0; i < dealersHand.Count(); i++)
        {
            int cardValue = dealersHand[i].RankToInt();
            dealerScore += cardValue;
            if (cardValue == 11)
            {
                dealerAces += 1;
            }
            while (playerScore > 21 && playerAces > 0)
            {
                dealerScore -= 10;
                dealerAces -= 1;
            }
        }
        if (playerScore > dealerScore && playerScore < 22 || dealerScore > 21 && playerScore < 22)
        {
            return 1;
        }
        else if (dealerScore > playerScore && dealerScore < 22 || playerScore > 21 && dealerScore < 22)
        {
            return 0;
        }
        else if (playerScore == dealerScore && playerScore < 22 && dealerScore < 22)
        {
            return 2;
        }
        else return 2;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.score = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
