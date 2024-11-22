using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayer : MonoBehaviour
{
    /*  Function to load any SinglePlayer specific options  */
    public void Casual() {
        Debug.Log("\tLoading Scene: 'General Game'");
        SceneManager.LoadSceneAsync("General Game");
    }

    /*  Function to load any MultiPlayer specific options  */
    public void MultiPlayer() {
        Debug.Log("\tLoading Scene: 'Modifiers'");
        SceneManager.LoadSceneAsync("Modifiers");
    }

    // Exit to the main menu
    public void ExitToMain() {
        Debug.Log("\tLoading Scene: 'Main Menu'");
        SceneManager.LoadSceneAsync("Main Menu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
