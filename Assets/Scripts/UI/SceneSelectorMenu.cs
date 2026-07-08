using UnityEngine;

public class SceneSelectorMenu : MonoBehaviour
{
    // Developer helper for quickly loading scenes while testing.
    public string[] sceneNames =
    {
        "MainMenu",
        "HousePrototype",
        "DreamHallway",
        "TestingSandbox"
    };

    public void LoadSceneByIndex(int index)
    {
        if (index < 0 || index >= sceneNames.Length)
        {
            Debug.LogWarning("SceneSelectorMenu index is out of range.");
            return;
        }

        LoadSceneByName(sceneNames[index]);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneLoader.EnsureExists().LoadScene(sceneName);
    }
}
