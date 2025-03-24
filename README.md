# Documentation of the game 'Chronicles of Justice'

---

## Game Overview
**Description:** An adventure with the protagonist Hera, which has developped the first AI Machine that lets humans visit historic events called 'VRHR'. She is supposed to present her machine to a scientific audience tomorrow and wants to test whether her chosen narratives of historically important women in science works but slowly notices that things are not as they are supposed to be... 

**Genre:** Point-and-Click Adventure

**Engine & Tools Used:** The Development Platform [Unity](https://unity.com/de) and the visual scripting tool [Fungus](https://fungusgames.com)

---

## Development Team
- **Lea Krumbach ([leakassandra](https://github.com/leakassandra))** - Coordination; Asset Creation; Game Development 
- **Lukas Daude ([lde-git](https://github.com/lde-git))** - Generation of Backgrounds & Assets; Game Development
- **Jonathan Voß ([TheJonny272](https://github.com/TheJonny272))** - Generation of Backgrounds & Assets; Sound Design 
- **Sandy Rodrigues** - Story Coordination; Storywriting; Asset Creation
- **Stephan Jäger ([EstebanCazador](https://github.com/EstebanCazador))** - Storywriting; Asset Creation
- **Norma Katrin Wilcken** - Storywriting; Asset Creation
- **Aleksandr Koloskov** - Storywriting; Asset Creation
- **Xiao Yang** - Storywriting; Asset Creation

---

## Installation & Running the Game
**Download & Install:**
1. Clone the repository:
   ```sh
   git clone https://github.com/lde-git/matilda_effect.git
   ```
2. Open with Unity version 6000.0.30f1.
3. Run the game in Unity Editor or build it for your target platform.

**System Requirements:**
- Works with Windows/macOS/Linux
- 8GB RAM

---

## Gameplay Mechanics
**Controls & UI:**
- Mouse Click: Interact with objects
- Mouse Drag and Drop: Minigame Mechanics

**Objectives:**
- The player helps the protagonist Hera find objects in her lab she needs for the presentation of her machine. The player follows her through historical narratives of female scientists, supporting her by solving puzzles and navigating through the time periods.

**Game Progression:**
- The game consits of an prologue, three main chapters and an epilogue which unravles the story.


---


## Art & Audio
**Visuals:**
- All backgrounds and some of the assets were generated with [Flux diffusion models](https://huggingface.co/docs/diffusers/main/api/pipelines/flux)
- Some assets were created manually using [Gimp](https://www.gimp.org) and [Microsoft Word](https://www.microsoft.com/de-de/microsoft-365/word?market=de)
- A small amount of assets were downloaded from [Vecteezy](https://de.vecteezy.com) under 'Free License'


**Music used for creation of our Sound Design:**

- 015828_school-bell-56309 by freesound_community from Pixabay
- 2-horse-carriage-41976 by freesound_community from Pixabay
- 4-min-fan-buzz-sound-247603 by Aman Kumar from Pixabay
- aachen_burning-fireplace-crackling-fire-soundswav-14561 by freesound_community from Pixabay
- ahem-83637 by freesound_community from Pixabay
- airport-ambience-mexico-51907 by freesound_community from Pixabay
- amb_outside_heard_from_inside-cordoba-city-center-ambience-with-music-people-walking-by-and-talking-in-different-languages-286879 by Eugenia Zanarini from Pixabay
- birds-chirping-calm-173695 by Zen from Pixabay
- bubbling-6184 by freesound_community from Pixabay
- cabled-mess-deep-06-low-machine-ambience-paulstretched-24595 by freesound_community from Pixabay
- camden-market-52710 by freesound_community from Pixabay
- church-bell-273815 by Traian Mitroi from Pixabay
- clearing-throat-103594 by freesound_community from Pixabay
- clear-throat-85636 by freesound_community from Pixabay
- clock-ticking-53528 by freesound_community from Pixabay
- crowd-talking-138493 by Björn Eichenauer from Pixabay
- dark-synth-165100 by modemirroir from Pixabay
- elevator-29654 by freesound_community from Pixabay
- Elevator Music by aeiouFU, YouTube: https://www.youtube.com/watch?v=jj0ChLVTpaA
- horse-clip-clopping-downhill-stereo-33768 by freesound_community from Pixabay
- horse-snort-95874 by freesound_community from Pixabay
- loud-thud-45719 by freesound_community from Pixabay
- low-hum-14645 by freesound_community from Pixabay
- main-door-opening-closing-38280 by freesound_community from Pixabay
- mechanical-hum-64405 by freesound_community from Pixabay
- mixer-94757 by freesound_community from Pixabaychirping-birds-ambience-217410 by Alex from Pixabay
- outdoor-ambience-31443 by freesound_community from Pixabay
- outdoor-white-noise-76812 by freesound_community from Pixabay
- pc-speaker-error-beep-104100 by freesound_community from Pixabay
- power-supply-buzz-77675 by freesound_community from Pixabay
- pushing-chair-96107 by freesound_community from Pixabay
- retro-space-sounds-8-176211 by Roland Horvers from Pixabay
- Retro Seeburg 1000 Elevator Music Volume 1 oiginal by Fardemark, YouTube: https://www.youtube.com/watch?v=RjSAiqOhOuQ
room-tone-with-computer-noise-33598 by freesound_community from Pixabay
- sci-fi-auto-sliding-door-45028 by freesound_community from Pixabay
- shopping-mall-ambience-24056 by freesound_community from Pixabay
- sliding-chair-47711 by freesound_community from Pixabay
- sneaking-on-wooden-floor-20306 by freesound_community from Pixabay
- throat-clear-2-85212 by freesound_community from Pixabay
- throat-clear-85527 by freesound_community from Pixabay
- water-bubbles-257594 by LIECIO from Pixabay
- water-bubbles-306915 by Jurij from Pixabay

---

## Technical Documentation
**Project Structure:**
```
/matilda_effect/
├── Assets/
│   ├── Audio/
│   ├── Fungus/
│   ├── FungusExamples/
│   ├── Resources/
│   ├── Scenes/
│   ├── Screens/
│   ├── Scripts/
│   ├── Settings/
│   ├── Shader/
│   ├── Sprites/
│   ├── TextMesh Pro/
│   ├── Ultimate 10 Plus Shaders/
│   ├── Video/
```

**Directory Information:**
- [Audio](https://github.com/lde-git/matilda_effect/tree/main/Assets/Audio): Contains all ambiance & general sounds used in the game
- [Fungus](https://github.com/lde-git/matilda_effect/tree/main/Assets/Fungus): Package required for using Fungus funcitonalities. Contains Fungus scripts. 
- [FungusExamples](https://github.com/lde-git/matilda_effect/tree/main/Assets/FungusExamples): Contains examples provided by the Fungus team.
- [Resources](https://github.com/lde-git/matilda_effect/tree/main/Assets/Resources): LUKAS
- [Scenes](https://github.com/lde-git/matilda_effect/tree/main/Assets/Scenes): Contains scenes that were created (we worked with only one scene). 
- [Screens](https://github.com/lde-git/matilda_effect/tree/main/Assets/Screens): Contains all background screens used in the game, ordered by chapters.
- [Scripts](https://github.com/lde-git/matilda_effect/tree/main/Assets/Scripts): Contains all self-written scripts for the Elevator logic & all of the five minigames.
- [Settings](https://github.com/lde-git/matilda_effect/tree/main/Assets/Settings): Settings automatically created and saved by Unity.
- [Shader](https://github.com/lde-git/matilda_effect/tree/main/Assets/Shader): Shader necessary for Elevator keypad visualization.
- [Sprites](https://github.com/lde-git/matilda_effect/tree/main/Assets/Sprites): Contains all assets used in the game, ordered by chapters.
- [TextMesh Pro](https://github.com/lde-git/matilda_effect/tree/main/Assets/TextMesh%20Pro): Package required for using UI Text.
- [Ultimate 10 Plus Shaders](https://github.com/lde-git/matilda_effect/tree/main/Assets/Ultimate%2010%20Plus%20Shaders): Shader necessary for Elevator keypad visualization.
- [Video](https://github.com/lde-git/matilda_effect/tree/main/Assets/Video): Contains used animations and video settings.

---

## Sprint Boards & Development Process
**Workflow Methodology:** Inpireded by SCRUM methodology & adapted for student project (visual plannings, weekly meetings, 2-week sprints etc.)

**Our Sprint Boards (created with [draw.io](https://app.diagrams.net)):**
- *[Project Backlog/Sprint](https://app.diagrams.net/#G1ka_AdyGGGaCoVKHTlgk0S-ojFGPTZ1_l#%7B%22pageId%22%3A%22wHack5X73N4nbYw141FW%22%7D)*: Shows a sketch we created at the beginning for how we want to structure our workflow (usable for each chapter) and a rough overview of our goals given a project backlog.
- *[Game Structure](https://app.diagrams.net/#G1ka_AdyGGGaCoVKHTlgk0S-ojFGPTZ1_l#%7B%22pageId%22%3A%220lTyS2NvCxlNhSA-qQsg%22%7D)*: Overview of the whole game structure.
- *[Story-Outline](https://app.diagrams.net/#G1ka_AdyGGGaCoVKHTlgk0S-ojFGPTZ1_l#%7B%22pageId%22%3A%22XRSnL2BsXbv-ScXAFCd3%22%7D)*: Contains a link to the [final story](https://docs.google.com/document/d/1uzjjJumECvRZHNOlzjzor7qDWGXV5WPmx4azDBTvCHI/edit?pli=1&tab=t.0) and visual concept + intem/background concepts for each chapter created by the Story group. The most important board for our collaborative work.
- *[Illustrations](https://app.diagrams.net/#G1ka_AdyGGGaCoVKHTlgk0S-ojFGPTZ1_l#%7B%22pageId%22%3A%2229w31AM4T58Htvt0CcKr%22%7D)*: Overview of all backgrounds and assets that needed to be created per chapter. Useful for the people that generated pictures with the Flux model.
- *[Unity Game Dev](https://app.diagrams.net/#G1ka_AdyGGGaCoVKHTlgk0S-ojFGPTZ1_l#%7B%22pageId%22%3A%225Bpciuma2Zk5voYzmIbd%22%7D)*: Basic steps for the Unity Development group that needed to be implemented per chapter.
---

## Known Issues & Future Improvements
- At the moment, the game cannot be saved & needs to be started from beginning when interrupted
- As to our knowledge, exporting a iOS stand-alone without problems is not possible 
- Due to limited time, we were only able to fix bugs given a single bug report. Some interactions that we did not test may make the game crash

---

## License & Contribution Guidelines
**License:** This game is licensed under the [Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International (CC BY-NC-ND 4.0)](https://creativecommons.org/licenses/by-nc-nd/4.0/).  
You may download, share, and play the game **for private, non-commercial use only**.  
You **may not modify** or distribute it for public or commercial purposes.

---
