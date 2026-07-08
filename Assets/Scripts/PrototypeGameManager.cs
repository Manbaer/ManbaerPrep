using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeGameManager : MonoBehaviour
{
    // This manager remembers completed tasks and the dream-door change between scene loads.
    public static PrototypeGameManager Instance;

    public string houseSceneName = "HousePrototype";
    public string dreamSceneName = "DreamHallway";

    private bool checkedTV;
    private bool checkedAnsweringMachine;
    private bool turnedOffKitchenLight;
    private bool strangeDoorOpened;

    private string message = "Check the small things before bed.";
    private float messageTimer = 4f;

    void Awake()
    {
        GameManager.EnsureExists();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void Update()
    {
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;
        }
    }

    public void UseTarget(InteractionTarget target)
    {
        if (target.kind == InteractionKind.TV)
        {
            checkedTV = true;
            StoryFlagManager.EnsureExists().SetFlag(StoryFlags.WatchedTV);
            ObjectiveManager.EnsureExists().CompleteCurrentObjective();
            ShowMessage("The TV shows static. A laugh track plays under the snow.");
        }
        else if (target.kind == InteractionKind.AnsweringMachine)
        {
            checkedAnsweringMachine = true;
            StoryFlagManager.EnsureExists().SetFlag("CheckedAnsweringMachine");
            ObjectiveManager.EnsureExists().SetObjective("Turn off the kitchen light");
            ShowMessage("One old message: 'Are you still there?'");
        }
        else if (target.kind == InteractionKind.KitchenLight)
        {
            turnedOffKitchenLight = true;
            StoryFlagManager.EnsureExists().SetFlag("KitchenLightOff");
            ObjectiveManager.EnsureExists().SetObjective("Go to bed");

            if (target.lightToTurnOff != null)
            {
                target.lightToTurnOff.enabled = false;
            }

            ShowMessage("The kitchen light clicks off. The room feels too quiet.");
        }
        else if (target.kind == InteractionKind.Bed)
        {
            TrySleep();
        }
        else if (target.kind == InteractionKind.DreamDoor)
        {
            strangeDoorOpened = true;
            StoryFlagManager.EnsureExists().SetFlag(StoryFlags.DreamOneComplete);
            ObjectiveManager.EnsureExists().SetObjective("Wake up and check the house");
            ShowMessage("The strange door opens onto your bedroom.");
            SceneManager.LoadScene(houseSceneName);
        }
    }

    private void TrySleep()
    {
        if (!checkedTV || !checkedAnsweringMachine || !turnedOffKitchenLight)
        {
            ShowMessage("You are not ready to sleep yet.");
            return;
        }

        ShowMessage("You lie down. The house stretches into a hallway.");
        StoryFlagManager.EnsureExists().SetFlag(StoryFlags.WentToBed);
        ObjectiveManager.EnsureExists().SetObjective("Enter the dream");
        SceneManager.LoadScene(dreamSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyHouseChanges();
        ShowMessage(GetSceneStartMessage(scene.name));
    }

    private string GetSceneStartMessage(string sceneName)
    {
        if (sceneName == dreamSceneName)
        {
            return "The hallway remembers things wrong. Fix them.";
        }

        if (HasFinishedFirstDream())
        {
            return "You wake up. Something new is in the room.";
        }

        return "A quiet day in the house.";
    }

    private bool HasFinishedFirstDream()
    {
        // The old strange-door flow used DreamOneComplete.
        // The new hallway puzzle sets DreamHallwayComplete. Accept both.
        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();
        return strangeDoorOpened
            || flagManager.HasFlag(StoryFlags.DreamOneComplete)
            || flagManager.HasFlag(StoryFlags.DreamHallwayComplete);
    }

    private void ApplyHouseChanges()
    {
        GameObject kitchenLightObject = GameObject.Find("Kitchen Light Source");

        if (kitchenLightObject != null)
        {
            Light kitchenLight = kitchenLightObject.GetComponent<Light>();

            if (kitchenLight != null)
            {
                kitchenLight.enabled = !turnedOffKitchenLight;
            }
        }

        GameObject changedObject = GameObject.Find("Dream Change Object");

        if (changedObject == null)
        {
            return;
        }

        bool shouldShowDreamChange = HasFinishedFirstDream();
        Renderer[] renderers = changedObject.GetComponentsInChildren<Renderer>(true);
        Collider[] colliders = changedObject.GetComponentsInChildren<Collider>(true);

        foreach (Renderer item in renderers)
        {
            item.enabled = shouldShowDreamChange;
        }

        foreach (Collider item in colliders)
        {
            item.enabled = shouldShowDreamChange;
        }
    }

    public void ShowMessage(string newMessage)
    {
        message = newMessage;
        messageTimer = 5f;
    }

    void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 18;
        labelStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(Screen.width / 2 - 5, Screen.height / 2 - 10, 20, 20), "+", labelStyle);

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == houseSceneName && DayManager.Instance == null)
        {
            // The DayManager draws its own day-aware task list when present.
            DrawHouseTasks(labelStyle);
        }
        else if (sceneName == dreamSceneName)
        {
            GUI.Label(new Rect(20, 20, 520, 35), "Dream: interact with the one wrong thing in each part of the hallway.", labelStyle);
        }

        if (messageTimer > 0f)
        {
            GUI.Box(new Rect(20, Screen.height - 90, 520, 55), message);
        }

        GUI.Label(new Rect(20, Screen.height - 35, 620, 25), "WASD move  |  Mouse look  |  E interact  |  Esc pause", labelStyle);
    }

    private void DrawHouseTasks(GUIStyle labelStyle)
    {
        string tvText = checkedTV ? "Done" : "Not done";
        string machineText = checkedAnsweringMachine ? "Done" : "Not done";
        string lightText = turnedOffKitchenLight ? "Done" : "Not done";

        GUI.Label(new Rect(20, 20, 420, 35), "House tasks", labelStyle);
        GUI.Label(new Rect(20, 50, 420, 30), "TV: " + tvText, labelStyle);
        GUI.Label(new Rect(20, 75, 420, 30), "Answering machine: " + machineText, labelStyle);
        GUI.Label(new Rect(20, 100, 420, 30), "Kitchen light: " + lightText, labelStyle);
    }
}
