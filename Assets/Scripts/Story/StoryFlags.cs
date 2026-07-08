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
