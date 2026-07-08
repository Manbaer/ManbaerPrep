using UnityEngine;

// Put this on a hallway prop next to an InteractableObject.
// It tells the puzzle manager when the player chooses this prop.
// correctStepIndex 0 or higher marks this prop as a wrong memory.
// Wrong memories can be fixed in any order.
// Use -1 for normal props that are never the wrong memory.
[RequireComponent(typeof(InteractableObject))]
public class HallwayMemoryObject : MonoBehaviour
{
    [Header("Puzzle")]
    public int correctStepIndex = -1;
    public string displayName = "hallway object";

    [Header("Solved Look")]
    public string solvedPrompt = "Nothing feels wrong here anymore.";
    public GameObject wrongSignToHide;
    public Transform visualToFlipWhenSolved;

    // Used by the flickering lamp: fixing it stops the flicker and turns the light off.
    public SimpleLightFlicker flickerToStopWhenSolved;

    private HallwayDreamPuzzleManager puzzleManager;
    private InteractableObject interactable;
    private string normalPrompt;
    private bool isSolved;

    void Start()
    {
        puzzleManager = Object.FindFirstObjectByType<HallwayDreamPuzzleManager>();
        interactable = GetComponent<InteractableObject>();
        normalPrompt = interactable.interactionPrompt;

        // Reuse the shared interaction system instead of writing a new one.
        interactable.onInteracted.AddListener(OnChosen);
    }

    private void OnChosen()
    {
        // Solved props stay quiet so the player cannot re-trigger them.
        if (isSolved || puzzleManager == null)
        {
            return;
        }

        puzzleManager.OnMemoryObjectChosen(this);
    }

    public void MarkSolved()
    {
        isSolved = true;
        interactable.interactionPrompt = solvedPrompt;

        if (wrongSignToHide != null)
        {
            wrongSignToHide.SetActive(false);
        }

        if (visualToFlipWhenSolved != null)
        {
            // Used by the upside-down painting so it turns right side up.
            visualToFlipWhenSolved.Rotate(0f, 0f, 180f);
        }

        if (flickerToStopWhenSolved != null)
        {
            // "Turn off the flickering lamp": stop the flicker and switch the light off.
            flickerToStopWhenSolved.enabled = false;

            if (flickerToStopWhenSolved.targetLight != null)
            {
                flickerToStopWhenSolved.targetLight.enabled = false;
            }
        }
    }

    public void ResetMemory()
    {
        if (!isSolved)
        {
            return;
        }

        isSolved = false;
        interactable.interactionPrompt = normalPrompt;

        if (wrongSignToHide != null)
        {
            wrongSignToHide.SetActive(true);
        }

        if (visualToFlipWhenSolved != null)
        {
            visualToFlipWhenSolved.Rotate(0f, 0f, 180f);
        }

        if (flickerToStopWhenSolved != null)
        {
            // The hallway reset brings the broken lamp back.
            flickerToStopWhenSolved.enabled = true;

            if (flickerToStopWhenSolved.targetLight != null)
            {
                flickerToStopWhenSolved.targetLight.enabled = true;
            }
        }
    }
}
