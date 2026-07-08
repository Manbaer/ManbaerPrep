using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Simple scene loading helper for UI buttons and interactable objects.
    public static SceneLoader Instance;
    public string mainMenuSceneName = "MainMenu";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public static SceneLoader EnsureExists()
    {
        if (Instance != null)
        {
            return Instance;
        }

        GameManager.EnsureExists();
        return Instance;
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogWarning("SceneLoader was asked to load an empty scene name.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game requested.");
        Application.Quit();
    }
}
