using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*  Function to load any SinglePlayer specific options  */
    public void LoadSinglePlayer() {
        Debug.Log("\tMainMenu.LoadSinglePlayerGame()");
        Debug.Log("\tDoing stuff for Single Player mode...");

        Debug.Log("\tLoading Scene: 'General Game'");
        SceneManager.LoadSceneAsync("General Game");
    }
    
    /*  Function to load any MultiPlayer specific options  */
    public void LoadMultiPlayer() {
        Debug.Log("\tMainMenu.LoadMultiPLayer()");
        Debug.Log("\tDoing stuff for Multi Player mode...");

        Debug.Log("\tLoading Scene: 'General Game'");
        SceneManager.LoadSceneAsync("General Game");
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
