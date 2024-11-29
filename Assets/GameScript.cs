using UnityEngine;

public class Game : MonoBehaviour
{
    private int score;
    private int modifier;
    private bool fccEnabled;
    private bool jokerEnabled;
    private bool lockedacesEnabled;
    private bool lockedbetEnabled;

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
