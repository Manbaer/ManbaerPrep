using UnityEngine;

// One day of the house routine, set up in the Inspector.
[System.Serializable]
public class DayPlan
{
    public int dayNumber = 1;

    // Objective shown when the day starts.
    public string dayStartObjective = "Finish your routine, then sleep.";

    // Task labels shown in the on-screen list. Same order as requiredFlagsForSleep.
    public string[] taskLabels;

    // Story flags that must all be true before the bed lets the player sleep.
    public string[] requiredFlagsForSleep;

    public string bedNotReadyMessage = "You are not ready to sleep yet.";

    // Dream scene loaded when the player sleeps. Leave empty if the
    // next dream is not built yet; cannotSleepMessage is shown instead.
    public string dreamSceneToLoad = "";
    public string cannotSleepMessage = "You lie down, but sleep will not come.";
}

// Tracks which day it is and runs the sleep routine.
// The current day is computed from completed dream flags, so it
// survives scene reloads without saving anything extra:
// no dreams done = day 1, hallway dream done = day 2, and so on.
public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [Header("Dream Completion Flags In Story Order")]
    public string[] dreamCompletionFlags = { StoryFlags.DreamHallwayComplete };

    [Header("Day Plans")]
    public DayPlan[] days;

    public int currentDay = 1;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        RefreshCurrentDay();
        ApplyDayStart();
    }

    public void RefreshCurrentDay()
    {
        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();
        int dreamsDone = 0;

        for (int i = 0; i < dreamCompletionFlags.Length; i++)
        {
            if (flagManager.HasFlag(dreamCompletionFlags[i]))
            {
                dreamsDone++;
            }
        }

        currentDay = 1 + dreamsDone;
    }

    private void ApplyDayStart()
    {
        DayPlan plan = GetTodaysPlan();

        if (plan != null && !string.IsNullOrWhiteSpace(plan.dayStartObjective))
        {
            ObjectiveManager.EnsureExists().SetObjective(plan.dayStartObjective);
        }
    }

    public DayPlan GetTodaysPlan()
    {
        if (days == null)
        {
            return null;
        }

        for (int i = 0; i < days.Length; i++)
        {
            if (days[i] != null && days[i].dayNumber == currentDay)
            {
                return days[i];
            }
        }

        return null;
    }

    // Called by the DreamBed when the player uses the bed.
    public void TrySleep()
    {
        DayPlan plan = GetTodaysPlan();

        if (plan == null)
        {
            ShowMessage("You cannot sleep right now.");
            return;
        }

        if (!AllFlagsSet(plan.requiredFlagsForSleep))
        {
            ShowMessage(plan.bedNotReadyMessage);
            return;
        }

        if (string.IsNullOrWhiteSpace(plan.dreamSceneToLoad))
        {
            // The next dream is not built yet.
            ShowMessage(plan.cannotSleepMessage);
            return;
        }

        StoryFlagManager.EnsureExists().SetFlag(StoryFlags.WentToBed);
        ObjectiveManager.EnsureExists().SetObjective("Enter the dream");
        SceneLoader.EnsureExists().LoadScene(plan.dreamSceneToLoad);
    }

    private bool AllFlagsSet(string[] flags)
    {
        if (flags == null || flags.Length == 0)
        {
            return true;
        }

        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();

        for (int i = 0; i < flags.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(flags[i]) && !flagManager.HasFlag(flags[i]))
            {
                return false;
            }
        }

        return true;
    }

    private void ShowMessage(string text)
    {
        if (PrototypeGameManager.Instance != null)
        {
            PrototypeGameManager.Instance.ShowMessage(text);
        }
        else
        {
            Debug.Log(text);
        }
    }

    void OnGUI()
    {
        // Draw the day number and today's task list in the top-left corner.
        DayPlan plan = GetTodaysPlan();

        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 18;
        labelStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 20, 420, 30), "Day " + currentDay, labelStyle);

        if (plan == null || plan.taskLabels == null)
        {
            return;
        }

        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();

        for (int i = 0; i < plan.taskLabels.Length; i++)
        {
            string state = "Not done";

            if (plan.requiredFlagsForSleep != null && i < plan.requiredFlagsForSleep.Length
                && flagManager.HasFlag(plan.requiredFlagsForSleep[i]))
            {
                state = "Done";
            }

            GUI.Label(new Rect(20, 50 + i * 25, 420, 25), plan.taskLabels[i] + ": " + state, labelStyle);
        }
    }
}
