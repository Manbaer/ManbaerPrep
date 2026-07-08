# Current_Tasks

This file breaks the game into reasonable development chunks from the current prototype to a complete game.

Use this as the living task roadmap. Each chunk should be finished, tested, and logged in [[Progress]] before moving to the next major chunk.

Related design docs:

- [[Core_Premise]]
- [[Dream_Environments]]
- [[Real_World_Consequences]]
- [[Story_Flags]]
- [[Prototype_Vertical_Slice]]
- [[Ending_Concepts]]
- [[Open_Questions]]

## Current Priority

CurrentTasks 01 through 07 are done. CurrentTask 08 (hospital dream blockout) is the recommended next task.

The project already has:

- Main menu
- HousePrototype scene
- DreamHallway scene
- Basic interaction system
- Story flags
- Objectives
- Scene loading
- WorldStateApplier
- A simple dream-to-house return flow

The next goal is to turn that simple flow into the first real vertical slice from [[Prototype_Vertical_Slice]].

## Roadmap Overview

1. Finish the first real hallway dream loop.
2. Make the first real-world consequences meaningful.
3. Expand the house into a stronger repeatable hub.
4. Add the wheat field dream and power consequences.
5. Add the hospital dream and medical-memory consequences.
6. Add the gas station dream and item consequences.
7. Build the final combined-house act.
8. Add the three main endings and optional secret ending.
9. Polish, test, balance, and prepare a complete playable version.

## CurrentTask 01: Upgrade The Hallway Dream Puzzle

Status: Done (2026-07-08). Verified in Play Mode; see [[Progress]].

Design source:

- [[Dream_Environments#Endless Bedroom Hallway]]
- [[Prototype_Vertical_Slice#Dream Puzzle]]

Goal:

Turn the current simple strange-door dream into the first real dream puzzle.

Build:

- Add repeated hallway sections.
- Add wrong-memory objects:
  - Upside-down painting
  - Flickering lamp
  - Ringing telephone
  - Backward clock
  - Mirror that does not show the player
- Make the player interact with the wrong object in each loop.
- Move the bedroom door closer after each correct interaction.
- Reset the hallway if the player chooses the wrong object.
- Set `DreamHallwayComplete` when the final door is reached.

Keep it simple:

- Use basic placeholder props at first.
- Use clear interaction prompts.
- Keep the puzzle readable before making it scarier.

Done when:

- The player can enter DreamHallway from the bed.
- The player can solve the five-object sequence.
- Wrong choices reset or clearly punish the loop.
- The final bedroom door returns the player to the house.
- `DreamHallwayComplete` is set.
- Unity console has no errors.

## CurrentTask 02: Add First Wake-Up Consequences

Status: Done (2026-07-08, together with CurrentTask 03). Verified in Play Mode; see [[Progress]].

Design source:

- [[Real_World_Consequences#Bedroom Hallway Consequences]]
- [[Story_Flags#Bedroom Hallway Flags]]

Goal:

Make the first completed dream visibly change the real house.

Build:

- Add a changed bedroom photo.
- Add a new answering machine message.
- Unlock the closet.
- Make the bedroom door slightly open after waking.
- Make the bedsheets messy after waking.
- Optional: cover the bathroom mirror if there is a bathroom area.

Story flags:

- `BedroomMemoryChanged`
- `AnsweringMachineMessageUnlocked`
- `ClosetUnlockedAfterHallway`
- `BathroomMirrorCovered`
- `HouseClocksImpossibleTime`

Done when:

- Returning from DreamHallway changes the house.
- The player can notice at least three clear differences.
- The closet becomes newly interactable.
- The answering machine has a new message after the dream.
- The changes persist when reloading the house scene during the session.

## CurrentTask 03: Make The House A Stronger Hub

Status: Done (2026-07-08, together with CurrentTask 02). Verified in Play Mode; see [[Progress]].

Design source:

- [[Core_Premise]]
- [[Real_World_Consequences]]
- [[Open_Questions#House Questions]]

Goal:

Turn the house from a single prototype room into a reusable hub for the whole game.

Build:

- Decide the first full house layout.
- Add or block out key rooms:
  - Bedroom
  - Hallway
  - Kitchen
  - Living room
  - Bathroom
  - Closet
  - Basement or basement door
  - Front door
- Add locked or unusable areas for later dreams.
- Add simple interactables for photos, notes, TV, answering machine, doors, and lights.
- Make the front door present early, even if it cannot be used yet.

Done when:

- The house feels like a place the player can return to repeatedly.
- Locked areas clearly suggest future changes.
- The first hallway dream consequences have somewhere meaningful to appear.
- The player understands the normal day routine before the next dream.

## CurrentTask 04: Add Day Progression And Sleep Routine

Status: Done (2026-07-08). Verified in Play Mode; see [[Progress]]. Day 2 currently ends with "sleep will not come" until the wheat field dream exists (CurrentTask 05); add "DreamHallway" replacement scene name to the Day 2 plan once built.

Design source:

- [[Core_Premise#Main Idea]]
- [[Prototype_Vertical_Slice#House Tasks]]

Goal:

Create a simple repeatable day structure that can support multiple dreams.

Build:

- Add day numbers or internal progression stages.
- Let different tasks appear on different days.
- Keep tasks ordinary:
  - Watch TV.
  - Check answering machine.
  - Eat or inspect kitchen.
  - Turn off lights.
  - Check a newly changed room.
  - Go to bed.
- Make bed availability depend on required tasks.
- Update objective text as the player progresses.

Done when:

- Day 1 leads to the hallway dream.
- Day 2 can begin after the hallway dream.
- The player cannot skip straight to sleep unless required tasks are complete.
- The system is simple enough to keep expanding.

## CurrentTask 05: Build Wheat Field Dream Blockout

Status: Done (2026-07-08, together with CurrentTask 06). Verified in Play Mode; see [[Progress]]. Audio (wind/hum) is still placeholder-silent; add during the polish pass.

Design source:

- [[Dream_Environments#Infinite Wheat Field With Electrical Poles]]

Goal:

Create the second dream environment as a playable blockout.

Build:

- Create a wheat field or field-like open space.
- Add a distant house with no power.
- Add electrical poles stretching into the distance.
- Add pole boxes with symbols:
  - Moon
  - House
  - Eye
  - Clock
  - Tree
  - Door
- Add a dead fuse box clue: "The house remembers the order."
- Add a door that appears after the puzzle is solved.

Done when:

- The player can enter the wheat field dream from sleep progression.
- The dream has a clear start, puzzle area, and exit.
- The space has wind, electrical hum, and distant-house mood.
- The dream can return the player to the house.

## CurrentTask 06: Add Wheat Field Circuit Puzzle

Status: Done (2026-07-08, together with CurrentTask 05). Symbol-order style chosen; clue ties to the day 2 house changes. Verified in Play Mode; see [[Progress]].

Design source:

- [[Dream_Environments#Puzzle Restore the Circuit]]
- [[Dream_Environments#Puzzle Variant Follow the Humming]]

Goal:

Make the wheat field dream solveable.

Build:

- Choose one puzzle style first:
  - Symbol order from the house
  - Loudest electrical humming
  - Simple combination of both
- Let the player activate poles in the correct order.
- Turn on distant lights after correct choices.
- Rearrange or reset the field after wrong choices.
- Set `WheatFieldPowerRestored` when complete.

Done when:

- The player can understand the circuit clue.
- Correct pole choices visibly restore power.
- Wrong choices have a clear result.
- The exit door appears after completion.
- `WheatFieldPowerRestored` is set.

## CurrentTask 07: Add Power Consequences In The House

Status: Done (2026-07-08). Verified in Play Mode; see [[Progress]]. Wall buzzing and radio audio are text placeholders until the audio/polish pass.

Design source:

- [[Real_World_Consequences#Wheat Field Consequences]]
- [[Story_Flags#Wheat Field Flags]]

Goal:

Make the wheat field dream unlock electrical changes and a new area.

Build:

- Turn on basement or utility room light.
- Let the player enter a previously dark or blocked area.
- Add TV static when the TV is off.
- Add flickering kitchen light.
- Add wall buzzing near electrical areas.
- Add radio with strange audio.

Story flags:

- `HousePowerChanged`
- `TVCanTurnOnByItself`
- `LockedBasementLightEnabled`
- `FuseRoomUnlocked`
- `WallBuzzingEnabled`
- `RadioStrangeAudioUnlocked`

Done when:

- The house visibly reacts to restored power.
- A new area becomes available.
- The TV/radio/electrical changes add story mood.
- The player has a reason to explore before the next sleep cycle.

## CurrentTask 08: Build Hospital Dream Blockout

Status: Not started

Design source:

- [[Dream_Environments#Hospital Dream]]

Goal:

Create the third dream environment as a playable blockout.

Build:

- Add a 1990s hospital ward.
- Add looping hospital hallway.
- Add abandoned nurse station.
- Add patient rooms 201 through 204.
- Put one house object in each room:
  - TV
  - Kitchen table
  - Childhood photo
  - Player's bed
- Add intercom clues.
- Add locked medicine cabinet.

Done when:

- The hospital dream has a readable layout.
- The player can identify room differences.
- The player can find the room decorated like the bedroom.
- The dream has an exit path ready for the puzzle.

## CurrentTask 09: Add Hospital Patient File Puzzle

Status: Not started

Design source:

- [[Dream_Environments#Puzzle Find the Correct Patient File]]
- [[Dream_Environments#Puzzle Variant Wrong Rooms]]

Goal:

Make the hospital dream solveable without using self-harm as an objective.

Build:

- Add a patient wristband.
- Add patient charts or files.
- Match name or number to the correct room.
- Unlock the medicine cabinet.
- Retrieve a medical item.
- Use the item on a machine, mannequin patient, or memory scene.
- Add discharge paper and exit door.
- Set `HospitalDreamComplete` and `PatientFileFound`.

Done when:

- The puzzle is clear and safe in objective framing.
- The hospital hallway changes after the solution.
- The exit unlocks.
- `HospitalDreamComplete` is set.

## CurrentTask 10: Add Medical Memory Consequences

Status: Not started

Design source:

- [[Real_World_Consequences#Hospital Consequences]]
- [[Story_Flags#Hospital Flags]]

Goal:

Make the house reveal medical or caretaking backstory after the hospital dream.

Build:

- Add medical document on kitchen table.
- Add clinic answering machine message.
- Add old medication bottles in bathroom cabinet.
- Add hospital privacy curtain or clinical lighting in bedroom.
- Unlock a new drawer with story clue.

Story flags:

- `HouseMedicalMemoryUnlocked`
- `BedroomChangedToSickRoom`
- `ClinicMessageUnlocked`
- `MedicalDrawerUnlocked`

Done when:

- The player sees the bedroom become less safe and more clinical.
- The new clues imply backstory without explaining everything.
- The unlocked drawer gives a meaningful story object or clue.

## CurrentTask 11: Build Gas Station Dream Blockout

Status: Not started

Design source:

- [[Dream_Environments#Infinite Gas Station]]

Goal:

Create the fourth dream environment as a playable blockout.

Build:

- Add 1990s gas station exterior at night.
- Add rain, neon sign, empty road mood.
- Add interior aisles:
  - Snacks
  - Motor oil
  - Magazines
  - Soda
  - Maps
  - Batteries
  - VHS tapes
- Add cashier counter and old register.
- Add payphone.
- Make deeper aisles become stranger.

Done when:

- The gas station has an inside, outside, aisles, and counter.
- The player can explore and collect placeholder items.
- The gas station mood is clear before final art polish.

## CurrentTask 12: Add Gas Station Item Puzzle

Status: Not started

Design source:

- [[Dream_Environments#Puzzle Buy the Correct Items]]
- [[Dream_Environments#Puzzle Variant Receipt Puzzle]]

Goal:

Make the gas station dream solveable and connect it to real house needs.

Build:

- Add item collection:
  - Batteries
  - VHS tape
  - Key
  - Map
  - Optional photo or milk carton
- Add register total: "TOTAL: 3 MEMORIES."
- Add receipt clue:
  - 1 thing to remember
  - 1 thing to watch
  - 1 thing to open
- Let the player place correct items on the counter.
- Print receipt with home address.
- Unlock exit door.
- Set `GasStationDreamComplete`.

Done when:

- The player can solve the item selection puzzle.
- Correct items connect to house problems.
- Wrong item choices give a clear response.
- The gas station exit returns to the house.

## CurrentTask 13: Add Item Consequences In The House

Status: Not started

Design source:

- [[Real_World_Consequences#Gas Station Consequences]]
- [[Story_Flags#Gas Station Flags]]

Goal:

Make gas station dream items appear in the real house.

Build:

- Add receipt on kitchen counter.
- Add gas station food in fridge.
- Make TV remote work.
- Spawn VHS tape beside TV.
- Let the player play VHS tape.
- Add map showing impossible rooms.
- Unlock drawer with dream key.
- Let the front door briefly open to an impossible road.

Story flags:

- `DreamBatteriesCollected`
- `DreamVHSCollected`
- `DreamKeyCollected`
- `DreamMapCollected`
- `HouseDrawerUnlocked`
- `TVRemoteWorks`
- `VHSCanBePlayed`
- `ImpossibleRoadDoorEnabled`

Done when:

- Dream items clearly become real items.
- VHS playback reveals a major story clue.
- The front door feels closer to being important.

## CurrentTask 14: Narrative Clue Pass

Status: Not started

Design source:

- [[Core_Premise#Story Interpretation]]
- [[Open_Questions#Story Questions]]

Goal:

Make sure the player can piece together the story without direct exposition.

Build:

- Write answering machine messages.
- Write VHS tape moments.
- Write receipt text.
- Write medical document text.
- Write photo and drawer clues.
- Decide what is implied about:
  - The player
  - The house
  - The hospital
  - Someone who used to live there
  - Why the player stopped leaving
- Keep the mystery ambiguous.

Done when:

- Each dream adds a new piece of story.
- The player can form theories without being told the full answer.
- Clues do not contradict each other.
- The final act has enough emotional setup.

## CurrentTask 15: Build The House After Sleep

Status: Not started

Design source:

- [[Real_World_Consequences#Final Act Consequences]]
- [[Ending_Concepts#Final Act Structure]]

Goal:

Create the final combined-house sequence.

Build:

- Make dreams and reality overlap:
  - Bedroom door opens into gas station aisle.
  - Hallway windows show wheat field.
  - Bathroom has hospital lighting.
  - TV plays footage of player sleeping.
  - Answering machine plays messages from different dates.
  - Front door opens into endless hallway.
- Add final dream objects:
  - Clock
  - Fuse
  - Patient file
  - Receipt
  - VHS tape
- Let the player place or use each object in the house.
- Unlock the front door after all final objects are used.

Story flags:

- `AllDreamsComplete`
- `HouseAfterSleepStarted`
- `FinalClockPlaced`
- `FinalFusePlaced`
- `FinalPatientFilePlaced`
- `FinalReceiptPlaced`
- `FinalVHSTapePlayed`
- `FrontDoorUnlocked`

Done when:

- The house feels like every dream is leaking into it.
- The final object puzzle is clear.
- The front door unlock moment feels quiet and important.

## CurrentTask 16: Add Main Endings

Status: Not started

Design source:

- [[Ending_Concepts#Main Ending Choice]]
- [[Ending_Concepts#Ending 1 Leave the House Morning]]
- [[Ending_Concepts#Ending 2 Stay Inside Routine]]
- [[Ending_Concepts#Ending 3 Go Back to Sleep Dream House]]

Goal:

Add the three main endings using player movement instead of a menu choice.

Build:

- Walk outside -> Morning ending.
- Close the door -> Routine ending.
- Go back upstairs to bed -> Dream House ending.
- Add ending flags:
  - `EndingLeaveHouse`
  - `EndingStayInside`
  - `EndingGoBackToSleep`
- Add simple ending scenes or controlled sequences.
- Keep each ending quiet and unsettling.

Done when:

- The player can naturally choose each ending.
- Each ending has a distinct emotional meaning.
- The game can return to main menu or credits after an ending.

## CurrentTask 17: Add Secret Front Door Ending

Status: Not started

Design source:

- [[Ending_Concepts#Secret Ending Open the Front Door Early]]
- [[Open_Questions#Ending Questions]]

Goal:

Add an optional hidden ending where the player opens the front door early.

Build:

- Decide hidden conditions.
- Let front door open early only after those conditions are met.
- Show the endless wheat field outside instead of a normal street.
- Set `SecretEndingFrontDoorEarly`.
- Keep this ending disturbing but not clearly good or bad.

Done when:

- The secret ending is discoverable but not obvious.
- It supports the idea that the outside world may already be part of the dream.
- It does not break the main progression.

## CurrentTask 18: Full Game Polish Pass

Status: Not started

Goal:

Make the whole game feel cohesive, readable, and complete.

Build:

- Improve lighting in every scene.
- Improve sound and ambience.
- Add simple 1990s props and textures.
- Make interaction prompts consistent.
- Make objective text consistent.
- Add pause/settings polish.
- Add save/checkpoint strategy if needed.
- Add credits.
- Remove temporary debug-only objects from final flow.

Done when:

- All major scenes feel like the same game.
- The player always understands what they can interact with.
- The game has a beginning, middle, final act, and endings.
- There are no obvious placeholder blockers.

## CurrentTask 19: Full QA And Bug Fixing

Status: Not started

Goal:

Test the complete game from start to finish.

Test:

- Main menu to ending flow.
- Every dream puzzle.
- Every house consequence.
- Every locked and unlocked room.
- Every story flag.
- Every ending.
- Pause menu.
- Scene loading.
- Restarting or replaying after an ending.

Done when:

- No console errors.
- No broken progression.
- No missing exits.
- No softlocks.
- No major visual problems.
- All known bugs are either fixed or listed in [[Bugs]].

## CurrentTask 20: Release Candidate

Status: Not started

Goal:

Prepare a complete playable build.

Build:

- Create final build settings.
- Confirm scene order.
- Test a fresh build outside the Unity Editor.
- Add final title screen details.
- Add simple credits.
- Archive current design docs and progress.
- Make a final known-issues list if needed.

Done when:

- A full build can be played from start to finish.
- The player can reach all intended endings.
- Progression does not require developer tools.
- The project has a clean final progress note.

## Notes For Future Codex Work

- Keep scripts beginner-friendly.
- Keep systems Inspector-driven when possible.
- Add comments for non-obvious logic.
- Do not build all dream environments at once.
- Finish and test one dream-to-house loop before starting the next.
- Update [[Progress]] after each completed task chunk.
- If something breaks, update [[Bugs]].

