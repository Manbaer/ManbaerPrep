using UnityEngine;

// Runs the "buy the correct items" puzzle in the gas station dream.
// The register does not want money. It wants three memories:
// one to remember (photo), one to watch (VHS tape), one to open (key).
// Checking out with all three prints a receipt with the player's home
// address, reveals a door on the road, and sets GasStationDreamComplete.
public class GasStationPuzzleManager : MonoBehaviour
{
    public int memoriesNeeded = 3;

    [Header("Revealed When Solved")]
    public GameObject printedReceipt;
    public GameObject doorOnTheRoad;

    [Header("Messages")]
    public string objectiveText = "The register wants three memories: remember, watch, open.";
    public string solvedObjective = "Take the door on the road home.";
    public string solvedMessage = "The register opens. A bell rings. The receipt prints your home address.";
    public string notEnoughMessage = "The register blinks: TOTAL - 3 MEMORIES. YOU HAVE ";

    private int heldMemories;
    private bool solved;

    void Start()
    {
        if (printedReceipt != null)
        {
            printedReceipt.SetActive(false);
        }

        if (doorOnTheRoad != null)
        {
            doorOnTheRoad.SetActive(false);
        }

        SetProgressObjective();
    }

    // Called by GasStationItem when the player picks something up.
    public void CollectItem(GasStationItem item)
    {
        if (item == null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(item.collectFlagToSet))
        {
            StoryFlagManager.EnsureExists().SetFlag(item.collectFlagToSet);
        }

        if (item.isMemoryItem)
        {
            heldMemories++;
        }

        // The item's own screenMessage already describes what was taken.
        item.gameObject.SetActive(false);
        SetProgressObjective();
    }

    // Wired to the register's onInteracted event.
    public void TryCheckout()
    {
        if (solved)
        {
            return;
        }

        if (heldMemories >= memoriesNeeded)
        {
            solved = true;
            StoryFlagManager.EnsureExists().SetFlag(StoryFlags.GasStationDreamComplete);

            if (printedReceipt != null)
            {
                printedReceipt.SetActive(true);
            }

            if (doorOnTheRoad != null)
            {
                doorOnTheRoad.SetActive(true);
            }

            ObjectiveManager.EnsureExists().SetObjective(solvedObjective);
            ShowMessage(solvedMessage);
        }
        else
        {
            // A clear response for wrong or incomplete checkouts.
            ShowMessage(notEnoughMessage + heldMemories + ".");
        }
    }

    private void SetProgressObjective()
    {
        if (solved)
        {
            return;
        }

        string progress = " (" + heldMemories + " of " + memoriesNeeded + ")";
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
