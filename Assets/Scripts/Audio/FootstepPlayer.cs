using System.Collections.Generic;
using UnityEngine;

// FootstepPlayer
// --------------
// Grounds the player in sound. As the player actually travels (using the real
// CharacterController velocity, not just key presses), this plays a footstep
// every stride. The surface under the player is found with a short downward ray
// that reads a FootstepSurface component, so carpet, wood, vinyl, tile,
// concrete, grass, and wet pavement each sound different.
//
// All the sounds are generated in CODE at runtime (like SimpleAmbientHum), so
// the game needs no audio files. The AudioSource is also created at runtime,
// because scene-saved AudioSources have refused to play in this project.
//
// Footsteps never play while standing still or pushing into a wall, because
// PlanarSpeed comes from the controller's real velocity, which is ~0 in those cases.
[RequireComponent(typeof(CharacterController))]
public class FootstepPlayer : MonoBehaviour
{
    [Header("Reference")]
    [Tooltip("Reads real speed / grounded state. Auto-found on this object if empty.")]
    public SimpleFirstPersonPlayer player;

    [Header("Stride")]
    [Tooltip("Metres walked between footsteps.")]
    public float walkStride = 0.75f;
    [Tooltip("Metres between footsteps while sprinting (shorter, quicker).")]
    public float sprintStride = 0.9f;
    [Tooltip("Only step when moving at least this fast (m/s). Stops wall-pushing steps.")]
    public float moveThreshold = 0.35f;

    [Header("Volume")]
    [Tooltip("Overall footstep loudness.")]
    [Range(0f, 1f)] public float volume = 0.55f;
    [Tooltip("Random volume wobble so steps are not identical.")]
    [Range(0f, 0.5f)] public float volumeJitter = 0.12f;
    [Tooltip("Random pitch wobble so steps are not identical.")]
    [Range(0f, 0.3f)] public float pitchJitter = 0.08f;

    [Header("Surface Detection")]
    [Tooltip("How far down to look for the floor.")]
    public float groundRayLength = 1.4f;
    [Tooltip("Surface used when the ray finds no tagged floor.")]
    public FootstepSurfaceType defaultSurface = FootstepSurfaceType.Wood;

    private CharacterController controller;
    private AudioSource source;
    private AudioReverbFilter reverb;
    private Vector3 lastPosition;
    private float distanceSinceStep;
    private bool wasGrounded = true;
    private int variantIndex;

    // Generated clips: several variants per surface for variety.
    private readonly Dictionary<FootstepSurfaceType, AudioClip[]> clips = new Dictionary<FootstepSurfaceType, AudioClip[]>();
    private const int VariantsPerSurface = 3;
    private const int SampleRate = 44100;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (player == null) player = GetComponent<SimpleFirstPersonPlayer>();

        // Runtime AudioSource (reliable) + a subtle interior reverb.
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 0f;      // first-person: heard "in the head"
        source.loop = false;

        reverb = gameObject.AddComponent<AudioReverbFilter>();
        reverb.reverbPreset = AudioReverbPreset.Room;

        BuildAllClips();

        lastPosition = transform.position;
    }

    void Update()
    {
        if (player == null || Time.timeScale <= 0f)
        {
            lastPosition = transform.position;
            return;
        }

        // Landing / weight-settle when the player touches down after a small drop.
        bool grounded = controller.isGrounded;
        if (grounded && !wasGrounded)
        {
            PlayFootstep(DetectSurface(), 0.9f, 0.85f); // softer + lower = a settle
            distanceSinceStep = 0f;
        }
        wasGrounded = grounded;

        // Only count distance actually travelled on the ground while moving.
        Vector3 delta = transform.position - lastPosition;
        delta.y = 0f;
        lastPosition = transform.position;

        if (!grounded || player.PlanarSpeed < moveThreshold)
        {
            return;
        }

        distanceSinceStep += delta.magnitude;
        float stride = player.IsSprinting ? sprintStride : walkStride;

        if (distanceSinceStep >= stride)
        {
            distanceSinceStep -= stride;
            // Sprinting is a touch louder (anxious fast walking + faint clothing movement).
            float loud = player.IsSprinting ? 1.15f : 1f;
            PlayFootstep(DetectSurface(), loud, 1f);
        }
    }

    // Look straight down for a tagged surface.
    FootstepSurfaceType DetectSurface()
    {
        Vector3 origin = transform.position + Vector3.up * 0.2f;
        RaycastHit[] hits = Physics.RaycastAll(origin, Vector3.down, groundRayLength, ~0, QueryTriggerInteraction.Collide);
        FootstepSurface best = null;
        float bestDist = float.MaxValue;
        foreach (var h in hits)
        {
            if (h.collider.transform.IsChildOf(transform)) continue; // ignore ourselves
            var fs = h.collider.GetComponentInParent<FootstepSurface>();
            if (fs != null && h.distance < bestDist)
            {
                bestDist = h.distance;
                best = fs;
            }
        }
        return best != null ? best.surface : defaultSurface;
    }

    void PlayFootstep(FootstepSurfaceType surface, float loudMul, float pitchMul)
    {
        if (!clips.ContainsKey(surface)) return;

        // Interior surfaces get a light room reverb; outdoors is dry.
        bool indoor = surface != FootstepSurfaceType.Grass && surface != FootstepSurfaceType.WetPavement;
        reverb.reverbPreset = surface == FootstepSurfaceType.Tile
            ? AudioReverbPreset.Bathroom            // tiled rooms ring a little more
            : (indoor ? AudioReverbPreset.Room : AudioReverbPreset.Off);

        AudioClip[] set = clips[surface];
        variantIndex = (variantIndex + 1) % set.Length;
        AudioClip clip = set[Random.Range(0, set.Length)];

        float baseVol = SurfaceVolume(surface) * volume * loudMul;
        float v = Mathf.Clamp01(baseVol * (1f + Random.Range(-volumeJitter, volumeJitter)));
        float p = pitchMul * (1f + Random.Range(-pitchJitter, pitchJitter));

        source.pitch = Mathf.Max(0.35f, p);
        source.PlayOneShot(clip, v);
    }

    // Relative loudness per surface. Carpet is hushed; tile and concrete are sharp.
    float SurfaceVolume(FootstepSurfaceType s)
    {
        switch (s)
        {
            case FootstepSurfaceType.Carpet: return 0.45f;
            case FootstepSurfaceType.Grass: return 0.55f;
            case FootstepSurfaceType.Vinyl: return 0.8f;
            case FootstepSurfaceType.Wood: return 0.85f;
            case FootstepSurfaceType.WetPavement: return 0.95f;
            case FootstepSurfaceType.Concrete: return 1f;
            case FootstepSurfaceType.Tile: return 1f;
            default: return 0.8f;
        }
    }

    // ---- Procedural footstep synthesis ----

    void BuildAllClips()
    {
        foreach (FootstepSurfaceType s in System.Enum.GetValues(typeof(FootstepSurfaceType)))
        {
            var set = new AudioClip[VariantsPerSurface];
            for (int i = 0; i < VariantsPerSurface; i++)
            {
                set[i] = BuildFootstepClip(s, 1000 + (int)s * 17 + i * 101);
            }
            clips[s] = set;
        }
    }

    // Builds one short footstep as shaped noise (plus optional tonal body / sharp tick).
    AudioClip BuildFootstepClip(FootstepSurfaceType s, int seed)
    {
        // Per-surface character.
        float duration;     // seconds
        float lowpass;      // 0 = bright, 1 = very dull
        float tonalFreq;    // Hz, 0 = none (adds a soft "body" thump)
        float tonalAmt;     // how loud the tonal body is
        float tickAmt;      // sharp high-frequency attack (hard floors)
        float noiseTail;    // how long the noise rings out (0..1)

        switch (s)
        {
            case FootstepSurfaceType.Carpet:
                duration = 0.10f; lowpass = 0.92f; tonalFreq = 90f; tonalAmt = 0.25f; tickAmt = 0f; noiseTail = 0.15f; break;
            case FootstepSurfaceType.Wood:
                duration = 0.13f; lowpass = 0.55f; tonalFreq = 170f; tonalAmt = 0.45f; tickAmt = 0.15f; noiseTail = 0.25f; break;
            case FootstepSurfaceType.Vinyl:
                duration = 0.11f; lowpass = 0.7f; tonalFreq = 130f; tonalAmt = 0.3f; tickAmt = 0.1f; noiseTail = 0.18f; break;
            case FootstepSurfaceType.Tile:
                duration = 0.15f; lowpass = 0.2f; tonalFreq = 0f; tonalAmt = 0f; tickAmt = 0.5f; noiseTail = 0.4f; break;
            case FootstepSurfaceType.Concrete:
                duration = 0.12f; lowpass = 0.35f; tonalFreq = 0f; tonalAmt = 0f; tickAmt = 0.35f; noiseTail = 0.22f; break;
            case FootstepSurfaceType.Grass:
                duration = 0.16f; lowpass = 0.8f; tonalFreq = 0f; tonalAmt = 0f; tickAmt = 0f; noiseTail = 0.6f; break;
            case FootstepSurfaceType.WetPavement:
                duration = 0.16f; lowpass = 0.3f; tonalFreq = 0f; tonalAmt = 0f; tickAmt = 0.4f; noiseTail = 0.55f; break;
            default:
                duration = 0.12f; lowpass = 0.5f; tonalFreq = 150f; tonalAmt = 0.3f; tickAmt = 0.2f; noiseTail = 0.25f; break;
        }

        int samples = Mathf.Max(64, (int)(SampleRate * duration));
        float[] data = new float[samples];
        System.Random rng = new System.Random(seed);
        float filtered = 0f;

        for (int i = 0; i < samples; i++)
        {
            float t = (float)i / samples;               // 0..1 through the clip
            float tSec = (float)i / SampleRate;

            // A fast attack then exponential decay. noiseTail stretches the decay.
            float decay = Mathf.Exp(-t * (9f - noiseTail * 6f));

            // Shaped white noise (the "scuff" of the foot).
            float white = (float)(rng.NextDouble() * 2.0 - 1.0);
            filtered = Mathf.Lerp(white, filtered, lowpass);   // more lowpass = duller
            float body = filtered * decay;

            // Optional soft tonal thump (wood/carpet have a little pitch).
            if (tonalFreq > 0f)
            {
                body += Mathf.Sin(2f * Mathf.PI * tonalFreq * tSec) * tonalAmt * Mathf.Exp(-t * 14f);
            }

            // Optional sharp tick at the very start (hard floors: tile/concrete/wet).
            if (tickAmt > 0f && t < 0.06f)
            {
                float tick = (float)(rng.NextDouble() * 2.0 - 1.0);
                body += tick * tickAmt * (1f - t / 0.06f);
            }

            data[i] = Mathf.Clamp(body, -1f, 1f);
        }

        AudioClip clip = AudioClip.Create("Footstep_" + s + "_" + seed, samples, 1, SampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }
}
