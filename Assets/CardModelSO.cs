using UnityEngine;

[CreateAssetMenu(fileName = "CardModelSO", menuName = "New Playing Card")]
public class CardModelSO : ScriptableObject
{
    // The suit of the card
    public string suit;
    
    // The string value of the card (2,3,4 ... A, J, Q, K)
    public string rank;
    
    // Override the ToString method
    override public string ToString() {
        return(rank + "-" + suit.ToUpper());
    }
}
