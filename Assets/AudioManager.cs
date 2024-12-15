using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public AudioClip casinoMusic;  // Audio for Main Menu, Modifiers, etc.
    public AudioClip mainMusic;    // Audio for General Game
    private AudioSource audioSource;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = casinoMusic; // Start with Casino Music
        audioSource.loop = true;
        audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene change event
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "General Game")
        {
            if (audioSource.clip != mainMusic)
            {
                audioSource.clip = mainMusic;
            }
            audioSource.Play();
        }
        else
        {
            if (audioSource.clip != casinoMusic)
            {
                audioSource.clip = casinoMusic;
            }
            audioSource.Play();
        }
    }
}