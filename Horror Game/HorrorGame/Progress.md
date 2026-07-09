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

2026-07-08 CurrentTask 15 + 16 + 17: The House After Sleep And All Four Endings

- The final act runs inside HousePrototype as Day 6. Sleeping on Day 5 sets HouseAfterSleepStarted
  (new DayPlan field flagToSetOnSleep) and reloads the house with every dream leaking in:
  - A gas station aisle grows along the bedroom wall (shelf, red neon, "price tags are all bedtimes").
  - Two windows that were never there appear in the hallway, glowing gold with wheat outside.
  - The bathroom light turns hospital white-green and flickers slowly.
  - The TV shows live footage of the player asleep on the couch ("You wave. The you on the couch does not.").
  - The answering machine plays every message from every date at once.
  - The old front door is replaced by the final front door, locked until the house "finishes remembering".
- Five final dream objects appear, one per dream plus the receipt; placing each sets its flag:
  hallway clock (set to 7:42), field fuse (the walls go quiet), Chart 204 (filed away, marked AWAKE),
  the receipt (laid beside the grocery note), the VHS tape ("the small voice says: wake up").
- When all five are placed, FinalActManager quietly sets FrontDoorUnlocked:
  "Somewhere inside the front door, something stops being locked." Objective: "Decide."
- Three movement-based endings (CurrentTask 16), each a quiet sequence that returns to the main menu:
  - Open the door and walk outside into the morning -> EndingLeaveHouse (EndingTrigger volume on the porch).
  - Open the door, then close it -> EndingStayInside (FrontDoorEnding: "Tomorrow will be the same, and that is the point.").
  - Go back to bed -> EndingGoBackToSleep (DayPlan sleepIsEnding; "a small voice says goodnight. You say it back.").
- Secret ending (CurrentTask 17), on the Day 5 impossible-road front door:
  - Hidden condition: use the dream-battery TV remote AND study the impossible map, then try the door a second time.
  - "The door opens all the way this time. Wheat to the horizon, where the street should be." -> SecretEndingFrontDoorEarly.
  - The first door try (a required Day 5 task) can never trigger it, so main progression is safe.
- New beginner-friendly scripts in Assets/Scripts/Story:
  - EndingRunner.cs (flag + final message + delay + return to MainMenu, one ending at a time)
  - EndingTrigger.cs (walk-into-it ending volume)
  - FrontDoorEnding.cs (open onto morning, close for the routine ending)
  - FinalActManager.cs (watches the five final flags, unlocks the door)
  - SecretFrontDoor.cs (hidden early ending conditions)
  - DayManager DayPlan gained flagToSetOnSleep and sleepIsEnding/endingFlag/endingMessage.
- Also set Application.runInBackground / PlayerSettings.runInBackground to true: play mode now keeps
  running when the editor loses focus. This explained two earlier oddities (skipped Starts, stalled coroutines).
- Verified in Play Mode:
  - Secret ending fires only on the second Day 5 door try with both optional items studied, then returns to the menu.
  - Day 6 activates all six leaks and five final objects; AllDreamsComplete set.
  - The door stays locked at 4 of 5 objects and unlocks quietly at 5.
  - All three main endings play their message and return to the main menu; all four ending flags confirmed.
- Validated the scene; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 13 + 14: Item Consequences And Narrative Clue Pass

- Added seven item consequences to the house, driven by GasStationDreamComplete and the individual
  item flags through WorldStateApplier (now 26 changes):
  - A Gas N Go receipt on the kitchen counter: "MILK. BATTERIES. ONE BUS TICKET. Items not purchased: goodbye."
  - The fridge is restocked with gas station food ("a slush cup still slowly spinning").
  - The TV remote appears on the couch and works - only if the dream batteries were bought (TVRemoteWorks).
  - The VHS tape appears beside the TV (VHSCanBePlayed). The VCR exists from day 1 ("blinks 12:00. There is no tape.").
  - Playing the tape is the major story clue: the living room filmed from the doorway,
    a small voice asking if you are asleep, then the VCR ejects the tape by itself (WatchedVHSTape).
  - An impossible floor plan appears on the wall - only if the dream map was taken - showing
    a second bedroom that does not exist, with a small bed drawn in it.
  - The kitchen drawer opens with the dream key: a bus ticket dated six years ago, never used.
  - The front door swaps to a version that opens an inch onto rain, an empty road, and neon,
    then pulls itself shut (ImpossibleRoadDoorEnabled, SawImpossibleRoad).
- Day 5 tasks: read the receipt, play the VHS, open the drawer, try the front door.
  The remote and map are optional bonuses (they may not exist if skipped in the dream - no softlock).
  After the tasks: "You lie down. For the first time in years, the house does not want you to sleep."
- Narrative clue pass (CurrentTask 14):
  - Created Horror Game/HorrorGame/GameDesign/Narrative_Clues.md - the story bible for all in-game text.
  - Defined the two intended readings (caretaker vs patient) and the rules that keep both alive
    (no names, no stated death, only "six years", the other person stays evidence-sized).
  - Inventoried every clue: three answering machine eras, the kitchen paper trail, five photos,
    screens and machines, doors and rooms, and the hospital ward text, each with what it implies.
  - Answered the five open story questions ambiguously and described the emotional setup for the final act.
  - Consistency checklist passes: "six years" everywhere, the errand list echoes, goodnight/Moon ties,
    the child never described directly, no contradictions.
  - Linked Narrative_Clues from 00_START_HERE.
- Fixed a bug where gas station items could fail to be picked up: GasStationItem now hooks its
  listener in Awake and resolves the manager lazily (logged in Bugs.md).
- Verified in Play Mode: all seven consequences active after the gas station, all Day 5 flags set,
  bed gating works, VCR refuses without a tape, empty/decoy checkouts still fail cleanly.
- Validated scenes; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 11 + 12: Gas Station Dream And Item Puzzle

- Built Assets/Scenes/DreamGasStation.unity, the fourth dream (added to Build Settings):
  - Night exterior: empty road with faded dashes, two red gas pumps under a canopy, buzzing
    "GAS 24H" neon with red glow, flickering doorway light, heavy dark-blue fog.
  - Interior: fluorescent store with three aisles. Front signs are normal (SNACKS/SODA, MOTOR OIL/MAPS,
    MAGAZINES/BATTERIES); deeper signs turn wrong in red (MILK/MISSING, MAPS OF YOUR STREET, VHS/MEMORIES).
  - Freezer bank that reflects "a different store", magazine rack where every cover is the player's house,
    payphone with a dial tone "that sounds like slow breathing", cashier counter with an old register.
- Item puzzle (CurrentTask 12), receipt-variant:
  - Register display: "TOTAL: 3 MEMORIES." A receipt by the register reads:
    1 THING TO REMEMBER. 1 THING TO WATCH. 1 THING TO OPEN.
  - Correct memories: the old photo of the house (remember), the unmarked VHS tape (watch),
    the house key on a hook labeled YOURS (open).
  - Other collectibles connect to house problems or are decoys: AA batteries ("the TV remote at home
    has been dead for years"), a road map of only your street, canned soup, a smiling milk carton.
  - Checking out without three memories gives a clear response: "TOTAL - 3 MEMORIES. YOU HAVE N."
  - Success: the register opens, a receipt prints "SOLD TO: the house at the end of your street. Come home.",
    GasStationDreamComplete is set, and a door frame appears standing on the empty road, labeled HOME.
- Item pickups set the flags CurrentTask 13 will need: DreamBatteriesCollected, DreamVHSCollected,
  DreamKeyCollected, DreamMapCollected, DreamPhotoCollected.
- New beginner-friendly scripts in Assets/Scripts/Dream:
  - GasStationItem.cs (pickup + optional collect flag)
  - GasStationPuzzleManager.cs (memory counting, checkout, reveal)
- Day system: Day 4 sleep now loads DreamGasStation; GasStationDreamComplete starts Day 5 (stub plan
  until CurrentTask 13). PrototypeGameManager got gas station start text and a HUD hint.
- Rain is a noted placeholder; mood currently comes from darkness, fog, and neon.
- Verified in Play Mode (full run Day 1 through Day 5):
  - The road door starts hidden; empty and decoy checkouts fail with the counter message.
  - Collecting photo + VHS + key and checking out completes the dream, prints the receipt, reveals the door.
  - The HOME door returns to the house and Day 5 begins; the bed shows the cannot-sleep message.
- Validated the scene; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 10: Medical Memory Consequences

- Added five house changes driven by HospitalDreamComplete through WorldStateApplier (now 19 changes total):
  - A medical document appears on the kitchen table:
    "PATIENT: J. DOE. DIAGNOSIS: illegible. CARE TRANSFERRED TO: HOME. The signature is yours." (HouseMedicalMemoryUnlocked)
  - The answering machine swaps to a clinic message:
    "Your prescription is ready for pickup. It has been ready for six years." (ClinicMessageUnlocked)
  - The bathroom cabinet swaps from empty ("A clean ring where something round once stood.")
    to rows of full, same-named medication bottles.
  - The bedroom turns clinical: a hospital privacy curtain appears beside the bed and the warm
    bedroom light is replaced with a cold white-green one (BedroomChangedToSickRoom).
  - The bedroom dresser drawer unlocks (MedicalDrawerUnlocked). It exists from day 1 as "Stuck. Or locked."
    Story clue inside: "a child-sized hospital bracelet, and a photo of two beds in one room."
- Clues imply caretaking/illness backstory without stating it; nothing is explained outright.
- Day 4 now has real tasks: read the document, hear the clinic message, check the cabinet, open the drawer.
  The bed then shows the cannot-sleep message until the gas station dream exists (CurrentTask 11/12).
- Verified in Play Mode (full run Day 1 through Day 4):
  - Pre-hospital: document hidden, clinic message absent, cabinet empty, warm light, drawer stuck.
  - Post-hospital: all five changes active, all four consequence flags set.
  - Bed refuses before the four Day 4 tasks and stalls gently after them.
- Validated the scene; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 08 + 09: Hospital Dream And Patient File Puzzle

- Built Assets/Scenes/DreamHospital.unity, the third dream (added to Build Settings):
  - Long, dim, foggy 1990s hospital corridor with sickly green lighting and one flickering ward light.
  - Patient rooms 201-204, each holding one object from the house:
    201 the TV, 202 the kitchen table, 203 a childhood photo with the face worn away, 204 the player's own bed.
  - Abandoned nurse station alcove with desk, wristband, four patient charts, and a locked medicine cabinet.
  - Two intercoms: "PATIENT MISSING FROM ROOM 204." and "THE PATIENT IS WHERE THEY SLEEP."
  - Locked ward exit at the far end.
- The patient file puzzle (CurrentTask 09) is a flag-gated chain built entirely from InteractableObject requirements - no new scripts needed:
  1. Read the wristband: "J. DOE. ROOM - WHERE THEY SLEEP." (FoundWristband)
  2. Take chart 204 ("DOES NOT WAKE. The handwriting is yours.") - locked until the wristband (PatientFileFound). Wrong charts give flavor text only.
  3. Open the medicine cabinet - locked until the file ("OPEN WITH PATIENT FILE") - take the bottle labeled FOR SLEEP (HospitalMedicineTaken).
  4. Use the heart monitor beside the bed in room 204 - the flat line lifts (HospitalDreamComplete).
- Completion wakes the ward: bright hallway lights turn on and a discharge paper appears
  ("DISCHARGE - J. DOE. Condition: AWAKE."), via a scene WorldStateApplier triggered by the monitor's onInteracted event.
- The ward exit stays locked ("Somewhere in the ward, a monitor is flat.") until complete, then returns to the house.
- The puzzle objective framing stays safe: the goal is finding and waking the missing patient, never self-harm.
- Day system: Day 3 sleep now loads DreamHospital; HospitalDreamComplete advances to Day 4 (stub plan until CurrentTask 10).
- PrototypeGameManager got hospital start text and a HUD hint line.
- Verified in Play Mode (full run Day 1 -> hallway -> Day 2 -> wheat field -> Day 3 -> hospital):
  - Exit, monitor, cabinet, and chart 204 all refuse out of order with clear messages.
  - The correct chain sets all four flags; lights and discharge paper appear only after the monitor.
  - The exit returns home and Day 4 begins; the bed shows the cannot-sleep message.
- Validated the scene; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 07: Power Consequences In The House

- Built a fuse room behind the previously locked basement door (east side of the house):
  - Dark room with a shelf and a big fuse box.
  - A bare-bulb light that only works after the wheat field dream (LockedBasementLightEnabled).
  - Fuse box message: "Every fuse is brand new. Someone replaced them while you slept." (sets CheckedFuseBox).
- Wired six power consequences into WorldStateApplier, driven by WheatFieldPowerRestored:
  - Fuse room light turns on (LockedBasementLightEnabled).
  - Basement door unlocks (FuseRoomUnlocked) - the new area to enter.
  - TV shows static while switched off via a static overlay (TVCanTurnOnByItself).
  - Kitchen gains a flickering light using SimpleLightFlicker (HousePowerChanged).
  - Two wall junction boxes appear where the walls buzz - kitchen and hallway (WallBuzzingEnabled).
  - The dead radio in the living room is replaced by one receiving strange audio (RadioStrangeAudioUnlocked).
- The radio exists from day 1 as "Radio Dead" ("Dead air. It has been dead for years.") so the change is noticeable.
- Day 3 now has real explore tasks before rest:
  - Look at the TV static, listen to the radio, listen to the buzzing wall, check the fuse room.
  - Bed message afterward: "You lie down, but sleep will not come. Not yet." (until the hospital dream exists).
- Wall buzzing and radio audio are text placeholders for now; real audio comes in the polish pass.
- Verified in Play Mode (full run Day 1 -> hallway -> Day 2 -> wheat field -> Day 3):
  - Pre-power: radio dead, no static, no flicker, fuse room dark, basement door refuses to open.
  - Post-power: all six consequences active, all six story flags set, basement door opens.
  - All four Day 3 tasks set their flags; the bed then shows the cannot-sleep message.
- Validated the scene; no issues. Checked the Unity console; no errors or warnings.

2026-07-08 CurrentTask 05 + 06: Wheat Field Dream And Circuit Puzzle

- Built Assets/Scenes/DreamWheatField.unity, the second dream (added to Build Settings):
  - Night field with dark ambient light, exponential fog, dim blue moonlight, and a pale moon in the sky.
  - ~240 scattered walk-through wheat stalks.
  - Six electrical poles in a line with crossbars, symbol boxes, and lamps: Tree, Clock, Moon, Door, Eye, House.
  - A dead fuse box near the start with the clue:
    "THE HOUSE REMEMBERS - wrong time, open door, covered eye, empty house, goodnight."
  - A distant dark house whose windows and glow light turn on when the circuit is restored.
  - A hidden exit door between two poles that appears after the puzzle.
- Player, managers, and the pause menu were copied from DreamHallway so all scenes share one setup.
- Chose the symbol-order puzzle style (CurrentTask 06):
  - Correct order: Clock, Door, Eye, House, Moon - each maps to a day 2 house observation
    (impossible clocks, ajar bedroom door, covered mirror, empty house photo, the goodnight message).
  - Tree is a decoy pole that is never correct.
  - Correct poles light their lamp and update the objective progress (x of 5 poles).
  - A wrong pole kills the hum and turns every pole off again.
  - Solving lights the distant house, reveals the door, and sets WheatFieldPowerRestored.
- New beginner-friendly scripts in Assets/Scripts/Dream:
  - WheatPole.cs (symbol + lamp, hooks into InteractableObject)
  - WheatFieldPuzzleManager.cs (ordered sequence, reset, reveal, flag)
- Day system hookup:
  - Day 2 sleep now loads DreamWheatField.
  - WheatFieldPowerRestored advances the day count; waking after it starts Day 3.
  - Added a Day 3 stub plan ("The power came back with you. Look around.") until CurrentTask 07 adds real consequences.
- PrototypeGameManager got wheat-field start text and HUD hint lines.
- Verified in Play Mode (full run from Day 1):
  - Day 1 -> hallway dream -> Day 2 -> wheat field.
  - The exit door starts hidden; a wrong pole resets progress to 0.
  - The correct order restores power, lights the house, reveals the door, sets the flag.
  - The door returns to the house and Day 3 begins with the stub objective.
  - Day 3 bed shows the cannot-sleep message.
- Tuned the night look (matte materials, denser fog, dimmer moonlight) after a washed-out first pass.
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