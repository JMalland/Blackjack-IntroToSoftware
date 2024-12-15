using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource backgroundNoiseSource;
    public AudioClip mainMusic;
    public AudioClip themeMusic;
    public AudioClip backgroundNoise;

    private void Start()
    {
        // Start playing background noise if enabled
        if (PlayerPrefs.GetInt("BackgroundNoiseEnabled", 1) == 1)
        {
            backgroundNoiseSource.clip = backgroundNoise;
            backgroundNoiseSource.loop = true;
            backgroundNoiseSource.Play();
        }

        // Play the selected music (default or themed)
        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            bool useThemeMusic = PlayerPrefs.GetInt("UseThemeMusic", 0) == 1;
            musicSource.clip = useThemeMusic ? themeMusic : mainMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void ToggleBackgroundNoise(bool isOn)
    {
        if (isOn)
        {
            backgroundNoiseSource.clip = backgroundNoise;
            backgroundNoiseSource.Play();
        }
        else
        {
            backgroundNoiseSource.Stop();
        }
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}