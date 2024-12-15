using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardModelSO", menuName = "New Playing Card")]
public class CardModelSO : ScriptableObject
{
    // The suit of the card (DIAMOND, HEART, CLUB, SPADE)
    public string suit;
    
    // The string value of the card (2,3,4 ... A, J, Q, K)
    public string rank;
    
    // Override the ToString method
    override public string ToString() {
        return(rank + "-" + suit.ToUpper());
    }

    public int RankToInt()
    {
        if (this.rank == "J" || this.rank == "Q" || this.rank == "K")
        {
            return 10;
        }
        else if (this.rank == "A")
        {
            return 11;
        }
        else
        {
            return Int32.Parse(this.rank);
        }
    }
}
