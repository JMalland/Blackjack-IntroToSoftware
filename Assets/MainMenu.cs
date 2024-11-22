using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*  Function to load any SinglePlayer specific options  */
    public void SinglePlayer() {
        Debug.Log("\tLoading Scene: 'General Game'");
        SceneManager.LoadSceneAsync("General Game");
    }
    
    /*  Function to load any MultiPlayer specific options  */
    public void MultiPlayer() {
        Debug.Log("\tLoading Scene: 'General Game'");
        SceneManager.LoadSceneAsync("General Game");
    }

    public void HowToPlay() {
        Debug.Log("\tLoading Scene: 'How To Play'");
        SceneManager.LoadSceneAsync("How To Play");
    }

    public void Settings() {
        Debug.Log("\tLoading Scene: 'Settings'");
        SceneManager.LoadSceneAsync("Settings");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
