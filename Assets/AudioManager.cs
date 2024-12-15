using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource;
    public AudioClip startingMusic;

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

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}