using UnityEngine;

// SettingsMenu
// ------------
// A simple settings panel drawn with Unity's IMGUI (OnGUI), matching the rest
// of this prototype's on-screen text. It edits the shared GameSettings live and
// saves on every change, so options take effect immediately.
//
// The existing menu "Settings" buttons open it (see MainMenuController /
// PauseMenuController), and a Back button returns to whatever opened it.
public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance { get; private set; }
    public static bool IsOpen { get; private set; }

    private System.Action onBack;
    private Vector2 scroll;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnsureExists()
    {
        if (Instance == null)
        {
            var go = new GameObject("SettingsMenu");
            go.AddComponent<SettingsMenu>();
        }
    }

    // Open the panel. onBackAction runs when the player presses Back.
    public void Open(System.Action onBackAction)
    {
        onBack = onBackAction;
        IsOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        IsOpen = false;
        var cb = onBack;
        onBack = null;
        if (cb != null) cb.Invoke();
    }

    void OnGUI()
    {
        if (!IsOpen) return;

        GameSettings s = GameSettings.Instance;
        if (s == null) return;

        // Draw in front of every other on-screen (IMGUI) element, e.g. the task HUD.
        GUI.depth = -1000;

        // Dark full-screen backdrop so the gameplay HUD does not bleed through.
        Color prev = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.75f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = prev;

        float w = 460f;
        float h = Mathf.Min(Screen.height - 40f, 560f);
        float x = (Screen.width - w) / 2f;
        float y = (Screen.height - h) / 2f;

        GUI.Box(new Rect(x, y, w, h), "Settings");

        // Scroll area for all the controls.
        Rect view = new Rect(x + 12, y + 34, w - 24, h - 84);
        Rect content = new Rect(0, 0, w - 48, 940);
        scroll = GUI.BeginScrollView(view, scroll, content);

        float ly = 0f;
        float lw = content.width;

        GUI.Label(new Rect(0, ly, lw, 22), "LOOK"); ly += 26;
        s.fov = LabeledSlider("Field of view", s.fov, 60f, 80f, ref ly, lw, "F0");
        s.sensitivityX = LabeledSlider("Mouse sensitivity X", s.sensitivityX, 0.2f, 8f, ref ly, lw, "F2");
        s.sensitivityY = LabeledSlider("Mouse sensitivity Y", s.sensitivityY, 0.2f, 8f, ref ly, lw, "F2");
        s.invertY = LabeledToggle("Invert vertical look", s.invertY, ref ly, lw);
        s.rawMouse = LabeledToggle("Raw mouse (no smoothing)", s.rawMouse, ref ly, lw);
        ly += 8;

        GUI.Label(new Rect(0, ly, lw, 22), "CAMERA MOTION"); ly += 26;
        s.headBobStrength = LabeledSlider("Head bob", s.headBobStrength, 0f, 1f, ref ly, lw, "P0");
        s.swayStrength = LabeledSlider("Camera sway", s.swayStrength, 0f, 1f, ref ly, lw, "P0");
        s.breathingStrength = LabeledSlider("Breathing", s.breathingStrength, 0f, 1f, ref ly, lw, "P0");
        s.screenShake = LabeledToggle("Screen shake / impulses", s.screenShake, ref ly, lw);
        ly += 8;

        GUI.Label(new Rect(0, ly, lw, 22), "ANALOG / VISUAL"); ly += 26;
        s.analogStrength = LabeledSlider("Analog filter strength", s.analogStrength, 0f, 1f, ref ly, lw, "P0");
        s.filmGrain = LabeledSlider("Film grain", s.filmGrain, 0f, 0.6f, ref ly, lw, "F2");
        s.chromaticAberration = LabeledSlider("Chromatic aberration", s.chromaticAberration, 0f, 0.3f, ref ly, lw, "F2");
        s.motionBlur = LabeledToggle("Motion blur", s.motionBlur, ref ly, lw);

        // Visual presets (Chunk 3).
        GUI.Label(new Rect(0, ly, lw, 22), "Visual preset:"); ly += 24;
        float bw = (lw - 18) / 4f;
        for (int i = 0; i < GameSettings.PresetNames.Length; i++)
        {
            bool sel = s.visualPreset == i;
            string label = (sel ? "> " : "") + GameSettings.PresetNames[i];
            if (GUI.Button(new Rect(i * (bw + 6), ly, bw, 26), label))
            {
                s.ApplyVisualPreset(i);
            }
        }
        ly += 34;

        GUI.Label(new Rect(0, ly, lw, 22), "GAMEPLAY"); ly += 26;
        s.showReticle = LabeledToggle("Interaction reticle", s.showReticle, ref ly, lw);
        bool holdBefore = s.sprintHold;
        s.sprintHold = LabeledToggle("Sprint: hold (off = toggle)", s.sprintHold, ref ly, lw);

        GUI.EndScrollView();

        // Buttons at the bottom.
        if (GUI.Button(new Rect(x + w - 130, y + h - 42, 118, 30), "Back"))
        {
            Close();
        }
        if (GUI.Button(new Rect(x + 12, y + h - 42, 150, 30), "Reset to defaults"))
        {
            ResetDefaults(s);
        }

        // Apply + save whenever something changed this frame.
        if (GUI.changed)
        {
            s.Commit();
        }
    }

    float LabeledSlider(string label, float value, float min, float max, ref float ly, float lw, string fmt)
    {
        GUI.Label(new Rect(0, ly, lw, 20), label + ":  " + value.ToString(fmt));
        ly += 20;
        float v = GUI.HorizontalSlider(new Rect(0, ly + 4, lw, 18), value, min, max);
        ly += 28;
        return v;
    }

    bool LabeledToggle(string label, bool value, ref float ly, float lw)
    {
        bool v = GUI.Toggle(new Rect(0, ly, lw, 22), value, "  " + label);
        ly += 26;
        return v;
    }

    void ResetDefaults(GameSettings s)
    {
        s.fov = 68f; s.sensitivityX = 2f; s.sensitivityY = 2f; s.invertY = false; s.rawMouse = false;
        s.headBobStrength = 1f; s.swayStrength = 1f; s.breathingStrength = 1f; s.screenShake = true;
        s.showReticle = true; s.sprintHold = true;
        s.ApplyVisualPreset(3); // Analog
    }
}
