using UnityEngine;

// The hidden early ending. Lives on the day 5 "impossible road" front door.
// Conditions: the player has used the dream-battery TV remote, studied the
// impossible map, and now tries the front door a SECOND time.
// The door opens fully onto endless wheat where the street should be.
[RequireComponent(typeof(InteractableObject))]
public class SecretFrontDoor : MonoBehaviour
{
    [TextArea] public string secretMessage = "The door opens all the way this time. Wheat to the horizon, where the street should be. It has been growing there for years.";
    public float endingDelaySeconds = 7f;

    private int timesUsed;

    void Awake()
    {
        // Reuse the shared interaction system instead of writing a new one.
        GetComponent<InteractableObject>().onInteracted.AddListener(OnDoorUsed);
    }

    private void OnDoorUsed()
    {
        timesUsed++;

        // The first try is the normal day 5 glimpse. The secret needs a second,
        // deliberate try by a player who studied the optional dream items.
        if (timesUsed < 2)
        {
            return;
        }

        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();

        bool conditionsMet =
            flagManager.HasFlag(StoryFlags.UsedTVRemote) &&
            flagManager.HasFlag(StoryFlags.LookedAtImpossibleMap);

        if (conditionsMet)
        {
            EndingRunner.RunEnding(StoryFlags.SecretEndingFrontDoorEarly, secretMessage, endingDelaySeconds);
        }
    }
}
