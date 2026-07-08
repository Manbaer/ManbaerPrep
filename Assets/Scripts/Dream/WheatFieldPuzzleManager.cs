using UnityEngine;

// Runs the "restore the circuit" puzzle in the wheat field dream.
// The player must activate the poles in the order the house remembers.
// A wrong pole kills the hum and resets every pole.
// Solving it lights the distant house, reveals the exit door,
// and sets WheatFieldPowerRestored.
public class WheatFieldPuzzleManager : MonoBehaviour
{
    [Header("Correct Symbol Order")]
    // Matches the fuse box clue: wrong time, open door, covered eye, empty house, goodnight.
    public string[] correctOrder = { "Clock", "Door", "Eye", "House", "Moon" };

    public int currentStep;

    [Header("Revealed When Solved")]
    public GameObject exitDoor;
    public GameObject[] distantHouseLights;

    [Header("Messages")]
    public string correctMessage = "A transformer hums awake. The wire sings down the line.";
    public string wrongMessage = "The hum dies. Every pole goes dark again.";
    public string solvedMessage = "Far away, the house turns its lights on. A door stands between the poles.";
    public string objectiveText = "Restore the power. The house remembers the order.";
    public string solvedObjective = "Walk to the door between the poles.";

    private WheatPole[] poles;

    void Start()
    {
        poles = Object.FindObjectsByType<WheatPole>(FindObjectsSortMode.None);

        // Everything the puzzle reveals starts hidden.
        if (exitDoor != null)
        {
            exitDoor.SetActive(false);
        }

        SetLights(false);
        SetProgressObjective();
    }

    public void OnPoleChosen(WheatPole pole)
    {
        if (currentStep >= correctOrder.Length)
        {
            return;
        }

        if (pole.symbol == correctOrder[currentStep])
        {
            pole.SetOn(true);
            currentStep++;

            if (currentStep >= correctOrder.Length)
            {
                RestorePower();
            }
            else
            {
                ShowMessage(correctMessage);
                SetProgressObjective();
            }
        }
        else
        {
            ResetField();
        }
    }

    private void RestorePower()
    {
        SetLights(true);

        if (exitDoor != null)
        {
            exitDoor.SetActive(true);
        }

        StoryFlagManager.EnsureExists().SetFlag(StoryFlags.WheatFieldPowerRestored);
        ObjectiveManager.EnsureExists().SetObjective(solvedObjective);
        ShowMessage(solvedMessage);
    }

    private void ResetField()
    {
        currentStep = 0;

        if (poles != null)
        {
            for (int i = 0; i < poles.Length; i++)
            {
                if (poles[i] != null)
                {
                    poles[i].SetOn(false);
                }
            }
        }

        ShowMessage(wrongMessage);
        SetProgressObjective();
    }

    private void SetLights(bool on)
    {
        if (distantHouseLights == null)
        {
            return;
        }

        for (int i = 0; i < distantHouseLights.Length; i++)
        {
            if (distantHouseLights[i] != null)
            {
                distantHouseLights[i].SetActive(on);
            }
        }
    }

    private void SetProgressObjective()
    {
        string progress = " (" + currentStep + " of " + correctOrder.Length + " poles)";
        ObjectiveManager.EnsureExists().SetObjective(objectiveText + progress);
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
}
