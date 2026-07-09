using UnityEngine;

// Generates a simple looping ambience in code, so no audio files are needed.
// A low sine hum (room tone, fluorescent buzz, refrigerators) plus
// soft filtered noise (wind, rain, static). Tune the numbers per scene.
// It creates its own AudioSource at runtime; do not add one in the scene.
public class SimpleAmbientHum : MonoBehaviour
{
    [Header("Hum")]
    // Use whole numbers so the loop stays seamless.
    public float humFrequency = 60f;
    public float humVolume = 0.1f;

    [Header("Noise (wind / rain / static)")]
    public float noiseVolume = 0.05f;

    [Header("Output")]
    public float volume = 0.35f;

    void Start()
    {
        int sampleRate = 44100;
        int seconds = 4;
        float[] data = new float[sampleRate * seconds];

        // Fixed seed so the ambience sounds the same every time.
        System.Random rng = new System.Random(12345);
        float filteredNoise = 0f;

        for (int i = 0; i < data.Length; i++)
        {
            float t = (float)i / sampleRate;

            // Base hum plus a quiet octave above it.
            float hum = Mathf.Sin(2f * Mathf.PI * humFrequency * t) * humVolume
                      + Mathf.Sin(2f * Mathf.PI * humFrequency * 2f * t) * humVolume * 0.3f;

            // Low-passed white noise sounds like wind or distant rain.
            float white = (float)(rng.NextDouble() * 2.0 - 1.0);
            filteredNoise = Mathf.Lerp(filteredNoise, white, 0.05f);

            data[i] = hum + filteredNoise * noiseVolume;
        }

        AudioClip clip = AudioClip.Create("Ambient Hum", data.Length, 1, sampleRate, false);
        clip.SetData(data, 0);

        // A runtime-created source plays reliably; scene-serialized ones can refuse.
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = true;
        source.volume = volume;
        source.spatialBlend = 0f;
        source.playOnAwake = false;
        source.Play();
    }
}
