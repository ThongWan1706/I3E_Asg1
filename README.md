# I3E_Asg1
**Author:** Lam Thong Wan

**Date:** 11 June 2026 

**Description:** This document will include informations regards on the project such as the gameplay, limitation & bugs faced in this project, settings required for the game, references used for the project and AI usage documentations

# Gameplay
The game consist of 3 levels which requires the player collect a card and coins along the way:
- 1st level being the easiest by finding the card around the area in order to open the door
- 2nd level being slightly difficult by jumping on obstacles to avoid falling into the lava that would lose hp, while finding the card around the area in order to open the door and get to the 3rd level
- 3rd being the hardest by having a maze that consist of traps:
  - Trap doors that once the player opens it, after a few seconds the door can slam the player into a one shot hazard that can cause instant death.
  - Smoke hazards around the room that can cause player to lose hp if they were to stay in the area for too long
  - During the process, the player were to find each room (while avoding the rooms that have trap doors and one shot hazards) while finding the final card to open the door and go to the Goal Area.

Key controls consist of:
- Shift key: Sprint (Recommended to use it while moving around the map)
- WASD key: Move around
- F key: Interaction with items around the map (Cards, doors and cardscanner)
- Mouse: Look around the surrounding

Answer key:
- The card locations and route are being showcased in the Card location folder (https://github.com/ThongWan1706/I3E_Asg1/tree/main/Card%20locations)

# Limitations & Bugs
### Limitations
- At the MVP panel and the Game Over Panel, the "Quit Game" button won't work as it only works when its in the exe file
- If you did accidentally get into the one shot trap that can cause instant death and restart the game, it'll restart back at the very beginning which requires the player to play the whole process again
  
- ### Bugs
- The trap door although work normally by letting the player open and after a few seconds it slams shut, it still unable to push the player down into the one shot hazard despite having colliders onto the door child that provide contact to the player
- At lvl 2 sometimes even though you landed on the steps, it would still cause lose of hp

# Platforms, hardware & desired settings required for the game
### Platforms
- Prefer in Windows PC

### Hardware
- Operating System: Windows 10/11
- Processor (CPU): minimum Intel Core i5 / AMD Ryzen 5
- Memory (RAM): 8 GB RAM
- Graphics (GPU): minimum NVIDIA GeForce GTX 1050 / AMD Radeon RX 560 
- Storage: Around 500 MB available space

### Developer & Build Settings (For Grading/Review)
If you are opening this project inside the Unity Editor, use these settings:
- Unity Editor Version: Unity 6000.3.13f1
- Render Pipeline: Universal Render Pipeline (URP)

### Desired In-Game Settings (For Optimal Playing Experience)
- Resolution: 1920 × 1080 (16:9 Aspect Ratio, Fullscreen)
- Target Frame Rate: 60 FPS (V-Sync Enabled)
- Graphics Quality Preset: Medium / High

# References used for the project
### Sound
- BGM: https://pixabay.com/music/electronic-crime-documentary-forensic-lab-background-cold-analytical-underscore-464812/
- Coin received: https://pixabay.com/sound-effects/film-special-effects-coin-recieved-230517/
- Open & Close Door: https://pixabay.com/sound-effects/film-special-effects-heavy-door-unlocking-515258/
- Lava: https://pixabay.com/sound-effects/nature-lava-loop-2-67306/

### Assets:
- Coins: https://assetstore.unity.com/packages/3d/props/stylized-coin-pack-15-coins-gold-silver-bronze-wood-340494
- Cards: https://assetstore.unity.com/packages/p/retro-psx-horror-puzzle-item-pack-icon-lowpoly-250188
- Probuilder in Unity

### Video:
- A few moments later clip (Used in editing the gameplay video showcase): https://www.youtube.com/watch?v=ViPAmJRR8H0

# AI Usage Documentations

The AI usage screenshots are being showcased in the AI Documentation folder (https://github.com/ThongWan1706/I3E_Asg1/tree/main/AI%20Documentations)

They are used in the following components in this project for references:
- Checkpoint
- Dying animation
- Game Over Screen Canvas
- Instant Death logic
- Smoke hazard logic

