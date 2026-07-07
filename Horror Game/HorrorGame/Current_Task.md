# Current Task  
  
Create a simple menu system for the Unity horror game.  
  
## Goal  
  
Add a basic main menu and pause menu.  
  
The menu system should be simple, beginner-friendly, and easy to expand later.  
  
---  
  
## Main Menu  
  
Create a main menu scene.  
  
The main menu should have three buttons:  
  
1. **Play**  
- Starts the game.  
- Loads the first gameplay scene.  
  
2. **Settings**  
- Opens a simple settings panel.  
- For now, the settings panel can be basic.  
- Include a back button to return to the main menu.  
  
3. **Exit Game**  
- Quits the application.  
- Also print a debug message in the Unity Console because quitting does not work the same way inside the Unity Editor.  
  
---  
  
## Pause Menu  
  
Create a pause menu that appears during gameplay when the player presses `Escape`.  
  
The pause menu should have three buttons:  
  
1. **Resume**  
- Closes the pause menu.  
- Unpauses the game.  
- Locks and hides the cursor again.  
  
2. **Settings**  
- Opens a simple settings panel.  
- Include a back button to return to the pause menu.  
  
3. **Exit to Main Menu**  
- Unpauses the game.  
- Loads the main menu scene.  
  
---  
  
## Technical Requirements  
  
- Use Unity UI.  
- Use C# scripts.  
- Keep the scripts simple and commented.  
- Use the old Unity Input Manager.  
- Use `Escape` to toggle the pause menu.  
- Use `Time.timeScale = 0` when paused.  
- Use `Time.timeScale = 1` when resumed.  
- Show the cursor when menus are open.  
- Hide and lock the cursor during gameplay.  
- Put scripts in `Assets/Scripts`.  
- Put scenes in `Assets/Scenes`.  
  
---  
  
## Scenes Needed  
  
Create or use these scenes:  
  
1. `MainMenu`  
2. First gameplay scene, such as `HouseScene`  
  
Make sure both scenes are added to the Unity Build Settings.  
  
---  
  
## Scripts Needed  
  
Create simple scripts such as:  
  
1. `MainMenuController.cs`  
- Handles Play, Settings, Back, and Exit Game.  
  
2. `PauseMenuController.cs`  
- Handles pausing, resuming, settings, and returning to the main menu.  
  
---  
  
## Done Means  
  
This task is complete when:  
  
- The game starts at the main menu.  
- The Play button loads the gameplay scene.  
- The Settings button opens a settings panel.  
- The Exit Game button quits the application or logs a quit message in the Unity Editor.  
- Pressing `Escape` during gameplay opens the pause menu.  
- Resume returns to gameplay.  
- Settings opens from the pause menu.  
- Exit to Main Menu returns to the main menu.  
- Cursor behavior works correctly.  
- The project has no Console errors.  
- Update `HorrorGame/Progress.md` when finished.