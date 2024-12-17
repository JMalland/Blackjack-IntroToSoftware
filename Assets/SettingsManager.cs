using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Button sfxOnButton;
    public Button sfxOffButton;
    public Button backgroundNoiseOnButton;
    public Button backgroundNoiseOffButton;
    public Button musicOnButton;
    public Button musicOffButton;
    public Button defaultThemeButton;
    public Button theme1Button;
    public Slider volumeSlider;

    public AudioSource backgroundNoiseAudioSource; // Reference to Background Noise AudioSource in General Game scene

    private void Start()
    {
        // Load saved settings
        SetButtonState(sfxOnButton, sfxOffButton, PlayerPrefs.GetInt("SFXEnabled", 1) == 1);
        SetButtonState(backgroundNoiseOnButton, backgroundNoiseOffButton, PlayerPrefs.GetInt("BackgroundNoiseEnabled", 1) == 1);
        SetButtonState(musicOnButton, musicOffButton, PlayerPrefs.GetInt("MusicEnabled", 1) == 1);
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);

        // Set theme based on saved settings
        int theme = PlayerPrefs.GetInt("UseThemeMusic", 0);
        SetThemeButtons(theme == 0);

        // Add listeners
        sfxOnButton.onClick.AddListener(() => OnSFXButtonClick(true));
        sfxOffButton.onClick.AddListener(() => OnSFXButtonClick(false));
        backgroundNoiseOnButton.onClick.AddListener(() => OnBackgroundNoiseButtonClick(true));
        backgroundNoiseOffButton.onClick.AddListener(() => OnBackgroundNoiseButtonClick(false));
        musicOnButton.onClick.AddListener(() => OnMusicButtonClick(true));
        musicOffButton.onClick.AddListener(() => OnMusicButtonClick(false));
        defaultThemeButton.onClick.AddListener(() => OnThemeButtonClick(0));
        theme1Button.onClick.AddListener(() => OnThemeButtonClick(1));
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    public void OnSFXButtonClick(bool isOn)
    {
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        SetButtonState(sfxOnButton, sfxOffButton, isOn);
        if (SFXManager.instance != null)
        {
            SFXManager.instance.ToggleSFX(isOn);
        }
    }

    public void OnBackgroundNoiseButtonClick(bool isOn)
    {
        PlayerPrefs.SetInt("BackgroundNoiseEnabled", isOn ? 1 : 0);
        SetButtonState(backgroundNoiseOnButton, backgroundNoiseOffButton, isOn);
        if (backgroundNoiseAudioSource != null)
        {
            backgroundNoiseAudioSource.mute = !isOn;
        }
    }

    public void OnMusicButtonClick(bool isOn)
    {
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        SetButtonState(musicOnButton, musicOffButton, isOn);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ToggleMusic(isOn);
        }
    }

    public void OnThemeButtonClick(int themeIndex)
    {
        PlayerPrefs.SetInt("UseThemeMusic", themeIndex);
        SetThemeButtons(themeIndex == 0);
        // Do not change the starting music here
        // Music change will be handled by GameAudioManager in the General Game scene
    }

    public void OnVolumeChange(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetVolume(volume);
        }
    }

    private void SetButtonState(Button onButton, Button offButton, bool isOn)
    {
        onButton.image.color = isOn ? Color.green : Color.red;
        offButton.image.color = isOn ? Color.red : Color.green;
    }

    private void SetThemeButtons(bool isDefaultSelected)
    {
        defaultThemeButton.interactable = !isDefaultSelected;
        theme1Button.interactable = isDefaultSelected;
    }
}