using UnityEngine;

// Put this on a gas station shelf item next to an InteractableObject.
// Picking it up hands it to the puzzle manager.
// Memory items are the three the register wants:
// one to remember (photo), one to watch (VHS), one to open (key).
[RequireComponent(typeof(InteractableObject))]
public class GasStationItem : MonoBehaviour
{
    public string itemId = "Item";
    public bool isMemoryItem;

    // Optional story flag set when picked up (used by house consequences later).
    public string collectFlagToSet = "";

    void Awake()
    {
        // Hook in Awake so the pickup always works, even if Start gets delayed.
        // Reuse the shared interaction system instead of writing a new one.
        GetComponent<InteractableObject>().onInteracted.AddListener(OnPickedUp);
    }

    private void OnPickedUp()
    {
        // Find the manager when needed, so load order never matters.
        GasStationPuzzleManager manager = Object.FindFirstObjectByType<GasStationPuzzleManager>();

        if (manager != null)
        {
            manager.CollectItem(this);
        }
    }
}
