using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource;
    public AudioClip startingMusic;
    public AudioClip defaultMusic;
    public AudioClip theme1Music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = startingMusic;
        musicSource.loop = true;
        musicSource.Play();
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

    public void ChangeToGameMusic(bool useThemeMusic)
    {
        musicSource.Stop();
        musicSource.clip = useThemeMusic ? theme1Music : defaultMusic;
        musicSource.Play();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void StopMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
}
