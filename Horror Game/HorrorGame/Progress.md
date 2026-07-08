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


2026-07-08 CurrentTask 01: Hallway Dream Puzzle

- Rebuilt Assets/Scenes/DreamHallway.unity into the first real dream puzzle from Current_Tasks CurrentTask 01.
- Built a long repeating hallway with five sections. Each section has five placeholder props:
  - Painting (left wall, with a top-mark so upside down is visible)
  - Standing lamp (right side, with its own point light)
  - Telephone on a stand (left side)
  - Wall clock (right wall)
  - Mirror (left wall)
- One prop per section is the "wrong memory", marked with a red floating sign and a clearer prompt:
  - Section 1: upside-down painting
  - Section 2: flickering lamp (real flickering light)
  - Section 3: ringing telephone
  - Section 4: backward clock
  - Section 5: empty mirror
- Interacting with the right wrong prop advances the puzzle and slides the whole end wall (with the bedroom door) 2.4 units closer.
- Interacting with any other prop resets the hallway: door back to the far end, solved props restored, player teleported to the start.
- The bedroom door reuses InteractableObject requirements: it stays locked until the puzzle sets HallwayPuzzleSolved, then sets DreamHallwayComplete, updates the objective, and loads HousePrototype.
- Added beginner-friendly scripts in Assets/Scripts/Dream:
  - HallwayDreamPuzzleManager.cs (step order, door movement, resets, objectives, messages)
  - HallwayMemoryObject.cs (marks props as the wrong memory, hooks into InteractableObject.onInteracted)
  - SimpleLightFlicker.cs (flickering lamp light)
- Added HallwayPuzzleSolved and DreamHallwayComplete constants to StoryFlags.cs.
- Small reuse edits:
  - PrototypeGameManager.ShowMessage is now public so the puzzle can use the existing message box.
  - PrototypeGameManager accepts DreamHallwayComplete or the old DreamOneComplete for the wake-up change.
  - InteractableObject shows its missing-requirement warning in the on-screen message box too.
  - HousePrototype's WorldStateApplier now reacts to DreamHallwayComplete instead of DreamOneComplete.
- Removed the old strange door and its label from DreamHallway (replaced by the bedroom door puzzle).
- Added placeholder materials (DreamHall*, DreamPainting, DreamLamp, DreamPhone, DreamClock, DreamMirror, DreamBedroomDoor) in Assets/Materials.
- Verified in Play Mode:
  - House tasks then bed loads DreamHallway.
  - The locked bedroom door refuses to open before the puzzle is done.
  - A wrong choice resets the hallway, the door position, and the player.
  - Solving painting -> lamp -> telephone -> clock -> mirror moves the door from z=75 to z=63 and sets HallwayPuzzleSolved.
  - The bedroom door sets DreamHallwayComplete and returns to HousePrototype.
  - The Dream Change Object appears in the house and HouseChangedAfterDreamOne is set.
- Validated both scenes; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 04: Day Progression And Sleep Routine

- Added a simple day system to HousePrototype:
  - Assets/Scripts/Story/DayManager.cs
  - Assets/Scripts/Story/DreamBed.cs
- The current day is computed from completed dream flags, so it survives scene reloads with no extra saving:
  - No dreams done = Day 1. DreamHallwayComplete = Day 2. Future dreams extend the list.
- Each day is an Inspector-driven DayPlan:
  - Day start objective text
  - Task labels for the on-screen list
  - Required story flags before the bed allows sleep
  - Dream scene to load on sleep (empty = "sleep will not come" message until that dream is built)
- Day 1: check the TV, check the answering machine, turn off the kitchen light, then sleep into DreamHallway.
- Day 2: listen to the new message, look at the changed bedroom photo, check the covered bathroom mirror, look in the fridge.
  - The changed photo, covered mirror, and new answering machine message now set their own day 2 flags
    (SawChangedPhoto, SawCoveredMirror, HeardNewMessage), and the fridge sets CheckedFridge.
  - Day 2 sleep shows "You lie down, but sleep will not come. Not yet." until the wheat field dream exists.
- The bed no longer loads scenes itself; DreamBed routes it through DayManager.TrySleep, so bed rules change per day.
- DayManager draws a day-aware task HUD (Day number + Done/Not done list); the old PrototypeGameManager task list steps aside when a DayManager is present.
- Verified in Play Mode:
  - Day 1: bed refuses before tasks, works after, loads DreamHallway.
  - Solving the dream and returning starts Day 2 with the new objective and task list.
  - Day 2: bed refuses before the four tasks; after them it shows the cannot-sleep message and stays in the house.
- Validated the scene; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 02 + 03: House Hub And First Wake-Up Consequences

- Rebuilt Assets/Scenes/HousePrototype.unity from one room into a full house blockout (CurrentTask 03):
  - Living room (TV, couch, answering machine, wall clock, strange Dream Change Object, front door)
  - Kitchen (counter, fridge, table with a note, locked drawer, wall clock, kitchen light and switch)
  - Hallway connecting everything
  - Bedroom (bed, dresser with photo, closet, bedroom door)
  - Bathroom (sink, mirror, toilet, bathtub)
  - Closet corner inside the bedroom
- Locked areas that suggest future dreams:
  - Front door: "You do not go outside anymore." (unlocks in the final act)
  - Basement door: "Locked. Cold air seeps from under the door." (wheat field dream)
  - Kitchen drawer: "The drawer will not open. Something rattles inside." (gas station dream)
  - Closet: locked until the hallway dream is finished
- Added swinging doors with a new beginner-friendly script:
  - Assets/Scripts/Interaction/SimpleDoor.cs (hinge + child slab, reuses InteractableObject and its lock flags)
- Added an on-screen feedback option to InteractableObject:
  - New screenMessage field shows in the existing message box after a successful interaction.
  - Used for photos, notes, clocks, and mirrors.
- Wired all first wake-up consequences (CurrentTask 02) through WorldStateApplier, driven by DreamHallwayComplete:
  - Bedroom photo swaps to a changed version (BedroomMemoryChanged).
  - Answering machine swaps to one with a new message (AnsweringMachineMessageUnlocked).
  - Closet unlocks (ClosetUnlockedAfterHallway).
  - Bedroom door swaps from closed to slightly ajar.
  - Bedsheets swap from neat to messy.
  - Bathroom mirror gets covered with a sheet (BathroomMirrorCovered).
  - Both clocks swap to the same impossible time 0:66 (HouseClocksImpossibleTime).
  - The old Dream Change Object still appears (HouseChangedAfterDreamOne).
- Added new StoryFlags constants for all of the above plus future locks (FuseRoomUnlocked, HouseDrawerUnlocked, FrontDoorUnlocked).
- Player now starts in the bedroom; room floors use different placeholder colors for readability.
- Verified in Play Mode:
  - Closet, front door, basement door, and drawer refuse to open before the dream and show warnings.
  - The bedroom door opens and closes normally.
  - Tasks then bed still leads to DreamHallway; solving the dream returns to the house.
  - All seven consequences applied and all five story flags set after waking.
  - The closet opens after the dream; the new answering machine message plays and sets CheckedAnsweringMachine.
  - Reloading the house scene mid-session keeps every change (WorldStateApplier reapplies on load).
- Validated the scene; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 Hallway Puzzle: Any-Order Fix + Lamp Flicker Fix

- The five wrong memories in DreamHallway can now be fixed in any order instead of a forced sequence.
  - HallwayDreamPuzzleManager now counts solved wrong memories instead of tracking a step index.
  - The objective shows progress, for example: "Fix the wrong memories in the hallway. (2 of 5 fixed)".
  - Touching a normal prop still resets everything.
- Fixed a bug where the flickering lamp kept flickering after being fixed.
  - HallwayMemoryObject has a new flickerToStopWhenSolved field.
  - Fixing the lamp disables the flicker and turns the light off; a hallway reset turns both back on.
- Re-tested in Play Mode:
  - Fixing the lamp first, then the painting, works.
  - A decoy interaction resets progress and brings the flicker back.
  - A fully scrambled solve order (mirror, lamp, clock, painting, telephone) completes the puzzle.
  - The bedroom door still sets DreamHallwayComplete and returns to the house.
- Checked the Unity console; no errors or warnings.