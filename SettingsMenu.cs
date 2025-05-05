using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private UnityEngine.UI.Slider volumeSlider;

    private void Start()
    {
        // Inicializar idioma y volumen
        int savedLanguage = PlayerPrefs.GetInt("LanguageKey", 0);
        languageDropdown.value = savedLanguage;

        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        volumeSlider.value = savedVolume;

        // Configurar eventos
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeLanguage(int languageID)
    {
        PlayerPrefs.SetInt("LanguageKey", languageID);
        PlayerPrefs.Save();
        EventManager.TriggerLanguageChanged(languageID);
    }

    private void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
        EventManager.TriggerVolumeChanged(volume);
    }
}