using UnityEngine;

// SimpleFirstPersonPlayer
// -----------------------
// Grounded, slightly heavy first-person controller for the horror game.
// The goal is to feel like a real person walking through a cramped 1990s house,
// not a fast FPS character.
//
// This script keeps the ORIGINAL gameplay working:
//  - CharacterController movement.
//  - Mouse look.
//  - E to interact, with the ray still coming from the player camera.
//  - Click-to-relock and the on-screen prompt.
//
// New feel added here:
//  - Slower, weighted walking with smooth acceleration and deceleration.
//  - Optional "anxious fast walk" sprint.
//  - Normalized diagonal movement (diagonals are not faster).
//  - Lightly smoothed mouse look with separate X/Y sensitivity, invert-Y,
//    a raw-input option, and a vertical clamp.
//
// The little wobbles (head bob, sway, breathing) live in a separate
// CameraEffects script so this file stays easy to read. This controller
// simply exposes a few read-only values that CameraEffects reads.
public class SimpleFirstPersonPlayer : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The first-person camera. Leave empty to auto-find a child camera.")]
    public Camera playerCamera;

    [Header("Movement Feel")]
    [Tooltip("Normal walking speed in metres per second.")]
    public float walkSpeed = 2.6f;
    [Tooltip("Sprint speed. Should feel like anxious fast walking, not athletic running.")]
    public float sprintSpeed = 4.2f;
    [Tooltip("Allow the player to sprint at all.")]
    public bool allowSprint = true;
    [Tooltip("If true, hold the sprint key. If false, tap it to toggle sprint.")]
    public bool holdToSprint = true;
    public KeyCode sprintKey = KeyCode.LeftShift;
    [Tooltip("Seconds to reach full speed. Small = snappier, larger = heavier.")]
    public float accelerationTime = 0.16f;
    [Tooltip("Seconds to slow to a stop. Slightly quicker than acceleration feels natural.")]
    public float decelerationTime = 0.10f;

    [Header("Gravity")]
    public float gravity = -18f;
    [Tooltip("Small downward force so the controller stays glued to the floor.")]
    public float groundedStick = -2f;

    [Header("Mouse Look")]
    [Tooltip("Horizontal look sensitivity.")]
    public float mouseSensitivity = 2f;
    [Tooltip("Vertical look sensitivity. Kept separate so players can tune it.")]
    public float mouseSensitivityY = 2f;
    [Tooltip("Invert the vertical look direction.")]
    public bool invertY = false;
    [Tooltip("Extra multiplier applied to both axes (used by a settings menu).")]
    public float lookSensitivityScale = 1f;
    [Tooltip("Light smoothing, in seconds. Keep tiny (0.03-0.06) so there is no input lag.")]
    [Range(0f, 0.15f)] public float lookSmoothing = 0.045f;
    [Tooltip("Accessibility: turn smoothing off for perfectly raw mouse input.")]
    public bool rawMouseInput = false;
    [Tooltip("How far up/down the player can look, in degrees.")]
    public float pitchClamp = 80f;

    [Header("Interaction")]
    [Tooltip("How far the interaction ray reaches, in metres.")]
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;
    [Tooltip("Show a small dot only when looking at something usable.")]
    public bool showReticle = true;

    // --- Read-only values other scripts (like CameraEffects) can use ---
    public float PlanarSpeed { get; private set; }     // current horizontal speed (m/s)
    public bool IsGrounded { get; private set; }
    public bool IsSprinting { get; private set; }
    public float StrafeInput { get; private set; }      // -1..1, left/right input for camera roll
    public float ForwardInput { get; private set; }     // -1..1, used for footstep/bob checks
    public Transform PitchPivot { get; private set; }   // the transform that tilts up/down
    public bool LookingAtInteractable { get; private set; } // true when the ray hits a usable object

    // --- Private state ---
    private CharacterController controller;
    private Transform pitchPivot;      // aims the camera up/down (created at runtime if missing)
    private float cameraPitch;         // current up/down angle
    private float verticalSpeed;       // gravity velocity
    private Vector3 planarVelocity;    // smoothed horizontal velocity
    private bool sprintToggled;        // used when holdToSprint is false

    // Smoothed mouse values
    private float smoothMouseX;
    private float smoothMouseY;
    private float mouseXVel;
    private float mouseYVel;

    private string currentPrompt = "";

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }

        SetupCameraRig();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Makes sure there is a small "pitch pivot" between the player body and the camera.
    // The body turns left/right (yaw), the pivot tilts up/down (pitch), and the camera
    // itself is free to do tiny head-bob/sway offsets without fighting the look controls.
    // If the scene camera is a direct child of the player, we insert the pivot at runtime,
    // so every scene works without hand-editing its hierarchy.
    void SetupCameraRig()
    {
        if (playerCamera == null)
        {
            return;
        }

        Transform cam = playerCamera.transform;

        // Already set up (camera sits under a pivot under the player)?
        if (cam.parent != null && cam.parent != transform)
        {
            pitchPivot = cam.parent;
        }
        else
        {
            // Build a pivot at the camera's current height and slide the camera under it.
            GameObject pivotGo = new GameObject("Camera Pitch Pivot");
            pitchPivot = pivotGo.transform;
            pitchPivot.SetParent(transform, false);
            pitchPivot.localPosition = cam.localPosition;   // keep the same eye height
            pitchPivot.localRotation = Quaternion.identity;

            cam.SetParent(pitchPivot, false);
            cam.localPosition = Vector3.zero;
            cam.localRotation = Quaternion.identity;
        }

        PitchPivot = pitchPivot;
    }

    void Update()
    {
        // While paused, do nothing so the world is completely still.
        if (Time.timeScale <= 0f)
        {
            return;
        }

        HandleMouseLock();
        LookAround();
        MovePlayer();
        UpdatePrompt();
        TryInteract();
    }

    void HandleMouseLock()
    {
        // If something unlocks the cursor during gameplay, clicking the game view relocks it.
        // The pause menu owns the Escape key.
        if (Cursor.lockState != CursorLockMode.Locked && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LookAround()
    {
        if (Cursor.lockState != CursorLockMode.Locked || playerCamera == null)
        {
            return;
        }

        float rawX = Input.GetAxisRaw("Mouse X");
        float rawY = Input.GetAxisRaw("Mouse Y");

        // Very light smoothing keeps the look responsive but not mechanically sharp.
        // Raw mode is an accessibility option that skips smoothing entirely.
        if (rawMouseInput || lookSmoothing <= 0f)
        {
            smoothMouseX = rawX;
            smoothMouseY = rawY;
        }
        else
        {
            smoothMouseX = Mathf.SmoothDamp(smoothMouseX, rawX, ref mouseXVel, lookSmoothing);
            smoothMouseY = Mathf.SmoothDamp(smoothMouseY, rawY, ref mouseYVel, lookSmoothing);
        }

        float lookX = smoothMouseX * mouseSensitivity * lookSensitivityScale;
        float lookY = smoothMouseY * mouseSensitivityY * lookSensitivityScale;

        if (invertY)
        {
            lookY = -lookY;
        }

        // Body turns left/right.
        transform.Rotate(Vector3.up * lookX);

        // Pivot tilts up/down, clamped so the player cannot over-rotate.
        cameraPitch -= lookY;
        cameraPitch = Mathf.Clamp(cameraPitch, -pitchClamp, pitchClamp);

        if (pitchPivot != null)
        {
            pitchPivot.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }
    }

    void MovePlayer()
    {
        // Read input and normalize it so diagonal movement is not faster.
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(inputX, 0f, inputZ);
        inputDir = Vector3.ClampMagnitude(inputDir, 1f);

        StrafeInput = inputX;
        ForwardInput = inputZ;

        // Work out the current target speed (walk or sprint).
        IsSprinting = false;
        if (allowSprint)
        {
            if (holdToSprint)
            {
                IsSprinting = Input.GetKey(sprintKey);
            }
            else
            {
                if (Input.GetKeyDown(sprintKey)) sprintToggled = !sprintToggled;
                IsSprinting = sprintToggled;
            }
        }
        // Only count as sprinting if actually trying to move.
        IsSprinting = IsSprinting && inputDir.sqrMagnitude > 0.01f;

        float targetSpeed = IsSprinting ? sprintSpeed : walkSpeed;

        // The direction we want to move in, in world space.
        Vector3 wishDir = transform.right * inputDir.x + transform.forward * inputDir.z;
        Vector3 targetVelocity = wishDir * targetSpeed;

        // Smoothly accelerate toward the target, or decelerate toward a stop.
        // This gives the movement a little weight/inertia without feeling frustrating.
        bool isTryingToMove = inputDir.sqrMagnitude > 0.01f;
        float smoothTime = isTryingToMove ? accelerationTime : decelerationTime;
        planarVelocity = Vector3.MoveTowards(
            planarVelocity,
            targetVelocity,
            (targetSpeed / Mathf.Max(0.01f, smoothTime)) * Time.deltaTime);

        // Gravity so the player stays grounded and can walk down small steps.
        IsGrounded = controller.isGrounded;
        if (IsGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = groundedStick;
        }
        verticalSpeed += gravity * Time.deltaTime;

        Vector3 motion = planarVelocity;
        motion.y = verticalSpeed;
        controller.Move(motion * Time.deltaTime);

        // Report the real horizontal speed for head bob / footsteps.
        Vector3 flat = controller.velocity;
        flat.y = 0f;
        PlanarSpeed = flat.magnitude;
    }

    void TryInteract()
    {
        if (!Input.GetKeyDown(interactKey) || playerCamera == null)
        {
            return;
        }

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance))
        {
            InteractableObject interactable = hit.collider.GetComponentInParent<InteractableObject>();
            if (interactable != null)
            {
                interactable.Interact();
                return;
            }

            InteractionTarget target = hit.collider.GetComponentInParent<InteractionTarget>();
            if (target != null)
            {
                target.Interact();
            }
        }
    }

    void UpdatePrompt()
    {
        currentPrompt = "";
        LookingAtInteractable = false;

        if (playerCamera == null)
        {
            return;
        }

        // The interaction ray still starts at the camera, exactly like before.
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance))
        {
            InteractableObject interactable = hit.collider.GetComponentInParent<InteractableObject>();
            if (interactable != null)
            {
                currentPrompt = interactable.GetPrompt();
                LookingAtInteractable = true;
                return;
            }

            InteractionTarget target = hit.collider.GetComponentInParent<InteractionTarget>();
            if (target != null)
            {
                currentPrompt = target.GetPrompt();
                LookingAtInteractable = true;
            }
        }
    }

    void OnGUI()
    {
        // Small, restrained centre reticle that only appears on interactable objects.
        // No permanent bright crosshair, matching the quiet analog look.
        if (showReticle && LookingAtInteractable && Time.timeScale > 0f && Cursor.lockState == CursorLockMode.Locked)
        {
            float dot = 4f;
            GUI.color = new Color(1f, 1f, 1f, 0.55f);
            GUI.DrawTexture(new Rect(Screen.width / 2f - dot / 2f, Screen.height / 2f - dot / 2f, dot, dot), Texture2D.whiteTexture);
            GUI.color = Color.white;
        }

        if (Time.timeScale > 0f && Cursor.lockState != CursorLockMode.Locked)
        {
            GUIStyle lockStyle = new GUIStyle(GUI.skin.label);
            lockStyle.fontSize = 18;
            lockStyle.alignment = TextAnchor.MiddleCenter;
            lockStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(Screen.width / 2 - 180, Screen.height / 2 + 55, 360, 30), "Click to lock mouse", lockStyle);
        }

        if (string.IsNullOrEmpty(currentPrompt))
        {
            return;
        }

        GUIStyle promptStyle = new GUIStyle(GUI.skin.label);
        promptStyle.fontSize = 18;
        promptStyle.alignment = TextAnchor.MiddleCenter;
        promptStyle.normal.textColor = new Color(0.92f, 0.92f, 0.9f, 0.95f);
        GUI.Label(new Rect(Screen.width / 2 - 180, Screen.height / 2 + 25, 360, 30), currentPrompt, promptStyle);
    }
}
