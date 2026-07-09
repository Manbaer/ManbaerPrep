using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // This script is used by the main menu buttons.
    public string firstGameplayScene = "HousePrototype";
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShowMainMenu();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstGameplayScene);
    }

    public void ShowSettings()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // Open the full settings overlay; Back returns to the main menu.
        if (SettingsMenu.Instance != null)
        {
            SettingsMenu.Instance.Open(ShowMainMenu);
        }
    }

    public void ShowMainMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game button pressed.");
        Application.Quit();
    }
}
