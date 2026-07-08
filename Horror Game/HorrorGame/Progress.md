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

2026-07-07 Modular Structure Verification

- Confirmed the modular scripts exist under Assets/Scripts:
  - Core/GameManager.cs
  - Story/StoryFlagManager.cs
  - Story/StoryFlags.cs
  - Story/ObjectiveManager.cs
  - Story/WorldStateApplier.cs
  - Interaction/InteractableObject.cs
  - Player/PlayerInteraction.cs
  - SceneManagement/SceneLoader.cs
  - UI/SceneSelectorMenu.cs
- Validated the modular scripts in Unity; no script errors were reported.
- Verified MainMenu, HousePrototype, and DreamHallway scene validation; all were clean.
- Confirmed the scenes are wired with the modular system:
  - MainMenu has GameManager and SceneSelectorMenu.
  - HousePrototype has GameManager, four InteractableObject components, and WorldStateApplier.
  - DreamHallway has GameManager and one InteractableObject on the strange door.
- Wired the bed InteractableObject to set WentToBed, update the objective, and load DreamHallway through SceneLoader.
- Ran the requested Play Mode flow:
  - Started from MainMenu.
  - Loaded HousePrototype.
  - Interacted with the TV.
  - Confirmed WatchedTV was set.
  - Confirmed the objective advanced to Eat dinner.
  - Interacted with the bed.
  - Confirmed WentToBed was set.
  - Confirmed the objective updated to Enter the dream.
  - Confirmed DreamHallway loaded.
  - Interacted with the strange dream door.
  - Confirmed DreamOneComplete was set.
  - Confirmed the game returned to HousePrototype.
  - Confirmed WorldStateApplier activated the dream-change object in the house.
  - Confirmed HouseChangedAfterDreamOne was set.
- Tested the developer SceneSelectorMenu:
  - Confirmed it lists MainMenu, HousePrototype, and DreamHallway.
  - Confirmed it can load DreamHallway by index.
- Checked the Unity console after verification; no errors or warnings were reported.

2026-07-07 Dream Consequence Visibility Fix

- Fixed a regression where the dream-change object in the house became active after the dream door but stayed invisible.
- Updated WorldStateApplier so enabled consequence objects also enable their child renderers and colliders.
- Updated WorldStateApplier to reapply scene changes when its scene loads.
- Re-tested the flow in Play Mode:
  - MainMenu loaded HousePrototype.
  - House interactions loaded DreamHallway.
  - The strange door set DreamOneComplete.
  - Returning to HousePrototype set HouseChangedAfterDreamOne.
  - Dream Change Object was active with all renderers and colliders enabled.
- Checked the Unity console after the fix; no errors or warnings were reported.

2026-07-08 Bed Task Requirement Fix

- Fixed a bug where the player could use the bed immediately without finishing the house tasks.
- Added simple required story flags to InteractableObject.
- Kept the old on-screen "not ready" warning for blocked bed use.
- Wired the bed to require:
  - WatchedTV
  - CheckedAnsweringMachine
  - KitchenLightOff
- Re-tested the flow in Play Mode:
  - Starting from MainMenu loaded HousePrototype.
  - Using the bed immediately kept the player in HousePrototype and did not set WentToBed.
  - Completing the TV, answering machine, and kitchen light tasks allowed the bed to set WentToBed and load DreamHallway.
  - Opening the strange dream door still returned to the house and showed the dream-change object.
- Validated HousePrototype; no scene issues were reported.
- Checked the Unity console after the fix; no errors or warnings were reported.

2026-07-08 Design Documentation Organization

- Organized the pasted game design notes into Obsidian markdown files in Horror Game/HorrorGame.
- Created:
  - Horror Game/HorrorGame/GameDesign/Core_Premise.md
  - Horror Game/HorrorGame/GameDesign/Dream_Environments.md
  - Horror Game/HorrorGame/GameDesign/Real_World_Consequences.md
  - Horror Game/HorrorGame/GameDesign/Ending_Concepts.md
  - Horror Game/HorrorGame/GameDesign/Story_Flags.md
  - Horror Game/HorrorGame/GameDesign/Prototype_Vertical_Slice.md
  - Horror Game/HorrorGame/GameDesign/Open_Questions.md
- Updated Horror Game/HorrorGame/00_START_HERE.md into an Obsidian index with wiki links.
- Documented the core dream-to-house loop, the four planned dream environments, real-world consequences, story flags, the first vertical slice, and ending concepts.
- Documented that dream actions should rewrite the real house.
- Documented that the first major dream should be the Endless Bedroom Hallway because it directly connects to the player's bedroom and sleep loop.
- Documented the suggested dream order:
  - Endless Bedroom Hallway
  - Infinite Wheat Field
  - Hospital Dream
  - Infinite Gas Station
- Documented that the ending should center on the front door as the final symbol of real change.
- Documented that main ending choices should be made through player movement:
  - Leave the house.
  - Stay inside.
  - Go back to sleep.
- Noted open decisions about the player's past, the house's true nature, the final room layout, first post-dream clues, closet contents, and secret ending conditions.
- No Unity code was written and Assets was not edited.

2026-07-08 Progress Log Cleanup

- Merged the duplicate DevLog progress notes into this main Progress.md file.
- Removed Horror Game/HorrorGame/DevLog/Progress.md so there is only one progress log.

2026-07-08 Current_Tasks Roadmap

- Created Horror Game/HorrorGame/Current_Tasks.md.
- Added a milestone roadmap that develops the game in reasonable chunks from the current prototype to a complete release candidate.
- Organized future work around the first hallway vertical slice, house consequences, wheat field dream, hospital dream, gas station dream, final combined-house act, endings, polish, QA, and release preparation.
- Linked Current_Tasks from 00_START_HERE.md.
- No Unity code was written and Assets was not edited.

2026-07-08 AGENTS.md Path Update

- Updated AGENTS.md to point future work at the real Obsidian notes folder: Horror Game/HorrorGame.
- Added the new GameDesign files and Current_Tasks.md to the recommended reading list.
- Clarified that future work should follow one current task chunk at a time, then verify and log progress.
- No Unity code was written and Assets was not edited.
