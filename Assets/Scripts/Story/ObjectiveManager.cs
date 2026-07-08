using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    // Keeps track of the player's current task.
    public static ObjectiveManager Instance;

    public List<string> objectives = new List<string>()
    {
        "Turn off the TV",
        "Eat dinner",
        "Lock the front door",
        "Go to bed",
        "Enter the dream"
    };

    public int currentObjectiveIndex;
    public string currentObjectiveText = "";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (string.IsNullOrEmpty(currentObjectiveText) && objectives.Count > 0)
        {
            SetObjective(objectives[0]);
        }
    }

    public static ObjectiveManager EnsureExists()
    {
        if (Instance != null)
        {
            return Instance;
        }

        GameManager.EnsureExists();
        return Instance;
    }

    public void SetObjective(string newObjective)
    {
        if (string.IsNullOrWhiteSpace(newObjective))
        {
            return;
        }

        currentObjectiveText = newObjective;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentObjective(currentObjectiveText);
        }

        Debug.Log("New objective: " + currentObjectiveText);
    }

    public void CompleteCurrentObjective()
    {
        Debug.Log("Objective complete: " + currentObjectiveText);

        currentObjectiveIndex++;

        if (currentObjectiveIndex >= 0 && currentObjectiveIndex < objectives.Count)
        {
            SetObjective(objectives[currentObjectiveIndex]);
        }
        else
        {
            SetObjective("No current objective");
        }
    }
}
