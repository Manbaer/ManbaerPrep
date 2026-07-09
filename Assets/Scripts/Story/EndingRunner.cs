using System.Collections;
using UnityEngine;

// Plays out a quiet ending: set the ending flag, show one last message,
// wait a moment, then return to the main menu.
// Any script can call EndingRunner.RunEnding(...).
public class EndingRunner : MonoBehaviour
{
    private static bool endingStarted;

    public static void RunEnding(string endingFlag, string message, float delaySeconds)
    {
        // Only one ending can play at a time.
        if (endingStarted)
        {
            return;
        }

        endingStarted = true;

        GameObject runnerObject = new GameObject("Ending Runner");
        EndingRunner runner = runnerObject.AddComponent<EndingRunner>();
        runner.StartCoroutine(runner.EndingRoutine(endingFlag, message, delaySeconds));
    }

    private IEnumerator EndingRoutine(string endingFlag, string message, float delaySeconds)
    {
        if (!string.IsNullOrWhiteSpace(endingFlag))
        {
            StoryFlagManager.EnsureExists().SetFlag(endingFlag);
        }

        if (PrototypeGameManager.Instance != null)
        {
            PrototypeGameManager.Instance.ShowMessage(message);
        }
        else
        {
            Debug.Log(message);
        }

        yield return new WaitForSecondsRealtime(delaySeconds);

        endingStarted = false;
        SceneLoader.EnsureExists().LoadScene("MainMenu");
    }
}
