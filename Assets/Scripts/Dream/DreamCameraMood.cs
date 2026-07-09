using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// DreamCameraMood
// ---------------
// Gives each dream (and the final act) its own quiet camera personality on top
// of the normal grounded feel. Everything here is subtle, gradual, and
// reversible, and never hides a puzzle or disorients the player unfairly.
//
// Put this on the player camera in a dream scene and pick a mood:
//   WheatWind        - the field breathes; a slow wind gently rocks the view.
//   HospitalClinical - unnaturally stable and still, like a held breath.
//   GasStationAnalog - stronger analog noise and wet, blooming neon light.
//   HallwayDistort   - heavier turn smoothing and a near-invisible lens bend.
//   HouseAfterSleep  - a barely-there FOV drift, only during the final act.
//
// It reads GameSettings so the player's accessibility choices still apply
// (e.g. lowering camera motion or the analog filter also calms these moods).
public class DreamCameraMood : MonoBehaviour
{
    public enum Mood { WheatWind, HospitalClinical, GasStationAnalog, HallwayDistort, HouseAfterSleep }

    [Tooltip("Which authored feeling this scene's camera has.")]
    public Mood mood = Mood.WheatWind;

    [Header("Wheat wind")]
    [Tooltip("Max gentle rock in degrees.")]
    public float windRockDegrees = 0.5f;
    [Tooltip("Wind speed (very slow).")]
    public float windSpeed = 0.25f;

    [Header("Hospital stillness")]
    [Tooltip("How much of the normal camera motion remains (0 = frozen, 1 = normal).")]
    [Range(0f, 1f)] public float clinicalMotion = 0.15f;

    [Header("Gas station analog")]
    [Tooltip("Extra film grain added (scaled by the analog setting).")]
    public float extraGrain = 0.18f;
    [Tooltip("Extra chromatic aberration added (scaled by the analog setting).")]
    public float extraChroma = 0.06f;
    [Tooltip("Wet neon bloom boost.")]
    public float bloomBoost = 0.6f;

    [Header("Hallway distortion")]
    [Tooltip("Near-invisible lens bend.")]
    public float hallwayDistortion = -0.06f;
    [Tooltip("Heavier turn smoothing added to the look (seconds).")]
    public float extraLookSmoothing = 0.03f;

    [Header("House after sleep")]
    [Tooltip("How many degrees the FOV drifts during the final act.")]
    public float fovDriftDegrees = 3f;
    [Tooltip("Speed of the slow drift.")]
    public float fovDriftSpeed = 0.15f;
    [Tooltip("Only drift once this story flag is set (leave as is).")]
    public string finalActFlag = "HouseAfterSleepStarted";

    private CameraEffects fx;
    private SimpleFirstPersonPlayer player;
    private Camera cam;
    private float baseLookSmoothing;
    private float seed;

    void Start()
    {
        fx = GetComponent<CameraEffects>();
        player = GetComponentInParent<SimpleFirstPersonPlayer>();
        cam = GetComponent<Camera>();
        if (cam == null) cam = GetComponentInParent<Camera>();
        if (player != null) baseLookSmoothing = player.lookSmoothing;
        seed = Random.Range(0f, 100f);
    }

    void LateUpdate()
    {
        float gsMotion = GameSettings.Instance != null ? 1f : 1f; // motion already scaled inside CameraEffects
        float analog = GameSettings.Instance != null ? GameSettings.Instance.analogStrength : 1f;
        bool paused = Time.timeScale <= 0f;
        float dt = Time.unscaledDeltaTime;

        switch (mood)
        {
            case Mood.WheatWind:
                ApplyWind(paused, dt);
                break;

            case Mood.HospitalClinical:
                ApplyClinical();
                break;

            case Mood.GasStationAnalog:
                ApplyAnalogBoost(analog);
                break;

            case Mood.HallwayDistort:
                ApplyHallway(analog);
                break;

            case Mood.HouseAfterSleep:
                ApplyFovDrift(paused, dt);
                break;
        }
    }

    // A slow, soft two-axis rock, like standing in an open field at night.
    void ApplyWind(bool paused, float dt)
    {
        if (fx == null) return;

        Vector3 target = Vector3.zero;
        if (!paused)
        {
            float t = (Time.time + seed) * windSpeed;
            // Two different rates so it never looks like a clean loop.
            float rockX = (Mathf.Sin(t * Mathf.PI * 2f) + 0.4f * Mathf.Sin(t * Mathf.PI * 2f * 1.7f)) * 0.5f;
            float rockZ = (Mathf.Cos(t * Mathf.PI * 2f * 0.8f)) ;
            target = new Vector3(rockX * windRockDegrees * 0.6f, 0f, rockZ * windRockDegrees);
        }
        // Ease toward the target (or toward zero while paused) so nothing jumps.
        fx.externalEulerOffset = Vector3.Lerp(fx.externalEulerOffset, target, Mathf.Clamp01(3f * dt));
    }

    // Hold the camera unnaturally still.
    void ApplyClinical()
    {
        if (fx == null || GameSettings.Instance == null) return;
        var gs = GameSettings.Instance;
        // Scale the player's chosen motion way down (but keep their setting as the ceiling).
        fx.headBobStrength = gs.headBobStrength * clinicalMotion;
        fx.swayStrength = gs.swayStrength * clinicalMotion;
        fx.breathingStrength = gs.breathingStrength * clinicalMotion;
        fx.externalEulerOffset = Vector3.zero;
    }

    // Push the analog look harder here, but still respect the analog setting.
    void ApplyAnalogBoost(float analog)
    {
        SetSceneGrainChromaBloom(
            GameSettings.Instance != null ? GameSettings.Instance.filmGrain : 0.2f,
            GameSettings.Instance != null ? GameSettings.Instance.chromaticAberration : 0.04f,
            extraGrain * analog, extraChroma * analog, bloomBoost * analog, -1f);
    }

    // Heavier turning + an almost invisible lens bend for the endless hallway.
    void ApplyHallway(float analog)
    {
        if (player != null)
        {
            player.lookSmoothing = baseLookSmoothing + extraLookSmoothing;
        }
        SetSceneGrainChromaBloom(
            GameSettings.Instance != null ? GameSettings.Instance.filmGrain : 0.2f,
            GameSettings.Instance != null ? GameSettings.Instance.chromaticAberration : 0.04f,
            0f, 0f, 0f, hallwayDistortion * Mathf.Max(0.15f, analog));
    }

    // A tiny, slow FOV breathing that only starts during the final act.
    void ApplyFovDrift(bool paused, float dt)
    {
        if (cam == null || GameSettings.Instance == null) return;

        bool active = StoryFlagManager.Instance != null && StoryFlagManager.Instance.HasFlag(finalActFlag);
        float baseFov = GameSettings.Instance.fov;

        float targetFov = baseFov;
        if (active && !paused)
        {
            float t = (Time.time + seed) * fovDriftSpeed;
            float drift = Mathf.Sin(t * Mathf.PI * 2f) * 0.5f + 0.5f;   // 0..1
            targetFov = baseFov + drift * fovDriftDegrees;
        }
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, Mathf.Clamp01(2f * dt));
    }

    // Helper: edit this scene's global Volume (instance profile) for grain/chroma/bloom/lens.
    // Pass a negative lens value to set lens distortion; pass -1 for lens to skip it.
    void SetSceneGrainChromaBloom(float baseGrain, float baseChroma, float addGrain, float addChroma, float addBloom, float lens)
    {
        foreach (var vol in FindObjectsOfType<Volume>())
        {
            if (!vol.isGlobal || vol.profile == null) continue;
            var prof = vol.profile;

            FilmGrain grain;
            if (prof.TryGet(out grain))
            {
                grain.intensity.overrideState = true;
                grain.intensity.value = Mathf.Clamp01(baseGrain + addGrain);
            }

            ChromaticAberration chroma;
            if (prof.TryGet(out chroma))
            {
                chroma.intensity.overrideState = true;
                chroma.intensity.value = Mathf.Clamp01(baseChroma + addChroma);
            }

            if (addBloom != 0f)
            {
                Bloom bloom;
                if (prof.TryGet(out bloom))
                {
                    bloom.active = true;
                    bloom.intensity.overrideState = true;
                    bloom.intensity.value = Mathf.Max(bloom.intensity.value, 0.9f + addBloom);
                }
            }

            if (lens > -0.99f)
            {
                LensDistortion ld;
                if (!prof.TryGet(out ld)) ld = prof.Add<LensDistortion>(true);
                ld.active = true;
                ld.intensity.overrideState = true;
                ld.intensity.value = lens;
            }
        }
    }
}
