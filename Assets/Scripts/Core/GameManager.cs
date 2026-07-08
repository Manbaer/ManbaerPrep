using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Central place for broad game progress.
    // Other systems still do their own jobs, so this does not become one giant script.
    public static GameManager Instance;

    public int currentChapter = 1;
    public string currentObjectiveText = "";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        EnsureHelperManagers();
    }

    public static GameManager EnsureExists()
    {
        if (Instance != null)
        {
            return Instance;
        }

        GameObject managerObject = new GameObject("Game Manager");
        return managerObject.AddComponent<GameManager>();
    }

    public void SetChapter(int newChapter)
    {
        currentChapter = newChapter;
    }

    public void SetCurrentObjective(string objectiveText)
    {
        currentObjectiveText = objectiveText;
    }

    private void EnsureHelperManagers()
    {
        if (StoryFlagManager.Instance == null)
        {
            gameObject.AddComponent<StoryFlagManager>();
        }

        if (ObjectiveManager.Instance == null)
        {
            gameObject.AddComponent<ObjectiveManager>();
        }

        if (SceneLoader.Instance == null)
        {
            gameObject.AddComponent<SceneLoader>();
        }
    }
}
