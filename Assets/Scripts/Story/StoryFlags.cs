public static class StoryFlags
{
    // Common story flag names live here so scripts do not need to type raw strings.
    public const string WatchedTV = "WatchedTV";
    public const string AteFood = "AteFood";
    public const string LockedDoor = "LockedDoor";
    public const string WentToBed = "WentToBed";
    public const string DreamOneComplete = "DreamOneComplete";
    public const string HallwayPuzzleSolved = "HallwayPuzzleSolved";
    public const string DreamHallwayComplete = "DreamHallwayComplete";

    // First wake-up consequences after the hallway dream.
    public const string BedroomMemoryChanged = "BedroomMemoryChanged";
    public const string AnsweringMachineMessageUnlocked = "AnsweringMachineMessageUnlocked";
    public const string ClosetUnlockedAfterHallway = "ClosetUnlockedAfterHallway";
    public const string BathroomMirrorCovered = "BathroomMirrorCovered";
    public const string HouseClocksImpossibleTime = "HouseClocksImpossibleTime";

    // Wheat field dream.
    public const string WheatFieldPowerRestored = "WheatFieldPowerRestored";

    // Power consequences in the house after the wheat field.
    public const string HousePowerChanged = "HousePowerChanged";
    public const string TVCanTurnOnByItself = "TVCanTurnOnByItself";
    public const string LockedBasementLightEnabled = "LockedBasementLightEnabled";
    public const string WallBuzzingEnabled = "WallBuzzingEnabled";
    public const string RadioStrangeAudioUnlocked = "RadioStrangeAudioUnlocked";

    // Day 3 morning-after tasks.
    public const string CheckedFuseBox = "CheckedFuseBox";
    public const string SawTVStatic = "SawTVStatic";
    public const string FeltWallBuzzing = "FeltWallBuzzing";
    public const string ListenedToRadio = "ListenedToRadio";

    // Day 2 morning-after tasks.
    public const string HeardNewMessage = "HeardNewMessage";
    public const string SawChangedPhoto = "SawChangedPhoto";
    public const string SawCoveredMirror = "SawCoveredMirror";
    public const string CheckedFridge = "CheckedFridge";

    // Future unlocks used by locked doors and drawers in the house.
    public const string FuseRoomUnlocked = "FuseRoomUnlocked";
    public const string HouseDrawerUnlocked = "HouseDrawerUnlocked";
    public const string FrontDoorUnlocked = "FrontDoorUnlocked";
    public const string HouseChangedAfterDreamOne = "HouseChangedAfterDreamOne";
    public const string FoundDreamObject = "FoundDreamObject";
}
