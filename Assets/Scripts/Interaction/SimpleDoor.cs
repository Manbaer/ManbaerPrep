using UnityEngine;

// A basic swinging door. Put this on a door hinge object together
// with an InteractableObject. The door visual should be a child cube
// offset from the hinge so it swings around the edge.
// Locking is handled by the InteractableObject's required story flags.
[RequireComponent(typeof(InteractableObject))]
public class SimpleDoor : MonoBehaviour
{
    public float openAngle = 100f;
    public float swingSpeed = 5f;
    public string openPrompt = "Press E - Open the door";
    public string closePrompt = "Press E - Close the door";

    private InteractableObject interactable;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen;

    void Start()
    {
        interactable = GetComponent<InteractableObject>();

        // Whatever rotation the door starts with counts as "closed".
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        // Reuse the shared interaction system instead of writing a new one.
        interactable.onInteracted.AddListener(ToggleDoor);
    }

    void Update()
    {
        Quaternion target = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, swingSpeed * Time.deltaTime);
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;
        interactable.interactionPrompt = isOpen ? closePrompt : openPrompt;
    }
}
