using UnityEngine;

// Put this on an electrical pole next to an InteractableObject.
// Each pole has a symbol. The puzzle manager checks if the player
// activated the poles in the order the house remembers.
[RequireComponent(typeof(InteractableObject))]
public class WheatPole : MonoBehaviour
{
    // Symbol on the pole box: Moon, House, Eye, Clock, Tree, or Door.
    public string symbol = "Moon";

    // Lamp objects (bulb + light) that turn on when this pole is correct.
    public GameObject lampOn;

    private WheatFieldPuzzleManager puzzleManager;
    private bool isOn;

    void Start()
    {
        puzzleManager = Object.FindFirstObjectByType<WheatFieldPuzzleManager>();

        // Reuse the shared interaction system instead of writing a new one.
        GetComponent<InteractableObject>().onInteracted.AddListener(OnChosen);
        SetOn(false);
    }

    private void OnChosen()
    {
        // Poles that are already humming ignore the player.
        if (isOn || puzzleManager == null)
        {
            return;
        }

        puzzleManager.OnPoleChosen(this);
    }

    public void SetOn(bool on)
    {
        isOn = on;

        if (lampOn != null)
        {
            lampOn.SetActive(on);
        }
    }
}
