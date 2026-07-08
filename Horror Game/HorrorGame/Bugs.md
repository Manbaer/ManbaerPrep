When playing the scene, here are the bugs I noticed:

- Fixed: You can press esc to unlock mouse but you cannot lock it back.
  - The player can now click the mouse button to lock the cursor again.

- Fixed: When you enter the dream hallway, there is no wall when you turn around after spawning in.
  - Added a back wall behind the dream hallway spawn point.

- Fixed: After interacting with the strange dream door, the changed object in the house did not visibly appear.
  - WorldStateApplier now turns renderers and colliders back on when enabling consequence objects.
  - WorldStateApplier also reapplies changes when its scene finishes loading.

- Fixed: The bed could be used immediately before finishing the house tasks.
  - The modular InteractableObject now supports required story flags.
  - The bed now requires WatchedTV, CheckedAnsweringMachine, and KitchenLightOff before it can load the dream hallway.
  - The bed still uses the old "not ready" warning when the player tries too early.
