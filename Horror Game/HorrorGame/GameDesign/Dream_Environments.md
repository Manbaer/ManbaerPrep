# Dream Environments

Each dream should follow the same design chain:

Dream environment
-> dream puzzle
-> symbolic meaning
-> real-world consequence
-> new house change or story flag

Dreams set flags. The house reads flags. The house changes itself.

See [[Real_World_Consequences]] and [[Story_Flags]] for how dream results affect the house.

## Endless Bedroom Hallway

### Dream Concept

The player appears in an impossibly long hallway. At the far end is their own bedroom door. No matter how long they walk, the door does not get closer.

The hallway repeats the same objects:

- Lamp
- Painting
- Clock
- Telephone
- Plant
- Mirror
- Bedroom door

Each repetition is slightly wrong:

- One painting is upside down.
- One clock is ticking backward.
- One lamp is flickering.
- One mirror shows the hallway behind the player, but not the player.
- One telephone rings only when the player walks away from it.

### Puzzle: Fix the Wrong Memory

The hallway repeats in sections. Each section looks almost identical, but one object is wrong. The player must notice the pattern and interact with the wrong object in each loop.

Example sequence:

1. Fix the upside-down painting.
2. Turn off the flickering lamp.
3. Answer the ringing phone.
4. Stop the backward clock.
5. Cover the mirror.

After each correct interaction, the bedroom door gets slightly closer. If the player interacts with the wrong object, the hallway resets.

### Symbolic Meaning

The hallway represents the player trying to return to comfort: their bedroom. The dream forces them to confront distorted pieces of their routine before they can reach safety.

### Real-World Connection

When the player wakes up, the bedroom has changed:

- A bedroom photo changes.
- The answering machine has a new message.
- All clocks in the house show the same impossible time.
- The bathroom mirror is covered with a sheet.

### House Changes

After this dream, the bedroom should feel less safe:

- The bedroom door is slightly open when it used to be closed.
- The bedsheets are messy even if the player made the bed.
- A new photo appears on the dresser.
- The closet door is unlocked.
- The answering machine has a strange message.

### Story Flags

- `DreamHallwayComplete = true`
- `BedroomMemoryChanged = true`
- `AnsweringMachineMessageUnlocked = true`

This should be the first major dream because it directly connects to the player's bedroom and sleep loop.

## Infinite Wheat Field With Electrical Poles

### Dream Concept

The player wakes up in a huge wheat field at night or sunset. Endless electrical poles stretch into the distance. Some poles have lights or transformers attached.

The field is quiet except for:

- Wind
- Buzzing wires
- Distant thunder
- Crows
- Electrical humming

The player sees their house far away in the field, but it has no power.

### Puzzle: Restore the Circuit

The electrical poles form a broken circuit. The player needs to follow the poles and restart the power by finding the broken sequence.

Each pole has a small metal box with a symbol:

- Moon
- House
- Eye
- Clock
- Tree
- Door

At the start, there is a dead fuse box with a clue:

> The house remembers the order.

The answer is based on objects from the real house. For example, if the player saw `Clock -> Door -> Tree painting -> Family photo -> TV` before sleeping, they must activate poles in that matching order.

### Puzzle Variant: Follow the Humming

Some poles hum louder than others. The player must listen carefully and activate the poles with the strongest hum.

Each correct pole turns on a light in the distance. Each wrong pole causes the field to rearrange.

### Dream Event

Once the correct poles are activated:

- The power lines spark.
- Lights turn on one by one across the field.
- The distant house lights up.
- The player hears a phone ringing from inside the field.
- A door appears between two electrical poles.

### Symbolic Meaning

The wheat field represents power, distance, and the outside world. The house is visible, but unreachable and powerless until the player reconnects it.

### Real-World Connection

When the player wakes up, the house power has changed:

- A previously dead room now has working lights.
- Some lights flicker randomly if the player made mistakes.
- The TV turns on by itself.
- Electrical buzzing can now be heard inside the walls.

### House Changes

After this dream:

- Basement light now works.
- TV shows static even when off.
- Kitchen light flickers.
- A radio starts receiving strange audio.
- The player can access a dark room that was previously unusable.

Good unlock targets:

- Basement
- Garage
- Utility room
- Locked hallway
- Fuse room

### Story Flags

- `WheatFieldPowerRestored = true`
- `HousePowerChanged = true`
- `TVCanTurnOnByItself = true`
- `LockedBasementLightEnabled = true`

## Hospital Dream

### Dream Concept

The player appears in an empty 1990s hospital ward. The lights are dim. The hallway loops. The nurse station is abandoned.

Patient rooms contain objects from the player's house:

- A bedroom lamp
- A family photo
- A TV
- A kitchen chair
- A locked medicine cabinet

The player hears a hospital intercom:

> Patient missing from room 204.

The strange part is that the patient room is decorated like the player's bedroom.

### Puzzle: Find the Correct Patient File

The player needs to find a patient file, match it to a room number, unlock the medicine cabinet, and use the medical item on a locked machine, mannequin patient, or memory scene.

Puzzle structure:

1. Find patient wristband.
2. Match name or number to patient chart.
3. Find the correct room.
4. Open medicine cabinet.
5. Retrieve medical item.
6. Use it on a medical machine or mannequin patient.
7. The hospital hallway changes.

This keeps the medical nightmare atmosphere without making the player's goal self-harm.

### Puzzle Variant: Wrong Rooms

Every hospital room looks almost the same, but each has one object from the player's house:

- Room 201: TV playing static.
- Room 202: Kitchen table.
- Room 203: Childhood photo.
- Room 204: Player's bed.

The intercom gives clues:

> The patient is where they sleep.

The correct room is the one with the bed.

### Dream Event

When the player solves it:

- The heart monitor starts beeping.
- The hallway lights turn on one by one.
- The patient bed is empty.
- A discharge paper appears.
- The exit door unlocks.

### Symbolic Meaning

The hospital connects to physical condition, avoidance, isolation, and fear of being treated or fixed.

### Real-World Connection

When the player wakes up:

- A medical document appears on the kitchen table.
- A locked room in the house now has hospital wallpaper or fluorescent lighting.
- The house phone receives a call from a hospital.
- The bedroom feels less like a safe place and more like a sick room.

### House Changes

After this dream:

- The player's bedroom has a hospital privacy curtain.
- The bathroom cabinet contains old medication bottles.
- The answering machine has a call from a clinic.
- The kitchen table has a medical bill.
- A new locked drawer opens.

This can reveal backstory without spelling everything out.

### Story Flags

- `HospitalDreamComplete = true`
- `PatientFileFound = true`
- `HouseMedicalMemoryUnlocked = true`
- `BedroomChangedToSickRoom = true`

## Infinite Gas Station

### Dream Concept

The player enters a 1990s gas station at night.

Outside:

- Darkness
- Rain
- Buzzing neon sign
- Empty road

Inside:

- Fluorescent lights
- Humming refrigerators
- Old snack shelves
- Cigarettes behind the counter
- Payphone
- Old cash register

The aisles stretch forever. Every aisle seems normal at first:

- Chips
- Cereal
- Motor oil
- Magazines
- Soda
- Candy
- Maps
- Batteries
- VHS tapes

The deeper the player walks, the more wrong the products become:

- Every magazine has the player's house on the cover.
- Milk cartons show missing-person photos.
- Road maps only show the player's street.
- Receipts print things the player has not bought yet.
- Freezer doors reflect a different location.

### Puzzle: Buy the Correct Items

The player needs to collect items from the endless aisles and bring them to the counter.

The cash register displays a total, but not in money:

> TOTAL: 3 MEMORIES

The player must choose three items that connect to the real house:

- AA batteries
- VHS tape
- Canned soup
- Road map
- Lighter
- Milk carton
- House key

The correct items correspond to unfinished tasks in the real world:

- TV remote has dead batteries -> AA batteries
- Unmarked VHS tape -> VHS tape
- Locked front door -> house key

### Puzzle Variant: Receipt Puzzle

The player finds a receipt at the counter:

1. 1 thing to remember
2. 1 thing to watch
3. 1 thing to open

The interpretation:

- Remember = photo
- Watch = VHS tape
- Open = key

The player brings those items to the cashier counter.

### Dream Event

When the correct items are placed on the counter:

- The register opens.
- A bell rings.
- A receipt prints.
- The receipt has the player's home address.
- The gas station door unlocks.
- Outside, the endless road now leads back to the house.

### Symbolic Meaning

The gas station represents leaving, travel, survival, and buying memories.

### Real-World Connection

When the player wakes up:

- The TV remote now works.
- A VHS tape appears beside the TV.
- A map appears on the wall, showing rooms that should not exist.
- A locked drawer can now open.

### House Changes

After this dream:

- A receipt appears on the kitchen counter.
- The fridge contains gas station food.
- The TV remote works.
- A VHS tape can now be played.
- The front door briefly opens to an impossible road instead of outside.

This dream is best for unlocking items instead of unlocking rooms.

### Story Flags

- `GasStationDreamComplete = true`
- `DreamBatteriesCollected = true`
- `DreamVHSCollected = true`
- `DreamKeyCollected = true`
- `HouseDrawerUnlocked = true`

## Suggested Dream Order

1. Endless Bedroom Hallway
2. Infinite Wheat Field With Electrical Poles
3. Hospital Dream
4. Infinite Gas Station

This arc moves from bedroom safety, to the outside world, to avoided truth, to the possibility of leaving.

