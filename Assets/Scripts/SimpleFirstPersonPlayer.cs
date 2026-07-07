using UnityEngine;

public class SimpleFirstPersonPlayer : MonoBehaviour
{
    // This script handles basic first-person movement, mouse looking, and pressing E on objects.
    public Camera playerCamera;
    public float moveSpeed = 4f;
    public float mouseSensitivity = 2f;
    public float interactDistance = 3f;

    private CharacterController controller;
    private float cameraPitch;
    private float verticalSpeed;
    private string currentPrompt = "";

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLock();
        LookAround();
        MovePlayer();
        UpdatePrompt();
        TryInteract();
    }

    void HandleMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Cursor.lockState != CursorLockMode.Locked && Input.GetMouseButtonDown(0))
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

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -75f, 75f);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move = Vector3.ClampMagnitude(move, 1f);

        if (controller.isGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = -1f;
        }

        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        move.y = verticalSpeed;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void TryInteract()
    {
        if (!Input.GetKeyDown(KeyCode.E) || playerCamera == null)
        {
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
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

        if (playerCamera == null)
        {
            return;
        }

        // Cast a short ray from the camera to see if the player is looking at something usable.
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            InteractionTarget target = hit.collider.GetComponentInParent<InteractionTarget>();

            if (target != null)
            {
                currentPrompt = target.GetPrompt();
            }
        }
    }

    void OnGUI()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
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
        promptStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(Screen.width / 2 - 180, Screen.height / 2 + 25, 360, 30), currentPrompt, promptStyle);
    }
}
