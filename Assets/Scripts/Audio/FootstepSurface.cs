using UnityEngine;

// FootstepSurface
// ---------------
// Put this on anything the player can walk on to tell the footstep system what
// it sounds like. The interior floor skins (carpet, wood, tile, vinyl) are
// decorative and have no collider, so for those we also add a thin trigger
// collider; the exterior ground pieces already have colliders.
//
// FootstepPlayer casts a short ray straight down and reads this component to
// pick the right footstep sound.
public enum FootstepSurfaceType
{
    Carpet,
    Wood,
    Vinyl,
    Tile,
    Concrete,
    Grass,
    WetPavement
}

public class FootstepSurface : MonoBehaviour
{
    [Tooltip("What this floor sounds like under the player's feet.")]
    public FootstepSurfaceType surface = FootstepSurfaceType.Wood;
}
