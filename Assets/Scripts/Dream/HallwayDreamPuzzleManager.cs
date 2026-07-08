using UnityEngine;

// Runs the "fix the wrong memories" puzzle in the DreamHallway scene.
// The hallway repeats in sections. One prop in each section is wrong.
// The wrong props can be fixed in ANY order.
// Fixing one moves the bedroom door closer.
// Interacting with any normal prop resets the whole hallway.
public class HallwayDreamPuzzleManager : MonoBehaviour
{
    [Header("Puzzle")]
    // Shown as the objective while the puzzle is unsolved. Progress is added after it.
    public string objectiveText = "Fix the wrong memories in the hallway.";
    public int solvedCount;

    [Header("Bedroom Door")]
    // The whole end wall (with the bedroom door) slides toward the player.
    public Transform bedroomDoorGroup;
    public float moveCloserPerStep = 2.4f;
    public string doorReadyObjective = "The bedroom door is close now. Open it.";

    [Header("Player Reset")]
    public SimpleFirstPersonPlayer player;

    [Header("Messages")]
    public string correctMessage = "Something clicks back into place. The door is closer.";
    public string wrongMessage = "Wrong memory. The hallway pulls you back to the start.";
    public string solvedMessage = "The hallway goes quiet. The bedroom door will open now.";

    private int totalWrongMemories;
    private Vector3 doorStartPosition;
    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;
    private HallwayMemoryObject[] memoryObjects;

    void Start()
    {
        if (player == null)
        {
            player = Object.FindFirstObjectByType<SimpleFirstPersonPlayer>();
        }

        if (bedroomDoorGroup != null)
        {
            doorStartPosition = bedroomDoorGroup.position;
        }

        if (player != null)
        {
            playerStartPosition = player.transform.position;
            playerStartRotation = player.transform.rotation;
        }

        // Count how many wrong memories are in the hallway.
        // A prop with correctStepIndex 0 or higher is a wrong memory.
        memoryObjects = Object.FindObjectsByType<HallwayMemoryObject>(FindObjectsSortMode.None);
        totalWrongMemories = 0;

        for (int i = 0; i < memoryObjects.Length; i++)
        {
            if (memoryObjects[i] != null && memoryObjects[i].correctStepIndex >= 0)
            {
                totalWrongMemories++;
            }
        }

        SetProgressObjective();
    }

    public void OnMemoryObjectChosen(HallwayMemoryObject chosenObject)
    {
        // Once the puzzle is solved, leftover props do nothing.
        if (solvedCount >= totalWrongMemories)
        {
            return;
        }

        // Any wrong memory counts, in any order the player likes.
        if (chosenObject.correctStepIndex >= 0)
        {
            HandleCorrectChoice(chosenObject);
        }
        else
        {
            ResetHallway();
        }
    }

    private void HandleCorrectChoice(HallwayMemoryObject chosenObject)
    {
        chosenObject.MarkSolved();
        solvedCount++;

        if (bedroomDoorGroup != null)
        {
            // Slide the end of the hallway a little closer to the player.
            bedroomDoorGroup.position += new Vector3(0f, 0f, -moveCloserPerStep);
        }

        if (solvedCount >= totalWrongMemories)
        {
            FinishPuzzle();
        }
        else
        {
            ShowMessage(correctMessage);
            SetProgressObjective();
        }
    }

    private void FinishPuzzle()
    {
        // The bedroom door checks this flag before it lets the player leave.
        StoryFlagManager.EnsureExists().SetFlag(StoryFlags.HallwayPuzzleSolved);
        ObjectiveManager.EnsureExists().SetObjective(doorReadyObjective);
        ShowMessage(solvedMessage);
    }

    private void ResetHallway()
    {
        solvedCount = 0;

        if (bedroomDoorGroup != null)
        {
            bedroomDoorGroup.position = doorStartPosition;
        }

        if (memoryObjects != null)
        {
            for (int i = 0; i < memoryObjects.Length; i++)
            {
                if (memoryObjects[i] != null)
                {
                    memoryObjects[i].ResetMemory();
                }
            }
        }

        MovePlayerToStart();
        ShowMessage(wrongMessage);
        SetProgressObjective();
    }

    private void MovePlayerToStart()
    {
        if (player == null)
        {
            return;
        }

        // The CharacterController fights direct teleports, so turn it off first.
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        player.transform.SetPositionAndRotation(playerStartPosition, playerStartRotation);

        if (controller != null)
        {
            controller.enabled = true;
        }
    }

    private void SetProgressObjective()
    {
        string progress = " (" + solvedCount + " of " + totalWrongMemories + " fixed)";
        ObjectiveManager.EnsureExists().SetObjective(objectiveText + progress);
    }

    private void ShowMessage(string text)
    {
        // Reuse the prototype manager's on-screen message box when it exists.
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
