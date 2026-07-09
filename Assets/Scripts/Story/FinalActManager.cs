using UnityEngine;

// Watches the five final dream objects during the House After Sleep.
// When all five are placed, the front door quietly unlocks.
// Each final object calls CheckFinalObjects through its onInteracted event.
public class FinalActManager : MonoBehaviour
{
    [TextArea] public string unlockMessage = "Somewhere inside the front door, something stops being locked.";
    public string unlockObjective = "The front door will open now. Decide.";

    private bool unlocked;

    void Start()
    {
        // In case the scene reloads halfway through the final act.
        CheckFinalObjects();
    }

    public void CheckFinalObjects()
    {
        if (unlocked)
        {
            return;
        }

        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();

        bool allPlaced =
            flagManager.HasFlag(StoryFlags.FinalClockPlaced) &&
            flagManager.HasFlag(StoryFlags.FinalFusePlaced) &&
            flagManager.HasFlag(StoryFlags.FinalPatientFilePlaced) &&
            flagManager.HasFlag(StoryFlags.FinalReceiptPlaced) &&
            flagManager.HasFlag(StoryFlags.FinalVHSTapePlayed);

        if (!allPlaced)
        {
            return;
        }

        unlocked = true;
        flagManager.SetFlag(StoryFlags.FrontDoorUnlocked);
        ObjectiveManager.EnsureExists().SetObjective(unlockObjective);

        if (PrototypeGameManager.Instance != null)
        {
            PrototypeGameManager.Instance.ShowMessage(unlockMessage);
        }
        else
        {
            Debug.Log(unlockMessage);
        }
    }
}
