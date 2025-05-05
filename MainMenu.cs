using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    public void StartNewGame()
    {
        PlayerPrefs.SetInt("HasGun", 0);
        PlayerPrefs.SetInt("BossDialogue", 0);
        SceneManager.LoadScene("Level 1");
    }

    public void ContinueGame()
    {
        // Cargar datos guardados
        SaveManager.LoadGame();
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}