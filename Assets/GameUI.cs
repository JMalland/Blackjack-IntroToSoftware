using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_InputField BetInput;
    Game currentGame = new Game();

    public void SetPlayerBet()
    {
        string stringBet = BetInput.text;
        int bet = Int32.Parse(stringBet);
        currentGame.StartRound(bet);
    }

    public void EndRoundUI()
    {
        //[todo] clear cards from screen
        currentGame.EndRound();
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
