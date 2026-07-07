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

2026-07-07 Menu System

- Added a simple Unity UI main menu scene:
  - Assets/Scenes/MainMenu.unity
- Added main menu buttons:
  - Play loads Assets/Scenes/HousePrototype.unity.
  - Settings opens a basic settings panel.
  - Exit Game calls Application.Quit and logs a debug message for the Unity Editor.
- Added a pause menu to the gameplay scenes:
  - Assets/Scenes/HousePrototype.unity
  - Assets/Scenes/DreamHallway.unity
- Added pause menu buttons:
  - Resume unpauses the game and locks/hides the cursor.
  - Settings opens a basic settings panel with a Back button.
  - Exit to Main Menu unpauses the game and loads MainMenu.
- Updated the player controls so Escape is used for the pause menu.
- Added beginner-friendly scripts:
  - Assets/Scripts/MainMenuController.cs
  - Assets/Scripts/PauseMenuController.cs
- Updated Build Settings so MainMenu is the first scene.
- Verified MainMenu, HousePrototype, and DreamHallway with Unity scene validation.
- Ran a Play Mode smoke test:
  - MainMenu renders.
  - Play loads HousePrototype.
  - Pause, Settings, Back, Resume, and Exit to Main Menu work.
- Checked the Unity console; no errors or warnings were reported.
