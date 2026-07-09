When playing the scene, here are the bugs I noticed:

- Fixed: You can press esc to unlock mouse but you cannot lock it back.
  - The player can now click the mouse button to lock the cursor again.

- Fixed: When you enter the dream hallway, there is no wall when you turn around after spawning in.
  - Added a back wall behind the dream hallway spawn point.

- Fixed: After interacting with the strange dream door, the changed object in the house did not visibly appear.
  - WorldStateApplier now turns renderers and colliders back on when enabling consequence objects.
  - WorldStateApplier also reapplies changes when its scene finishes loading.

- Fixed: Ambience audio sources refused to play when the AudioSource component had been created by editor scripting and saved into the scene.
  - SimpleAmbientHum now creates its own AudioSource at runtime, which plays reliably.
  - The saved AudioSource components were removed from all five scenes.

- Fixed: Checking the TV/answering machine/kitchen light advanced a stale leftover objective list ("Eat dinner", "Lock the front door").
  - The legacy PrototypeGameManager no longer touches objectives; the DayManager owns all objective text.

- Fixed: Play mode froze (Start methods skipped, coroutines stalled) whenever the Unity editor window lost focus.
  - Cause: Run In Background was off, so the play loop stopped ticking while unfocused.
  - Application.runInBackground and PlayerSettings.runInBackground are now enabled.
  - This was the root cause behind the earlier gas station pickup oddity.

- Fixed: Gas station items could sometimes not be picked up (their pickup hook was added in Start, which the editor occasionally skipped after a scene load hiccup).
  - GasStationItem now hooks its pickup listener in Awake and finds the puzzle manager at pickup time, so load order never matters.

- Fixed: The wrong lamp in the dream hallway kept flickering after the player fixed it.
  - Fixing the lamp now stops the flicker and turns the light off.
  - Resetting the hallway brings the flicker back.

- Fixed: The bed could be used immediately before finishing the house tasks.
  - The modular InteractableObject now supports required story flags.
  - The bed now requires WatchedTV, CheckedAnsweringMachine, and KitchenLightOff before it can load the dream hallway.
  - The bed still uses the old "not ready" warning when the player tries too early.
