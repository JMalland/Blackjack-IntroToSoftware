using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Game : MonoBehaviour
{
    /*TODO:
     * Insurance()
     * DealerTurn() - This will be when the dealer reveals their card and draws if necessary
    */
    private int score;
    private int currentBet;
    private int sideBet;
    private int insuranceBetAmount = 0;
    private bool insuranceBetPlaced = false;
    public bool isSplit;
    public bool isSplitStand;



    //Triggered at beginning of each round. Removes bet from player score. 
    public void StartRound(int playerBet, ref DealerDisplay dealer, ref PlayerDisplay player) 
    {
        this.currentBet = playerBet;
        this.score -= playerBet;
        this.isSplit = false;
        Hit(ref player, ref dealer);
        Hit(ref player, ref dealer);
        DealerHit(ref player, ref dealer);
        DealerHit(ref player, ref dealer);
        //[todo] offer insurance if applicable
    }

    public void Hit(ref PlayerDisplay player, ref DealerDisplay dealer)
    {
        CardModelSO newCard = dealer.deck.NextCard();
        int handValue = 0;
        if (!(this.isSplitStand))
        {
            player.hand.AddCard(newCard);
            handValue = player.hand.GetValue();
        }
        else if (this.isSplitStand)
        {
            player.splitHand.AddCard(newCard);
            handValue = player.splitHand.GetValue();
        }
        dealer.mostRecentCard = newCard;
        if (handValue > 21)
        {
            EndRound(ref player.hand, ref player.splitHand, ref dealer.dealerHand, ref dealer.deck);
        }
    }

    //Doubles player's bet and forces them to draw one card before force-ending the round.
    public void DoubleDown(ref PlayerDisplay player, ref DealerDisplay dealer, ref CardModelSO mostRecentCard)
    {
        this.sideBet = this.currentBet;
        this.score -= this.sideBet;
        Hit(ref player, ref dealer);
        //ends round if hand <= 21, as Hit() ends round if hand > 21. This is just to make sure EndRound() doesn't trigger twice.
        if (this.score <= 21)
        {
            EndRound(ref player.hand, ref player.splitHand, ref dealer.dealerHand, ref dealer.deck);
        }

    }

    public void Split(ref HandModelSO playerHand, ref HandModelSO splitHand, ref DeckModelSO deck, ref CardModelSO mostRecentCard)
    {
        bool canSplit = playerHand.CanSplit();
        if (canSplit)
        {
            this.isSplit = true;
            CardModelSO newCard = playerHand.Split();
            splitHand.AddCard(newCard);
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
    //EndRound: Resets hands (player, dealer), deck, bet, and adds winnings if applicable.
    public void EndRound(ref HandModelSO playerHand, ref HandModelSO splitHand, ref HandModelSO dealerHand, ref DeckModelSO deck)
    {
        //if splitting did not occur
        if (!(this.isSplitStand) && !(this.isSplit))
        {
            bool blackjack = isBlackJack(playerHand);
            int result = roundResult(ref playerHand, ref dealerHand);

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

            this.currentBet = 0;
            this.sideBet = 0;
            this.isSplit = false;
            this.isSplitStand = false;
            this.insuranceBetAmount = 0;
            this.insuranceBetPlaced = false;
            deck = ScriptableObject.CreateInstance<DeckModelSO>();
        }

        //if splitting occurred and the first split hand has concluded, allowing game to evaluate.
        else if (this.isSplitStand && this.isSplit)
        {
            //evaluates first hand
            bool blackjack = isBlackJack(playerHand);
            int result = roundResult(ref playerHand, ref dealerHand);

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
            //evaluates second hand (hand born from split)
            int splitResult = roundResult(ref splitHand, ref dealerHand);
            bool isSplitBlackjack = isBlackJack(splitHand);
            if (splitResult == 1)
            {
                UpdatePoints(isSplitBlackjack, this.sideBet);
            }
            else if (splitResult == 2)
            {
                this.score += this.sideBet;
            }

            this.currentBet = 0;
            this.sideBet = 0;
            this.isSplit = false;
            this.isSplitStand = false;
            this.insuranceBetAmount = 0;
            this.insuranceBetPlaced = false;
            deck = ScriptableObject.CreateInstance<DeckModelSO>();
        }
        //splitting has occured, but first hand is only now being settled. Fxn below indicates that
        //the first hand is now settled, allows the next hand to begin.
        else if (!(this.isSplitStand) && this.isSplit)
        {
            this.isSplitStand = true;
        }
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
    public int roundResult(ref HandModelSO playerHand, ref HandModelSO dealerHand)
    {
        int playerScore = playerHand.GetValue();
        int dealerScore = dealerHand.GetValue();

        if (playerScore > dealerScore && playerScore < 22 || dealerScore > 21 && playerScore < 22)
        {
            Debug.Log("Player wins!");
            return 1;
        }
        else if (dealerScore > playerScore && dealerScore < 22 || playerScore > 21 && dealerScore < 22)
        {
            Debug.Log("Player loses!");
            return 0;
        }
        else if (playerScore == dealerScore && playerScore < 22 && dealerScore < 22)
        {
            Debug.Log("Round is pushed!");
            return 2;
        }
        else return 2;

    }
     //Insurance(): determines if player wants to use insurance and calculate the bet amount
    public void Insurance(bool playerUsesInsurance, ref PlayerDisplay player, ref DealerDisplay dealer){
        if(dealer.dealerHand.GetCard(0).ToString().EndsWith("A")){
            //[todo] prompt for insurance from player!
            if(playerUsesInsurance){
                insuranceBetAmount = currentBet / 2;
                score -= insuranceBetAmount;
                this.insuranceBetPlaced = true;
            }
        }
        else{
            Debug.Log("Insurance is not available, dealer's up card is not an Ace.");
        }
    }

    public void DealerHit(ref PlayerDisplay player, ref DealerDisplay dealer)
    {
        CardModelSO newCard = dealer.deck.NextCard();
        int handValue = 0;
        dealer.dealerHand.AddCard(newCard);
        handValue = dealer.dealerHand.GetValue();
        dealer.mostRecentCard = newCard;
        if (handValue > 21)
        {
            EndRound(ref player.hand, ref player.splitHand, ref dealer.dealerHand, ref dealer.deck);
        }
    }
    public void DealerTurn(ref PlayerDisplay player, ref DealerDisplay dealer){
        Debug.Log("Dealer reveals their second card");
        while(dealer.dealerHand.GetValue() < 17){
            DealerHit(ref player, ref dealer);
            Debug.Log("Dealer draws a card.");
        }
        EndRound(ref player.hand, ref player.splitHand, ref dealer.dealerHand, ref dealer.deck);
        /*
        else{
            int result = roundResult(currentHand);
            if (this.insuranceBetPlaced){
                if (isBlackJack(dealerHand)){
                    Debug.Log("Dealer has a Blackjack! Insurance wins.");
                    score += insuranceBetAmount * 2;
                    
                }
                else{
                    Debug.Log("Dealer does not have a Blackjack. Insurance lost.");
                }
                
            }
            EndRound(roundResult());
        }
        */
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
