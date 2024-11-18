using UnityEngine;

public class Game : MonoBehaviour
{
    private int score;
    private int modifier;
    private bool fccEnabled;
    private bool jokerEnabled;
    private bool lockedacesEnabled;
    private bool lockedbetEnabled;

    public void GetPlayerBet(int playerBet)
    {

    }
    //Triggered at beginning of each round. Removes bet from player score. 
    public void BetPoints(int playerBet) 
    {
        this.score -= playerBet;
    }

    //Triggered at end of each round if player won. Updates their score with their winnings.
    public void UpdatePoints(bool blackjack, int playerBet)
    {
        if (blackjack)
        {
            this.score += (playerBet * 2);
        }
        else
        {
            this.score += (playerBet * 2);
            this.score += (playerBet / 2);
        }
    }
    //Hand triggers this fxn, pays out to player like normal.
    public void FiveCardCharlie(int playerBet)
    {
        this.score += (playerBet * 2);
    }

    //Hand triggers this fxn, pays out blackjack to player.
    public void JokerBlackjack(int numOfJokers, int playerBet)
    {
        if (numOfJokers == 1)
        {
            this.score += (playerBet * 2);
            this.score += (playerBet / 2);
        }
        if (numOfJokers == 2)
        {
            this.score += (playerBet * 5);
        }
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
