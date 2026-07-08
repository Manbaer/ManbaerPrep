using System.Collections.Generic;
using UnityEngine;

public class StoryFlagManager : MonoBehaviour
{
    // Stores true/false story flags such as "WatchedTV" or "DreamOneComplete".
    public static StoryFlagManager Instance;

    private Dictionary<string, bool> flags = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public static StoryFlagManager EnsureExists()
    {
        if (Instance != null)
        {
            return Instance;
        }

        GameManager.EnsureExists();
        return Instance;
    }

    public void SetFlag(string flagName, bool value = true)
    {
        if (string.IsNullOrWhiteSpace(flagName))
        {
            return;
        }

        flags[flagName] = value;
        Debug.Log("Story flag set: " + flagName + " = " + value);
    }

    public bool HasFlag(string flagName)
    {
        if (string.IsNullOrWhiteSpace(flagName))
        {
            return false;
        }

        return flags.ContainsKey(flagName) && flags[flagName];
    }

    public void ClearFlag(string flagName)
    {
        if (flags.ContainsKey(flagName))
        {
            flags.Remove(flagName);
        }
    }
}
