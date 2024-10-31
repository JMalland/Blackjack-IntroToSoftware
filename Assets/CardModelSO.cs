using UnityEngine;

[CreateAssetMenu(fileName = "CardModelSO", menuName = "New Playing Card")]
public class CardModelSO : ScriptableObject
{
    // The suit of the card
    public string cardSuit;
    
    // The string value of the card (2,3,4 ... A, J, Q, K)
    public string cardType;
    
    // The card image
    public Sprite frontCardImage;

    // The back card image
    public Sprite backCardImage;

    // Override the ToString method
    override public string ToString() {
        return(cardType + "-" + cardSuit.ToUpper());
    }
}
