using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public AudioSource backgroundNoiseSource;
    public AudioClip backgroundNoise;
    public AudioSource gameMusicSource;
    public AudioClip defaultMusic;
    public AudioClip theme1Music;

    private void Start()
    {
        // Start background noise if enabled
        if (PlayerPrefs.GetInt("BackgroundNoiseEnabled", 1) == 1)
        {
            backgroundNoiseSource.clip = backgroundNoise;
            backgroundNoiseSource.loop = true;
            backgroundNoiseSource.Play();
        }

        // Play game music based on theme preference and global music setting
        bool useThemeMusic = PlayerPrefs.GetInt("UseThemeMusic", 0) == 1;
        bool musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

        if (musicEnabled)
        {
            gameMusicSource.clip = useThemeMusic ? theme1Music : defaultMusic;
            gameMusicSource.loop = true;
            gameMusicSource.Play();
        }
    }

    private void OnEnable()
    {
        // Stop the global music when this scene is enabled
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }
    }

    private void OnDisable()
    {
        // Resume the global music when this scene is disabled
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ResumeMusic();
        }
    }
}