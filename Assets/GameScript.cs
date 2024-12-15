using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Game : MonoBehaviour
{
    /*TODO:
     * Hit() - DONE(EP)
     * Stand() - DONE(EP)
     * ^actually, we'll just use EndRound() instead of making a seperate stand function, since it'll functionally do the same thing.
     * Insurance()
     * DoubleDown()
     * Split()
     * DealerTurn() - This will be when the dealer reveals their card and draws if necessary
    */
    private int score;
    private int currentBet;
    private int sideBet;
    private bool isSplit;
    HandModelSO currentHand = new HandModelSO();
    HandModelSO splitHand = new HandModelSO();
    HandModelSO dealerHand = new HandModelSO();
    DeckModelSO deck = new DeckModelSO();


    //Triggered at beginning of each round. Removes bet from player score. 
    public void StartRound(int playerBet) 
    {
        this.currentBet = playerBet;
        this.score -= playerBet;
        this.isSplit = false;
        Hit(this.dealerHand);
        Hit(this.dealerHand);
        Hit(this.currentHand);
        Hit(this.currentHand);
    }

    public void Hit(HandModelSO hand)
    {
        CardModelSO newCard = deck.NextCard();
        hand.AddCard(newCard);
        int handValue = EvaluateHandValue(hand);
        if (score > 21)
        {
            EndRound();
        }
    }

    //Doubles player's bet and forces them to draw one card before force-ending the round.
    public void DoubleDown()
    {
        this.sideBet = this.currentBet;
        this.score -= this.sideBet;
        Hit(this.currentHand);
        //ends round if hand <= 21, as Hit() ends round if hand > 21. This is just to make sure EndRound() doesn't trigger twice.
        if (score <= 21)
        {
            EndRound();
        }

    }

    public void Split()
    {
        bool canSplit = this.currentHand.CanSplit();
        if (canSplit)
        {
            this.isSplit = true;
            CardModelSO newCard = this.currentHand.Split();
            this.splitHand.AddCard(newCard);
        }

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
    //Calculates Player's hand value
    public int EvaluateHandValue(HandModelSO hand)
    {
        CardModelSO[] playersHand = hand.GetCards();
        int playerScore = 0;
        int playerAces = 0;

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

        return playerScore;
    }
    //EndRound: Resets hands (player, dealer), deck, bet, and adds winnings if applicable.
    public void EndRound()
    {
        bool blackjack = isBlackJack(this.currentHand);
        int result = roundResult(this.currentHand);

        if (result == 1)
        {
            UpdatePoints(blackjack, this.currentBet);
            if (!(this.isSplit))
            {
                UpdatePoints(blackjack, this.sideBet);
            }
        }
        else if (result == 2)
        {
            this.score += this.currentBet;
            if (!(this.isSplit))
            {
                this.score += this.sideBet;
            }
        }

        if (this.isSplit)
        {
            int splitResult = roundResult(this.splitHand);
            bool isSplitBlackjack = isBlackJack(this.splitHand);
            if (splitResult == 1)
            {
                UpdatePoints(isSplitBlackjack, this.sideBet);
            }
            else if (splitResult == 2)
            {
                this.score += this.sideBet;
            }
        }


        // [todo] clear currentHand, reset deck
        this.currentBet = 0;
        this.sideBet = 0;
        this.isSplit = false;
        //[todo] RESET PLAYER HAND
        //[todo] RESET SPLIT HAND
        //[todo] RESET DEALER HAND
        //[todo] RESET DECK

    }

    //isBlackJack: Determines if hand is a valid blackjack. Valid blackjacks are 21 off deal. Determines if a hand has 1 Ace and 1 Face card/10 card
    public bool isBlackJack(HandModelSO hand)
    {
        CardModelSO[] playerHand = hand.GetCards();
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
    public int roundResult(HandModelSO hand)
    {
        CardModelSO[] playersHand = hand.GetCards();
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
