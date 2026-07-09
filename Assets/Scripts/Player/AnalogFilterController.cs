using UnityEngine;
using UnityEngine.Rendering;

// AnalogFilterController
// ----------------------
// A tiny helper so the lo-fi "analog" look can be turned down or off from a
// settings menu without touching the Volume profile by hand.
//
// Put this on the global post-processing Volume (the "House Mood Volume").
// It simply drives the Volume's weight from a single 0-1 strength value:
//   1 = full analog treatment, 0 = clean modern image.
//
// This keeps accessibility simple: disabling the filter never changes the
// player's speed, footsteps, interaction, or story progress - it only changes
// how the picture looks.
[RequireComponent(typeof(Volume))]
public class AnalogFilterController : MonoBehaviour
{
    [Tooltip("Turn the whole analog filter on or off.")]
    public bool analogEnabled = true;

    [Tooltip("How strong the analog look is. 0 = clean, 1 = full lo-fi.")]
    [Range(0f, 1f)] public float analogStrength = 1f;

    private Volume volume;

    void Awake()
    {
        volume = GetComponent<Volume>();
    }

    void OnEnable()
    {
        Apply();
    }

    void Update()
    {
        // Cheap to keep in sync so a menu slider updates live.
        Apply();
    }

    // Call this from a settings menu when the player changes the slider.
    public void SetStrength(float value)
    {
        analogStrength = Mathf.Clamp01(value);
        Apply();
    }

    // Call this from a settings menu toggle.
    public void SetEnabled(bool on)
    {
        analogEnabled = on;
        Apply();
    }

    void Apply()
    {
        if (volume == null)
        {
            volume = GetComponent<Volume>();
            if (volume == null) return;
        }

        volume.weight = analogEnabled ? Mathf.Clamp01(analogStrength) : 0f;
    }
}
