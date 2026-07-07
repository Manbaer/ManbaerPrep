using UnityEngine;

public enum InteractionKind
{
    TV,
    AnsweringMachine,
    KitchenLight,
    Bed,
    DreamDoor
}

public class InteractionTarget : MonoBehaviour
{
    // Add this to any object the player should be able to use.
    public InteractionKind kind;
    public string promptText;
    public Light lightToTurnOff;

    public void Interact()
    {
        // Send the interaction to the manager so all game state stays in one simple place.
        if (PrototypeGameManager.Instance != null)
        {
            PrototypeGameManager.Instance.UseTarget(this);
        }
    }

    public string GetPrompt()
    {
        if (!string.IsNullOrWhiteSpace(promptText))
        {
            return promptText;
        }

        return "Press E to interact";
    }
}
