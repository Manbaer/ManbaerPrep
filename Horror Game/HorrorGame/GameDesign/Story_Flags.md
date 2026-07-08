# Story Flags

Story flags track what the player has done in dreams and what the house should change afterward.

Implementation rule for later:

Dreams set flags. The house reads flags. The house changes itself.

No Unity code should be written from this document yet. This is only a design reference.

See [[Dream_Environments]] and [[Real_World_Consequences]] for context.

## Dream Completion Flags

| Flag | Meaning |
| --- | --- |
| `DreamHallwayComplete` | Player solved the endless bedroom hallway. |
| `WheatFieldPowerRestored` | Player restored the circuit in the wheat field. |
| `HospitalDreamComplete` | Player completed the hospital dream. |
| `GasStationDreamComplete` | Player completed the gas station dream. |

## Bedroom Hallway Flags

| Flag | Meaning |
| --- | --- |
| `BedroomMemoryChanged` | A bedroom memory/photo changed after the hallway dream. |
| `AnsweringMachineMessageUnlocked` | A new answering machine message is available. |
| `ClosetUnlockedAfterHallway` | The bedroom closet can now open. |
| `BathroomMirrorCovered` | The bathroom mirror is covered after the mirror loop. |
| `HouseClocksImpossibleTime` | House clocks display the same impossible time. |

## Wheat Field Flags

| Flag | Meaning |
| --- | --- |
| `HousePowerChanged` | The house electrical state changed after the wheat field. |
| `TVCanTurnOnByItself` | TV can trigger static or turn itself on. |
| `LockedBasementLightEnabled` | Basement light now works. |
| `FuseRoomUnlocked` | Fuse room or utility room can now be entered. |
| `WallBuzzingEnabled` | Electrical buzzing can be heard inside walls. |
| `RadioStrangeAudioUnlocked` | Radio can receive strange audio. |

## Hospital Flags

| Flag | Meaning |
| --- | --- |
| `PatientFileFound` | Player found the correct patient file. |
| `HouseMedicalMemoryUnlocked` | Medical-related memories can appear in the house. |
| `BedroomChangedToSickRoom` | Bedroom now has hospital-like details. |
| `ClinicMessageUnlocked` | Answering machine has a call from a clinic. |
| `MedicalDrawerUnlocked` | A locked drawer opens after the hospital dream. |

## Gas Station Flags

| Flag | Meaning |
| --- | --- |
| `DreamBatteriesCollected` | Dream batteries were bought. |
| `DreamVHSCollected` | Dream VHS tape was bought. |
| `DreamKeyCollected` | Dream key was bought. |
| `DreamMapCollected` | Dream map was bought. |
| `HouseDrawerUnlocked` | A locked drawer can open. |
| `TVRemoteWorks` | TV remote now works. |
| `VHSCanBePlayed` | VHS tape can be watched. |
| `ImpossibleRoadDoorEnabled` | Front door can briefly open to an impossible road. |

## Final Act Flags

| Flag | Meaning |
| --- | --- |
| `AllDreamsComplete` | Main dream arc is complete. |
| `HouseAfterSleepStarted` | Final combined-house sequence has begun. |
| `FinalClockPlaced` | Hallway clock has been placed or set. |
| `FinalFusePlaced` | Wheat field fuse has been placed. |
| `FinalPatientFilePlaced` | Hospital patient file has been placed. |
| `FinalReceiptPlaced` | Gas station receipt has been placed. |
| `FinalVHSTapePlayed` | Final VHS has been played. |
| `FrontDoorUnlocked` | Player can make the final ending choice. |
| `EndingLeaveHouse` | Player chose the Morning ending. |
| `EndingStayInside` | Player chose the Routine ending. |
| `EndingGoBackToSleep` | Player chose the Dream House ending. |
| `SecretEndingFrontDoorEarly` | Player found the early front door ending. |

## Example House Logic

These are design notes, not code.

If `WheatFieldPowerRestored = true`:

- Turn on basement light.
- Enable TV static event.
- Unlock fuse room.

If `GasStationDreamComplete = true`:

- Spawn VHS tape.
- Spawn receipt.
- Unlock drawer.

If `HospitalDreamComplete = true`:

- Spawn medical paper.
- Change bedroom props.
- Add hospital curtain.

If `DreamHallwayComplete = true`:

- Change bedroom photo.
- Unlock closet.
- Add answering machine message.

