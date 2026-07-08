using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WorldStateChange
{
    public string requiredFlag;
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
    public Light[] lightsToChange;
    public bool changeLights;
    public Color lightColor = Color.white;
    public float lightIntensity = 1f;
    public string flagToSetAfterApplying;
}

public class WorldStateApplier : MonoBehaviour
{
    // Place this in a scene to apply changes based on story flags.
    public WorldStateChange[] changes;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        ApplyWorldState();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameObject.scene == scene)
        {
            ApplyWorldState();
        }
    }

    public void ApplyWorldState()
    {
        if (changes == null)
        {
            return;
        }

        StoryFlagManager flagManager = StoryFlagManager.EnsureExists();

        for (int i = 0; i < changes.Length; i++)
        {
            WorldStateChange change = changes[i];

            if (change == null || !flagManager.HasFlag(change.requiredFlag))
            {
                continue;
            }

            SetObjectsActive(change.objectsToEnable, true);
            SetObjectsActive(change.objectsToDisable, false);

            if (change.changeLights)
            {
                ApplyLightChanges(change);
            }

            if (!string.IsNullOrWhiteSpace(change.flagToSetAfterApplying))
            {
                flagManager.SetFlag(change.flagToSetAfterApplying);
            }
        }
    }

    private void SetObjectsActive(GameObject[] objects, bool isActive)
    {
        if (objects == null)
        {
            return;
        }

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                objects[i].SetActive(isActive);
                SetObjectVisible(objects[i], isActive);
            }
        }
    }

    private void SetObjectVisible(GameObject targetObject, bool isVisible)
    {
        // Some prototype objects were hidden by disabling renderers and colliders.
        // Turning those back on makes dream consequences visibly appear again.
        Renderer[] renderers = targetObject.GetComponentsInChildren<Renderer>(true);
        Collider[] colliders = targetObject.GetComponentsInChildren<Collider>(true);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = isVisible;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = isVisible;
        }
    }

    private void ApplyLightChanges(WorldStateChange change)
    {
        if (change.lightsToChange == null)
        {
            return;
        }

        for (int i = 0; i < change.lightsToChange.Length; i++)
        {
            if (change.lightsToChange[i] != null)
            {
                change.lightsToChange[i].color = change.lightColor;
                change.lightsToChange[i].intensity = change.lightIntensity;
            }
        }
    }
}
