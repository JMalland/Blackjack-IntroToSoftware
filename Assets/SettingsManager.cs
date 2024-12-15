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

    private void OnSFXButtonClick(bool isOn)
    {
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        SetButtonState(sfxOnButton, sfxOffButton, isOn);
        // Update SFX in the game as needed
    }

    private void OnBackgroundNoiseButtonClick(bool isOn)
    {
        PlayerPrefs.SetInt("BackgroundNoiseEnabled", isOn ? 1 : 0);
        SetButtonState(backgroundNoiseOnButton, backgroundNoiseOffButton, isOn);
        // Call GameAudioManager's ToggleBackgroundNoise if in General Game scene
    }

    private void OnMusicButtonClick(bool isOn)
    {
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        SetButtonState(musicOnButton, musicOffButton, isOn);
        AudioManager.instance.ToggleMusic(isOn);
    }

    private void OnThemeButtonClick(int themeIndex)
    {
        PlayerPrefs.SetInt("UseThemeMusic", themeIndex);
        SetThemeButtons(themeIndex == 0);
        // Change music if in General Game scene
    }

    private void OnVolumeChange(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        AudioManager.instance.SetVolume(volume);
        // Call GameAudioManager's SetVolume if in General Game scene
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