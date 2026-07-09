using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// GameSettings
// ------------
// One place that stores every player-facing option, saves it with PlayerPrefs,
// and applies it to whatever scene is loaded. It survives scene changes
// (DontDestroyOnLoad) and re-applies itself each time a scene loads, so the
// player, camera effects, analog filter, and post-processing always match the
// player's chosen settings.
//
// Nothing here changes movement speed, footsteps, interaction, or story - only
// how the game looks and feels to control.
public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    // ---- Look ----
    public float fov = 68f;              // 60..80
    public float sensitivityX = 2f;      // horizontal mouse
    public float sensitivityY = 2f;      // vertical mouse
    public bool invertY = false;
    public bool rawMouse = false;        // true = no smoothing (accessibility)

    // ---- Camera motion (accessibility) ----
    public float headBobStrength = 1f;   // 0..1
    public float swayStrength = 1f;      // 0..1
    public float breathingStrength = 1f; // 0..1
    public bool screenShake = true;      // allow authored camera impulses

    // ---- Analog look ----
    public float analogStrength = 1f;    // 0..1 (0 disables the lo-fi filter)
    public float filmGrain = 0.2f;       // 0..1
    public float chromaticAberration = 0.04f; // 0..1
    public bool motionBlur = false;      // off by default

    // ---- Gameplay feel ----
    public bool showReticle = true;
    public bool sprintHold = true;       // true = hold to sprint, false = toggle

    // ---- Visual preset (Chunk 3) ----
    // 0 Low, 1 Medium, 2 High, 3 Analog. -1 = custom (player changed something).
    public int visualPreset = 3;

    const string P = "settings_";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        Apply();
    }

    void OnDestroy()
    {
        if (Instance == this) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        // Wait one frame is not needed; objects exist on load. Apply now.
        Apply();
    }

    // Make sure a GameSettings exists as early as possible in any scene.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnsureExists()
    {
        if (Instance == null)
        {
            var go = new GameObject("GameSettings");
            go.AddComponent<GameSettings>();
        }
    }

    // ---------- Persistence ----------

    public void Load()
    {
        fov = PlayerPrefs.GetFloat(P + "fov", fov);
        sensitivityX = PlayerPrefs.GetFloat(P + "sensX", sensitivityX);
        sensitivityY = PlayerPrefs.GetFloat(P + "sensY", sensitivityY);
        invertY = PlayerPrefs.GetInt(P + "invertY", invertY ? 1 : 0) == 1;
        rawMouse = PlayerPrefs.GetInt(P + "rawMouse", rawMouse ? 1 : 0) == 1;

        headBobStrength = PlayerPrefs.GetFloat(P + "headBob", headBobStrength);
        swayStrength = PlayerPrefs.GetFloat(P + "sway", swayStrength);
        breathingStrength = PlayerPrefs.GetFloat(P + "breathing", breathingStrength);
        screenShake = PlayerPrefs.GetInt(P + "screenShake", screenShake ? 1 : 0) == 1;

        analogStrength = PlayerPrefs.GetFloat(P + "analog", analogStrength);
        filmGrain = PlayerPrefs.GetFloat(P + "grain", filmGrain);
        chromaticAberration = PlayerPrefs.GetFloat(P + "chroma", chromaticAberration);
        motionBlur = PlayerPrefs.GetInt(P + "motionBlur", motionBlur ? 1 : 0) == 1;

        showReticle = PlayerPrefs.GetInt(P + "reticle", showReticle ? 1 : 0) == 1;
        sprintHold = PlayerPrefs.GetInt(P + "sprintHold", sprintHold ? 1 : 0) == 1;

        visualPreset = PlayerPrefs.GetInt(P + "preset", visualPreset);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(P + "fov", fov);
        PlayerPrefs.SetFloat(P + "sensX", sensitivityX);
        PlayerPrefs.SetFloat(P + "sensY", sensitivityY);
        PlayerPrefs.SetInt(P + "invertY", invertY ? 1 : 0);
        PlayerPrefs.SetInt(P + "rawMouse", rawMouse ? 1 : 0);

        PlayerPrefs.SetFloat(P + "headBob", headBobStrength);
        PlayerPrefs.SetFloat(P + "sway", swayStrength);
        PlayerPrefs.SetFloat(P + "breathing", breathingStrength);
        PlayerPrefs.SetInt(P + "screenShake", screenShake ? 1 : 0);

        PlayerPrefs.SetFloat(P + "analog", analogStrength);
        PlayerPrefs.SetFloat(P + "grain", filmGrain);
        PlayerPrefs.SetFloat(P + "chroma", chromaticAberration);
        PlayerPrefs.SetInt(P + "motionBlur", motionBlur ? 1 : 0);

        PlayerPrefs.SetInt(P + "reticle", showReticle ? 1 : 0);
        PlayerPrefs.SetInt(P + "sprintHold", sprintHold ? 1 : 0);

        PlayerPrefs.SetInt(P + "preset", visualPreset);
        PlayerPrefs.Save();
    }

    // ---------- Apply to the current scene ----------

    public void Apply()
    {
        // Player controller + camera.
        var player = FindObjectOfType<SimpleFirstPersonPlayer>();
        if (player != null)
        {
            player.mouseSensitivity = sensitivityX;
            player.mouseSensitivityY = sensitivityY;
            player.invertY = invertY;
            player.rawMouseInput = rawMouse;
            player.showReticle = showReticle;
            player.holdToSprint = sprintHold;

            if (player.playerCamera != null)
            {
                // Store a base FOV so dream effects can offset from the player's choice.
                player.playerCamera.fieldOfView = fov;
            }
        }

        // Camera motion effects.
        var fx = FindObjectOfType<CameraEffects>();
        if (fx != null)
        {
            fx.headBobStrength = headBobStrength;
            fx.swayStrength = swayStrength;
            fx.breathingStrength = breathingStrength;
        }

        // Analog filter master control.
        var analog = FindObjectOfType<AnalogFilterController>();
        if (analog != null)
        {
            analog.analogEnabled = analogStrength > 0.001f;
            analog.analogStrength = analogStrength;
        }

        ApplyPostFx();
    }

    // Edits the per-scene Volume instance profile (never the shared asset).
    void ApplyPostFx()
    {
        foreach (var vol in FindObjectsOfType<Volume>())
        {
            if (!vol.isGlobal || vol.profile == null) continue;
            var prof = vol.profile; // accessing .profile clones an instance we can safely edit

            FilmGrain grain;
            if (prof.TryGet(out grain))
            {
                grain.intensity.overrideState = true;
                grain.intensity.value = filmGrain;
            }

            ChromaticAberration chroma;
            if (prof.TryGet(out chroma))
            {
                chroma.intensity.overrideState = true;
                chroma.intensity.value = chromaticAberration;
            }

            MotionBlur mb;
            if (prof.TryGet(out mb))
            {
                mb.active = motionBlur;
            }
            else if (motionBlur)
            {
                // Add a very light motion blur only if the player turned it on.
                mb = prof.Add<MotionBlur>(true);
                mb.intensity.overrideState = true;
                mb.intensity.value = 0.1f;
            }
        }
    }

    // Convenience for the settings menu: change a value, apply, save.
    public void Commit()
    {
        fov = Mathf.Clamp(fov, 60f, 80f);
        Apply();
        Save();
    }

    // ---------- Visual presets (Chunk 3) ----------
    // 0 Low, 1 Medium, 2 High, 3 Analog. These set the URP quality level plus the
    // analog-look strength, so the horror presentation still reads at every setting.
    public static readonly string[] PresetNames = { "Low", "Medium", "High", "Analog" };

    public void ApplyVisualPreset(int preset)
    {
        visualPreset = Mathf.Clamp(preset, 0, 3);

        switch (visualPreset)
        {
            case 0: // Low - clean and cheap, filter mostly off so weak hardware stays readable.
                analogStrength = 0.25f; filmGrain = 0.1f; chromaticAberration = 0.02f; motionBlur = false;
                break;
            case 1: // Medium - restrained analog treatment.
                analogStrength = 0.6f; filmGrain = 0.15f; chromaticAberration = 0.03f; motionBlur = false;
                break;
            case 2: // High - full quality, moderate analog.
                analogStrength = 0.8f; filmGrain = 0.18f; chromaticAberration = 0.035f; motionBlur = false;
                break;
            default: // Analog - the intended lo-fi 1990s home-video look.
                analogStrength = 1f; filmGrain = 0.2f; chromaticAberration = 0.045f; motionBlur = false;
                break;
        }

        // Map presets onto the project's URP quality levels (clamped to what exists).
        int levels = QualitySettings.names.Length;
        int qi = visualPreset == 0 ? 0 : Mathf.Min(visualPreset, levels - 1);
        QualitySettings.SetQualityLevel(qi, true);

        Apply();
        Save();
    }
}
