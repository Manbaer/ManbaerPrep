using UnityEngine;

// Put this on the bed next to an InteractableObject.
// Using the bed asks the DayManager whether the player may sleep,
// so bed rules can change from day to day.
[RequireComponent(typeof(InteractableObject))]
public class DreamBed : MonoBehaviour
{
    void Start()
    {
        // Reuse the shared interaction system instead of writing a new one.
        GetComponent<InteractableObject>().onInteracted.AddListener(OnBedUsed);
    }

    private void OnBedUsed()
    {
        if (DayManager.Instance != null)
        {
            DayManager.Instance.TrySleep();
        }
        else
        {
            Debug.LogWarning("DreamBed found no DayManager in the scene.");
        }
    }
}
