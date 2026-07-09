
DONE (2026-07-09) - Chunk 1, Footsteps + physical presence: see Progress.md. Per-surface procedural
footstep audio (carpet/wood/vinyl/tile/concrete/grass/wet), generated in code with pitch/volume
variation, triggered by real distance travelled, room-appropriate reverb, plus a landing/settle sound.
Surfaces tagged across the house; dream scenes use a sensible default each. Verified in Play Mode.

DONE (2026-07-09) - Chunks 2-5, all remaining follow-ups: see Progress.md.
2. Settings system (GameSettings + PlayerPrefs) and an IMGUI settings overlay opened by the existing
   Main Menu / Pause "Settings" buttons: FOV, X/Y sensitivity, invert-Y, raw mouse, head-bob/sway/
   breathing strength, screen shake, analog strength, film grain, chromatic aberration, motion blur,
   reticle, sprint hold/toggle, plus Reset to defaults. Applies live and persists.
3. Visual presets Low/Medium/High/Analog (buttons in the settings menu), persisted.
4. Per-dream camera moods (DreamCameraMood): wheat-field wind, hospital stillness, gas-station analog +
   wet bloom, hallway heavier turn + faint lens bend, and a flag-gated 3-degree FOV drift in the final
   act. All gradual, reversible, and respect the player's settings.
5. Optional corner lean (CameraLean): camera-only, wall-clamped, OFF by default because E is the
   interact key (configurable keys in the Inspector).

Also fixed a latent bug: the house analog profile's chromatic aberration / lens distortion / tonemapping
had not been persisting; re-added as proper asset sub-assets. See Bugs.md.

THE FEATURE ROADMAP FROM THE ORIGINAL CAMERA/MOVEMENT/POST BRIEF IS NOW COMPLETE.
Possible future polish (not requested): richer clothing/gear audio layer, real Low/High render-scale
differences if more URP quality levels are added, and hand/arm reach visuals on interaction.
------------------------------------------------------------------------
