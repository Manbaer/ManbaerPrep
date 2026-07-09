using UnityEngine;

// Put this on a trigger volume. When the player walks into it,
// the chosen ending plays. Used for the walk-outside Morning ending.
[RequireComponent(typeof(Collider))]
public class EndingTrigger : MonoBehaviour
{
    public string endingFlag = "";
    [TextArea] public string endingMessage = "";
    public float delaySeconds = 5f;

    private bool fired;

    void OnTriggerEnter(Collider other)
    {
        if (fired)
        {
            return;
        }

        // Only the player (with its CharacterController) counts.
        if (other.GetComponent<CharacterController>() == null)
        {
            return;
        }

        fired = true;
        EndingRunner.RunEnding(endingFlag, endingMessage, delaySeconds);
    }
}
