2026-07-07

- Built the first Unity prototype.
- Added a first-person player controller with WASD movement, mouse look, and E interaction.
- Added two scenes:
  - Assets/Scenes/HousePrototype.unity
  - Assets/Scenes/DreamHallway.unity
- Built a simple 1990s-style house room with a TV, answering machine, kitchen light, and bed.
- Added three house tasks:
  - Check the TV.
  - Check the answering machine.
  - Turn off the kitchen light.
- Added a bed interaction that sends the player to the dream hallway after the tasks are done.
- Built a simple dream hallway with repeating doors and one strange door.
- Added strange-door interaction that returns the player to the house.
- Added a hidden change in the house that appears after opening the strange dream door.
- Added beginner-friendly scripts in Assets/Scripts:
  - SimpleFirstPersonPlayer.cs
  - InteractionTarget.cs
  - PrototypeGameManager.cs
- Added simple on-screen task text, messages, and interaction prompts.
- Verified both prototype scenes with Unity scene validation.
- Checked the Unity console; no errors or warnings were reported.

2026-07-07 Bug Fixes

- Fixed mouse locking:
  - Esc still unlocks the mouse.
  - Left mouse click now locks the mouse again.
  - Updated the on-screen controls text to mention click-to-relock.
- Fixed the dream hallway spawn:
  - Added a wall behind the player spawn so turning around no longer shows an open end.
- Verified the dream hallway and house scenes with Unity scene validation.
- Checked the Unity console; no errors or warnings were reported.
