STATUS (2026-07-09): Core rework DONE - see Progress.md.
Delivered: grounded weighted movement + smoothed mouse look (SimpleFirstPersonPlayer, applies to all
scenes via a runtime camera pitch pivot), velocity-driven head bob / strafe roll / turn lag / idle
breathing with 0-1 accessibility strengths (CameraEffects, on all five scene cameras), camera lens
(1.65m eye height, FOV 68, near clip 0.04, SMAA), analog URP post on the house (grain, vignette,
chromatic aberration, soft bloom, faint lens distortion, muted cool grade, Neutral tonemapping), and a
one-slider AnalogFilterController to scale/disable the analog look. Verified in Play Mode, no console errors.

REMAINING FOLLOW-UP CHUNKS (next work):
1. Footsteps + physical presence: per-surface footstep audio (carpet/wood/vinyl/tile/concrete/grass/wet),
   pitch/volume variation, triggered from the head-bob step cycle; quiet clothing/weight-settle sounds.
2. Settings menu wiring: expose FOV, X/Y sensitivity, invert-Y, head-bob/sway/breathing strength,
   analog strength, film grain, chromatic aberration, motion blur, screen shake, reticle, sprint
   toggle/hold, and raw/smoothed mouse in the existing Settings panel, saving with PlayerPrefs.
3. Visual presets: Low / Medium / High / Analog quality presets.
4. Per-dream authored camera responses: gentle wind sway (wheat field), unnaturally stable/clinical
   (hospital), stronger analog noise + wet bloom (gas station), subtle 2-4 degree FOV drift in the
   final House After Sleep, plus optional near-invisible hallway lens distortion. All gradual/reversible.
5. Optional: simple corner-lean (Q/E) if it stays simple and never pushes the controller through walls.

------------------------------------------------------------------------
ORIGINAL BRIEF (reference):

Rework the first-person camera, player movement, and post-processing for my Unity psychological horror game. The desired feeling is inspired by the grounded, lo-fi analog atmosphere of Fears to Fathom: intimate first-person exploration, slightly imperfect camera movement, retro digital/VHS presentation, realistic darkness, and the uncomfortable feeling that the player is physically standing inside the house.

Do not directly copy assets, shaders, UI, or exact effects from Fears to Fathom. Capture the broader visual language and player feeling while preserving this game’s own 1990s house-and-dream identity.

IMPORTANT PROJECT REQUIREMENTS

- This is a Unity URP project.
- Preserve the existing CharacterController-based movement.
- Preserve mouse-look, interaction raycasts, interaction distance, pause behavior, objectives, doors, story events, and all existing gameplay.
- The interaction ray must continue to originate from the player camera.
- Keep all new scripts simple, commented, beginner-friendly, and exposed in the Inspector.
- Do not use Cinemachine unless it provides a clear benefit.
- Do not make the camera floaty, cinematic, drunk, or uncomfortable.
- The player should feel vulnerable and human, not like a fast FPS character.
- Camera effects must never interfere with puzzle readability.

CAMERA POSITION AND LENS

Make the camera feel like a person walking through a cramped residential house:

- Camera eye height: approximately 1.62–1.68 metres.
- Default vertical field of view: approximately 68 degrees.
- Provide an adjustable FOV range of 60–80.
- Use a near clipping plane around 0.03–0.05 so walls and doors do not disappear when approached.
- Do not use an extreme wide-angle or fisheye effect.
- The house should feel enclosed because of its architecture and movement speed, not because the FOV is artificially narrow.
- Use normal perspective projection.
- Keep the horizon level while standing still.
- Avoid permanent Dutch angles.
- Do not allow the camera to clip through walls, doors, furniture, or the ceiling.

MOUSE LOOK

Replace raw, mechanically sharp mouse movement with responsive but subtly weighted looking:

- Keep mouse input immediate enough to feel controllable.
- Add very light smoothing, approximately 0.03–0.06 seconds.
- Do not create noticeable input lag.
- Separate horizontal and vertical sensitivity controls.
- Add an invert-Y option.
- Clamp vertical look to approximately -80 and +80 degrees.
- Support sensitivity adjustment in the settings menu.
- Allow raw input as an accessibility option.
- Do not add automatic camera movement while the player is standing still, except for extremely subtle breathing.

Looking around should feel deliberate and observant. The player should naturally examine doorways, dark corners, photographs, windows, and objects without fighting the camera.

PLAYER MOVEMENT FEEL

Make movement slower and more physical than a typical first-person action game:

- Walking speed: approximately 2.4–2.8 metres per second.
- Optional sprint speed: approximately 4.0–4.5 metres per second.
- Sprint should feel like anxious fast walking rather than athletic running.
- Add smooth acceleration over approximately 0.12–0.20 seconds.
- Add slightly quicker but still smooth deceleration.
- Preserve precise movement around furniture and interactable objects.
- Reduce or remove slippery diagonal movement.
- Normalize diagonal input so it is not faster.
- Use believable gravity and grounded behavior.
- The player should not jump unless jumping is required by the game.
- Do not add crouching unless it has a real gameplay purpose.
- Add a small amount of inertia without making controls frustrating.
- Allow the player to stop accurately beside doors, tables, and clues.

Movement should make crossing the house feel meaningful. The player should never glide silently like a spectator camera.

HEAD BOB

Add restrained procedural head movement connected to actual player velocity:

Walking:

- Vertical amplitude around 0.018–0.028 metres.
- Horizontal amplitude around 0.012–0.020 metres.
- Frequency around 1.6–1.9 steps per second.
- Use a soft sine-based motion.
- Include tiny rotation, no more than approximately 0.15–0.30 degrees.

Sprinting:

- Increase frequency and amplitude only slightly.
- Do not create aggressive bouncing.
- Add a small increase in forward momentum and breathing intensity.

Stopping:

- Smoothly return the camera to its neutral position.
- Never snap the camera back to centre.
- Do not continue bobbing when pushing into a wall.
- Base the effect on real CharacterController velocity, not input alone.

Provide an accessibility slider from 0% to 100%, including the ability to disable head bob completely.

STEP SWAY AND TURNING

Add subtle physical imperfections:

- Very small lateral camera sway while walking.
- A tiny amount of roll when strafing, approximately 0.2–0.4 degrees maximum.
- A tiny amount of camera lag during sharp turns.
- Smoothly catch up within a fraction of a second.
- Do not make the camera feel detached from the body.
- Do not apply strong roll during ordinary mouse movement.
- Remove all sway while paused or during menus.

The effect should usually be felt subconsciously rather than visibly noticed.

BREATHING AND IDLE MOTION

When standing still:

- Add extremely subtle breathing movement.
- Vertical displacement should remain below approximately 0.004 metres.
- Rotation should remain below approximately 0.05 degrees.
- Use a slow, irregular rhythm rather than an obvious looping sine wave.
- Reduce or disable breathing while the player is reading an important object.
- Increase breathing slightly during selected dream or final-act moments only when controlled by story state.
- Never use breathing motion to simulate fear automatically without narrative cause.

FOOTSTEPS AND PHYSICAL PRESENCE

Connect movement to sound so the camera feels grounded:

- Trigger footsteps from travelled distance or the head-bob step cycle.
- Use different sounds for carpet, wood, vinyl, bathroom tile, concrete, grass, and wet pavement.
- Add slight pitch and volume variation.
- Footsteps should be quieter on bedroom carpet and sharper in the bathroom or fuse room.
- Add subtle clothing movement during faster movement.
- Add quiet landing or weight-settling sounds when descending small steps.
- Interior footsteps should have room-appropriate reverb.
- Do not play footsteps while the player is stationary or pushing against a collider.

INTERACTION FEEL

Preserve the existing E-to-interact system while making it more natural:

- Keep interaction raycasts tied to the centre of the camera.
- Use an interaction range around 2.5–3 metres.
- Add a small, minimal centre reticle only when looking at something interactable.
- Avoid a permanent bright crosshair.
- Make interaction prompts small, clean, and slightly imperfect in keeping with the analog presentation.
- Optionally add a very subtle camera focus or hand-reaching suggestion when operating doors, switches, tapes, phones, and drawers.
- Never pull control away from the player for ordinary interactions.
- Do not add strong zooming every time the player interacts.
- Important readable documents may use a controlled inspection view with head bob temporarily disabled.

DOORWAYS AND CORNERS

The camera should make doorways feel tense:

- Keep the player close enough to doors that opening one reveals the room gradually.
- Do not automatically centre the camera on doors.
- Allow the player to lean visually around corners only if it can be implemented simply and does not complicate controls.
- If leaning is included, use a very small offset and roll with Q and E or an optional control.
- Leaning must not move the CharacterController through walls.
- It should be optional and may be omitted if it weakens simplicity.

CAMERA RESPONSE TO STORY EVENTS

Keep the normal camera restrained so unusual moments have more impact:

- Do not use constant screen shake.
- Electrical flickers may cause an almost imperceptible exposure response, not physical camera shaking.
- Loud dream events may use a single soft camera impulse.
- The endless hallway can introduce slightly heavier turn smoothing or nearly invisible lens distortion.
- The wheat-field dream can use gentle wind-driven camera movement.
- The hospital dream can feel unnaturally stable and clinical.
- The gas-station dream can use stronger analog noise and wet-light bloom.
- The final House After Sleep may subtly change FOV by 2–4 degrees during impossible transitions.
- These changes must be gradual and reversible.
- Never use effects that reveal puzzle solutions or disorient the player unfairly.

ANALOG VISUAL PRESENTATION

Create a restrained lo-fi 1990s recorded-video appearance using URP post-processing and, if necessary, a lightweight custom full-screen effect.

Use:

- Slightly reduced internal rendering resolution or subtle pixel structure.
- Mild film grain.
- Very faint chromatic aberration near the edges.
- Subtle colour bleeding.
- Slightly crushed blacks without removing important shadow detail.
- Soft highlight bloom around lamps, television screens, streetlights, and reflective wet surfaces.
- Mild lens distortion, close to invisible.
- Very subtle vignette.
- Slight temporal instability or analog noise.
- Occasional faint horizontal interference associated with supernatural events.
- Restrained sharpening or softness that resembles consumer video rather than modern cinematic footage.
- Cool blue-grey shadows and warm amber practical lights.
- Muted saturation with selected reds, greens, and warm lamps remaining noticeable.

Suggested starting strengths:

- Film grain: 0.15–0.25.
- Vignette: 0.12–0.20 with soft edges.
- Chromatic aberration: 0.02–0.06.
- Bloom: low intensity with a relatively high threshold.
- Lens distortion: extremely subtle, approximately -0.02 to -0.05.
- Saturation: approximately -8 to -15.
- Contrast: approximately +5 to +12.

These are starting points, not mandatory final values. Judge the result in motion.

Do not use:

- Heavy VHS tracking lines at all times.
- Constant glitching.
- Excessive chromatic aberration.
- Strong fisheye distortion.
- Extreme pixelation that makes text unreadable.
- Excessive bloom.
- Pure black shadows.
- Strong motion blur.
- Aggressive depth of field during ordinary exploration.
- Fake dust and scratches covering the entire screen.
- A bright modern horror-game crosshair.

IMAGE QUALITY AND RESOLUTION

The result should feel intentionally lo-fi, not simply low quality:

- Preserve clean silhouettes and readable interactable objects.
- Use modern lighting and materials beneath the retro treatment.
- Allow the analog filter to be disabled.
- Provide Low, Medium, High, and Analog visual presets.
- Keep UI text rendered clearly after the analog effect if possible.
- Do not distort pause menus or accessibility menus.
- Avoid unstable anti-aliasing shimmer.
- Test at 1080p and common aspect ratios.
- Keep the horror presentation effective at both high and low graphics settings.

MOTION BLUR AND DEPTH OF FIELD

- Disable motion blur by default.
- If used, apply only an extremely low camera-motion blur value.
- Never smear the screen during mouse movement.
- Keep gameplay depth of field disabled or extremely subtle.
- Use depth of field only for controlled document inspection or selected story sequences.
- Provide toggles for both effects.

EXPOSURE

Use controlled exposure rather than aggressive automatic adaptation:

- Prevent the image from pumping brighter and darker whenever the player turns.
- Interior exposure should keep lamps warm while preserving details in shadowed doorways.
- Windows should be somewhat brighter than the room but not pure white rectangles.
- Moving between the house and outside may use slow, subtle adaptation.
- Dream-state exposure changes should be authored rather than random.

ACCESSIBILITY

Provide settings for:

- FOV.
- Mouse sensitivity.
- Invert Y.
- Head-bob strength.
- Camera sway strength.
- Analog/VHS filter strength.
- Film grain.
- Chromatic aberration.
- Motion blur.
- Screen shake.
- Reticle visibility.
- Sprint toggle or hold.
- Raw or smoothed mouse input.

Disabling camera motion effects must not affect player speed, footsteps, interactions, or story progression.

TARGET PLAYER EXPERIENCE

The completed camera should make the player feel physically present inside a quiet house late at night. Movement should be cautious and slightly heavy. Looking around should be smooth, responsive, and intimate. The camera should reveal rooms gradually and make empty doorways uncomfortable without relying on jumpscares.

The analog graphics should suggest an old memory, home video, or reconstructed event. The player should occasionally wonder whether they are controlling a person, watching a recording, or remembering something incorrectly.

The final feeling should be:

- Grounded.
- Intimate.
- Vulnerable.
- Lo-fi but visually deliberate.
- Smooth enough to play comfortably.
- Slightly imperfect.
- Quietly unsettling.
- Similar in atmosphere to Fears to Fathom, while remaining visually and mechanically distinct to this project.