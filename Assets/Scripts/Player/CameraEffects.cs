using UnityEngine;

// CameraEffects
// -------------
// Adds the small physical imperfections that make the player feel like a real
// person inside the house: gentle head bob while walking, a tiny roll when
// strafing, a small camera lag on sharp turns, and almost invisible breathing
// while standing still.
//
// This script should sit on the PLAYER CAMERA itself. It only ever writes a
// small LOCAL offset (position + rotation) on the camera, so it never fights
// the mouse look (which lives on the body and the pitch pivot). When the player
// stops, everything eases smoothly back to neutral - it never snaps.
//
// Every effect has a 0-1 strength so a settings menu can dial it down or off
// without changing how fast the player moves or how interaction works.
[RequireComponent(typeof(Transform))]
public class CameraEffects : MonoBehaviour
{
    [Header("Reference")]
    [Tooltip("The player controller that reports real velocity. Auto-found in a parent if empty.")]
    public SimpleFirstPersonPlayer player;

    [Header("Master Strength (accessibility)")]
    [Tooltip("Overall head-bob amount. 0 = off, 1 = full.")]
    [Range(0f, 1f)] public float headBobStrength = 1f;
    [Tooltip("Overall sway / roll / turn-lag amount. 0 = off, 1 = full.")]
    [Range(0f, 1f)] public float swayStrength = 1f;
    [Tooltip("Breathing amount while standing still. 0 = off, 1 = full.")]
    [Range(0f, 1f)] public float breathingStrength = 1f;

    [Header("Head Bob (walking)")]
    [Tooltip("Up/down bob distance in metres.")]
    public float walkBobVertical = 0.022f;
    [Tooltip("Left/right bob distance in metres.")]
    public float walkBobHorizontal = 0.016f;
    [Tooltip("Steps per second while walking.")]
    public float walkStepsPerSecond = 1.75f;
    [Tooltip("Tiny bob rotation in degrees.")]
    public float walkBobRotation = 0.22f;

    [Header("Head Bob (sprint)")]
    [Tooltip("Sprinting bobs only slightly more - not an aggressive bounce.")]
    public float sprintBobMultiplier = 1.35f;
    public float sprintStepsPerSecond = 2.3f;

    [Header("Strafe Roll")]
    [Tooltip("Max roll while strafing, in degrees.")]
    public float strafeRoll = 0.35f;

    [Header("Turn Lag")]
    [Tooltip("How far the camera lags behind a fast turn, in degrees.")]
    public float turnLagAmount = 0.9f;
    [Tooltip("How quickly the camera catches up after a turn. Higher = snappier.")]
    public float turnLagRecovery = 12f;

    [Header("Breathing (idle)")]
    [Tooltip("Idle up/down movement in metres. Keep this tiny.")]
    public float breathVertical = 0.0035f;
    [Tooltip("Idle rotation in degrees. Keep this tiny.")]
    public float breathRotation = 0.045f;
    [Tooltip("Roughly one breath every this many seconds.")]
    public float breathPeriod = 4.2f;
    [Tooltip("Story hook: set above 1 during scripted moments for heavier breathing. Never automatic.")]
    public float breathingMoodMultiplier = 1f;

    [Header("Smoothing")]
    [Tooltip("How quickly offsets ease toward their target. Higher = tighter.")]
    public float smoothing = 10f;
    [Tooltip("When reading an important object you can disable bob from other scripts.")]
    public bool effectsEnabled = true;

    // Neutral pose captured on Start (usually zero, since the camera sits under a pivot).
    private Vector3 neutralPosition;
    private Quaternion neutralRotation;

    // Running values.
    private float bobTimer;
    private Vector3 currentPosOffset;
    private Vector3 currentRotOffset;   // stored as euler degrees (pitch, yaw, roll)
    private float breathSeed;

    // Turn-lag tracking.
    private float lastYaw;
    private float yawLag;

    private bool ready;

    void Start()
    {
        // Random start so breathing does not look like an obvious repeating loop.
        breathSeed = Random.Range(0f, 100f);
    }

    // Capture the resting pose only after the controller has finished building the
    // camera rig (it may reparent this camera during its own Start). Doing this on
    // the first LateUpdate guarantees every Start has already run.
    void CaptureNeutral()
    {
        if (player == null)
        {
            player = GetComponentInParent<SimpleFirstPersonPlayer>();
        }

        neutralPosition = transform.localPosition;
        neutralRotation = transform.localRotation;

        if (player != null)
        {
            lastYaw = player.transform.eulerAngles.y;
        }

        ready = true;
    }

    void LateUpdate()
    {
        if (!ready)
        {
            CaptureNeutral();
        }

        // Freeze completely while paused or when effects are switched off.
        if (Time.timeScale <= 0f || !effectsEnabled || player == null)
        {
            EaseToNeutral();
            return;
        }

        float dt = Time.deltaTime;

        // How fast are we actually moving on the floor? (0 when standing still.)
        float speed = player.PlanarSpeed;
        bool moving = speed > 0.15f && player.IsGrounded;

        Vector3 targetPos = Vector3.zero;
        Vector3 targetRot = Vector3.zero;   // euler degrees

        if (moving)
        {
            // Advance the step cycle based on how fast we are going, so bob matches the feet.
            float stepsPerSecond = player.IsSprinting ? sprintStepsPerSecond : walkStepsPerSecond;
            float speedFactor = Mathf.Clamp01(speed / (player.IsSprinting ? player.sprintSpeed : player.walkSpeed));
            bobTimer += dt * stepsPerSecond * Mathf.PI * 2f * Mathf.Max(0.35f, speedFactor);

            float amp = (player.IsSprinting ? sprintBobMultiplier : 1f) * headBobStrength * speedFactor;

            // Soft sine motion. Horizontal runs at half the frequency of vertical,
            // which is what makes a natural "figure-eight" walking bob.
            float vertical = Mathf.Sin(bobTimer) * walkBobVertical * amp;
            float horizontal = Mathf.Cos(bobTimer * 0.5f) * walkBobHorizontal * amp;

            targetPos.y += vertical;
            targetPos.x += horizontal;

            // Tiny rotation so it does not feel like a floating slider.
            targetRot.z += Mathf.Cos(bobTimer * 0.5f) * walkBobRotation * amp;
            targetRot.x += Mathf.Sin(bobTimer) * walkBobRotation * 0.5f * amp;
        }
        else
        {
            // Standing still: extremely subtle, slightly irregular breathing.
            // Two sine waves at different rates keep it from looking like a clean loop.
            float t = (Time.time + breathSeed);
            float breath = Mathf.Sin(t * (Mathf.PI * 2f / breathPeriod))
                         + 0.35f * Mathf.Sin(t * (Mathf.PI * 2f / (breathPeriod * 1.7f)));
            breath *= 0.5f;

            float bStrength = breathingStrength * breathingMoodMultiplier;
            targetPos.y += breath * breathVertical * bStrength;
            targetRot.x += breath * breathRotation * bStrength;
        }

        // --- Strafe roll: lean very slightly into sideways movement ---
        float strafe = Mathf.Clamp(player.StrafeInput, -1f, 1f);
        targetRot.z += -strafe * strafeRoll * swayStrength;

        // --- Turn lag: the camera trails a fast turn, then catches up ---
        float currentYaw = player.transform.eulerAngles.y;
        float yawDelta = Mathf.DeltaAngle(lastYaw, currentYaw);
        lastYaw = currentYaw;
        // Feed the turn into the lag, then recover toward zero.
        yawLag += yawDelta;
        yawLag = Mathf.Lerp(yawLag, 0f, Mathf.Clamp01(turnLagRecovery * dt));
        yawLag = Mathf.Clamp(yawLag, -turnLagAmount * 3f, turnLagAmount * 3f);
        targetRot.y += -Mathf.Clamp(yawLag, -turnLagAmount, turnLagAmount) * swayStrength;

        // Ease the actual offsets toward their targets so nothing ever snaps.
        float k = Mathf.Clamp01(smoothing * dt);
        currentPosOffset = Vector3.Lerp(currentPosOffset, targetPos, k);
        currentRotOffset = Vector3.Lerp(currentRotOffset, targetRot, k);

        Apply();
    }

    // Smoothly return the camera to its resting pose (used when paused / disabled).
    void EaseToNeutral()
    {
        float k = Mathf.Clamp01(smoothing * Time.unscaledDeltaTime);
        currentPosOffset = Vector3.Lerp(currentPosOffset, Vector3.zero, k);
        currentRotOffset = Vector3.Lerp(currentRotOffset, Vector3.zero, k);
        bobTimer = 0f;
        Apply();
    }

    void Apply()
    {
        transform.localPosition = neutralPosition + currentPosOffset;
        transform.localRotation = neutralRotation * Quaternion.Euler(currentRotOffset);
    }
}
