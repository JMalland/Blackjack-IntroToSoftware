using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ToggleButton : MonoBehaviour {
    public GameObject SelfGameObject;
    public Sprite AlternateImage;
    public Vector3 AlternateImageScale;
    public Color AlternateColor;

    // Takes in a modifier type, and toggles the button
    public void Toggle(string type) {
        // Get the button and the image
        UnityEngine.UI.Image button = SelfGameObject.transform.GetComponent<UnityEngine.UI.Image>();
        SpriteRenderer image = SelfGameObject.transform.Find("Image").GetComponent<SpriteRenderer>();

        // Disable the modifier
        Debug.Log("\tDisabling Modifier: '" + type +"'");

        // Load the alternate icon
        Sprite tempSprite = image.sprite;
        image.sprite = AlternateImage;
        AlternateImage = tempSprite;

        // Adjust the scale of the icon
        Vector3 tempScale = image.transform.localScale;
        image.transform.localScale = AlternateImageScale;
        AlternateImageScale = tempScale;
        
        // Change the button color
        Color tempColor = button.color;
        button.color = AlternateColor;
        AlternateColor = tempColor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
