using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Raycasts from the player camera and interacts with InteractableObject components.
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    private string currentPrompt = "";

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }
    }

    void Update()
    {
        FindPrompt();

        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    private void FindPrompt()
    {
        currentPrompt = "";

        InteractableObject target = FindInteractable();

        if (target != null)
        {
            currentPrompt = target.GetPrompt();
        }
    }

    private void TryInteract()
    {
        InteractableObject target = FindInteractable();

        if (target != null)
        {
            Debug.Log(target.GetPrompt());
            target.Interact();
        }
    }

    private InteractableObject FindInteractable()
    {
        if (playerCamera == null)
        {
            return null;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            return hit.collider.GetComponentInParent<InteractableObject>();
        }

        return null;
    }

    void OnGUI()
    {
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
