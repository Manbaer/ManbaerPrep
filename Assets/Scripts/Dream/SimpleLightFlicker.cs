using UnityEngine;

// Makes a light flicker like a dying bulb.
// Used by the wrong lamp in the dream hallway.
public class SimpleLightFlicker : MonoBehaviour
{
    public Light targetLight;
    public float minIntensity = 0.1f;
    public float maxIntensity = 1.6f;
    public float flickerSpeed = 14f;

    private float noiseOffset;

    void Start()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        // Random offset so two flickering lights never blink in sync.
        noiseOffset = Random.value * 100f;
    }

    void Update()
    {
        if (targetLight == null)
        {
            return;
        }

        float noise = Mathf.PerlinNoise(noiseOffset, Time.time * flickerSpeed);
        targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
