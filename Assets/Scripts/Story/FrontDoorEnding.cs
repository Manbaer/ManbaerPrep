using UnityEngine;

// The final front door. Its InteractableObject stays locked until
// FrontDoorUnlocked. After that:
// - First use swings the door open onto the morning outside.
//   Walking out reaches the EndingTrigger (Morning ending).
// - Using it again closes the door: the Routine ending (stay inside).
[RequireComponent(typeof(InteractableObject))]
public class FrontDoorEnding : MonoBehaviour
{
    public float openAngle = -110f;
    public float swingSpeed = 3f;
    [TextArea] public string openMessage = "The door swings open. Morning is out there, waiting to be believed.";
    public string closePrompt = "Press E - Close the door";
    [TextArea] public string stayEndingMessage = "You close the door. The lock clicks by itself. Tomorrow will be the same, and that is the point.";
    public float endingDelaySeconds = 6f;

    private InteractableObject interactable;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen;

    void Awake()
    {
        interactable = GetComponent<InteractableObject>();
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        // Reuse the shared interaction system instead of writing a new one.
        interactable.onInteracted.AddListener(OnDoorUsed);
    }

    void Update()
    {
        Quaternion target = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, swingSpeed * Time.deltaTime);
    }

    private void OnDoorUsed()
    {
        if (!isOpen)
        {
            isOpen = true;
            interactable.interactionPrompt = closePrompt;
            ShowMessage(openMessage);
        }
        else
        {
            // Choosing to shut the morning out is the Routine ending.
            EndingRunner.RunEnding(StoryFlags.EndingStayInside, stayEndingMessage, endingDelaySeconds);
        }
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
