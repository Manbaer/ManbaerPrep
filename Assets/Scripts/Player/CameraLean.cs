using UnityEngine;

// CameraLean (optional)
// ---------------------
// A small, visual-only lean around corners. It NEVER moves the CharacterController,
// so it can never push the player through a wall - it only slides the camera a few
// centimetres sideways and adds a gentle roll. A short sideways ray keeps the camera
// from poking into walls when leaning in a tight spot.
//
// It layers on top of the normal camera feel through CameraEffects' external offset,
// so it does not fight head bob or sway.
//
// NOTE ON KEYS: this game already uses E to interact, so holding a lean key on E would
// also fire interactions. Because of that this feature is OFF by default; enable it and
// choose non-conflicting keys in the Inspector if you want it. It is intentionally
// optional, exactly as the design brief allows.
public class CameraLean : MonoBehaviour
{
    [Header("Enable")]
    [Tooltip("Off by default so it never clashes with the E interact key.")]
    public bool enableLean = false;
    public KeyCode leanLeftKey = KeyCode.Q;
    [Tooltip("Avoid KeyCode.E here - E is the interact key.")]
    public KeyCode leanRightKey = KeyCode.None;

    [Header("Feel")]
    [Tooltip("How far the camera slides sideways, in metres.")]
    public float leanDistance = 0.28f;
    [Tooltip("How much the view rolls, in degrees.")]
    public float leanRoll = 6f;
    [Tooltip("How quickly the lean eases in and out.")]
    public float leanSpeed = 8f;

    [Header("Wall safety")]
    [Tooltip("Keep the camera this far from walls when leaning.")]
    public float wallMargin = 0.12f;

    private CameraEffects fx;
    private float currentLean;   // -1..1 eased lean amount

    void Start()
    {
        fx = GetComponent<CameraEffects>();
    }

    void LateUpdate()
    {
        if (fx == null) return;

        float target = 0f;
        if (enableLean && Time.timeScale > 0f)
        {
            if (Input.GetKey(leanLeftKey)) target -= 1f;
            if (leanRightKey != KeyCode.None && Input.GetKey(leanRightKey)) target += 1f;
        }

        currentLean = Mathf.Lerp(currentLean, target, Mathf.Clamp01(leanSpeed * Time.deltaTime));

        // How far we can actually lean before hitting a wall on that side.
        float allowed = leanDistance;
        if (Mathf.Abs(currentLean) > 0.01f)
        {
            Vector3 dir = transform.right * Mathf.Sign(currentLean);
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, leanDistance + wallMargin))
            {
                allowed = Mathf.Max(0f, hit.distance - wallMargin);
            }
        }

        // Feed the lean into the shared external camera offset (position + roll).
        fx.externalPositionOffset = transform.right != Vector3.zero
            ? new Vector3(currentLean * allowed, 0f, 0f)
            : Vector3.zero;
        // Roll opposite the slide direction, like leaning your head.
        Vector3 e = fx.externalEulerOffset;
        e.z = -currentLean * leanRoll;
        fx.externalEulerOffset = e;
    }
}
