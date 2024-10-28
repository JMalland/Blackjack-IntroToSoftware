using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    /*  Function to exit game to Main Menu  */
    public void ExitToMain()
    {
        Debug.Log("\tGameMenu.ExitToMain()");
        Debug.Log("\tDoing stuff to quit the game...");

        Debug.Log("\tLoading Scene: 'Main Menu'");
        SceneManager.LoadSceneAsync("Main Menu");
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
