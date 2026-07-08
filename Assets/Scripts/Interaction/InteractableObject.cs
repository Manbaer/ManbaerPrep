using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    // Reusable interaction script for TVs, doors, notes, dream objects, and exits.
    public string interactionPrompt = "Press E to interact";

    [Header("Requirements")]
    public string[] requiredStoryFlags;
    public string missingRequirementMessage = "You are not ready to do that yet.";
    public bool callLegacyWhenRequirementsMissing;

    [Header("Story Flag")]
    public bool setStoryFlag;
    public string storyFlagToSet;

    [Header("Objective")]
    public bool completeCurrentObjective;
    public bool setNewObjective;
    public string newObjectiveText;

    [Header("Scene Loading")]
    public bool loadSceneAfterInteraction;
    public string sceneToLoad;

    [Header("Feedback")]
    // Shown in the on-screen message box after a successful interaction.
    public string screenMessage;

    [Header("Optional Extras")]
    public string debugMessage;
    public bool onlyUseOnce;
    public bool callLegacyInteractionTarget;
    public UnityEvent onInteracted = new UnityEvent();

    private bool hasBeenUsed;

    public string GetPrompt()
    {
        return interactionPrompt;
    }

    public void Interact()
    {
        if (onlyUseOnce && hasBeenUsed)
        {
            return;
        }

        if (!HasRequiredFlags())
        {
            if (!string.IsNullOrWhiteSpace(missingRequirementMessage))
            {
                Debug.Log(missingRequirementMessage);

                // Also show the warning on screen when the prototype message box exists.
                if (PrototypeGameManager.Instance != null)
                {
                    PrototypeGameManager.Instance.ShowMessage(missingRequirementMessage);
                }
            }

            if (callLegacyWhenRequirementsMissing)
            {
                CallLegacyInteractionTarget();
            }

            return;
        }

        hasBeenUsed = true;

        if (!string.IsNullOrWhiteSpace(debugMessage))
        {
            Debug.Log(debugMessage);
        }

        if (!string.IsNullOrWhiteSpace(screenMessage) && PrototypeGameManager.Instance != null)
        {
            PrototypeGameManager.Instance.ShowMessage(screenMessage);
        }

        if (setStoryFlag)
        {
            StoryFlagManager.EnsureExists().SetFlag(storyFlagToSet);
        }

        if (completeCurrentObjective)
        {
            ObjectiveManager.EnsureExists().CompleteCurrentObjective();
        }

        if (setNewObjective)
        {
            ObjectiveManager.EnsureExists().SetObjective(newObjectiveText);
        }

        if (onInteracted != null)
        {
            onInteracted.Invoke();
        }

        if (callLegacyInteractionTarget)
        {
            CallLegacyInteractionTarget();
        }

        if (loadSceneAfterInteraction && !string.IsNullOrWhiteSpace(sceneToLoad))
        {
            SceneLoader.EnsureExists().LoadScene(sceneToLoad);
        }
    }

    private bool HasRequiredFlags()
    {
        if (requiredStoryFlags == null || requiredStoryFlags.Length == 0)
        {
            return true;
        }

        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();

        for (int i = 0; i < requiredStoryFlags.Length; i++)
        {
            string flagName = requiredStoryFlags[i];

            if (!string.IsNullOrWhiteSpace(flagName) && !flagManager.HasFlag(flagName))
            {
                return false;
            }
        }

        return true;
    }

    private void CallLegacyInteractionTarget()
    {
        InteractionTarget oldTarget = GetComponent<InteractionTarget>();

        if (oldTarget != null)
        {
            oldTarget.Interact();
        }
    }
}
