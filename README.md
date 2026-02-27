# Grid Lab

Grid Lab is a Unity-based card matching game project. The objective is to match pairs of cards on a grid, progressing through levels and tracking scores. The project is structured for modularity and scalability, supporting features like user progress tracking, sound effects, UI screens, and card shuffling.

## Features
- Card matching gameplay with score tracking
- Level progression and user progress management with total of 5 levels
- UI screens for results and gameplay
- Sound effects for matched/mismatched pairs
- Card shuffling and grid management

## Scoring Logic & Combo System
- Card matched: +10 points
- Card mismatched: -2 points penalty
- Combo: If you match cards consecutively (2 streak), your score for the next match is doubled (+20)

## Project Structure
- `Assets/Art/`: Game art assets
- `Assets/Prefabs/`: Prefab objects for cards and UI
- `Assets/Resources/`: Game resources
- `Assets/Scenes/`: Unity scenes
- `Assets/ScriptableObjects/`: Scriptable objects for configuration
- `Assets/Scripts/`: Core game scripts
    - `Core/`: Main game logic (GameManager, GridManager, etc.)
    - `UI/`: UI management and factories
    - `Sound/`: Sound management
    - `Card/`: Card logic and state
- `Library/`, `Logs/`, `obj/`, `Temp/`: Unity-generated folders for build, cache, and logs
- `ProjectSettings/`: Unity project settings
- `Packages/`: Unity package manager files

## Key Scripts
- `GameManager.cs`: Handles game flow, score, level progression, and event responses
- `GridManager.cs`: Manages card grid, shuffling, and card state
- `Card.cs`: Represents individual card logic and state
- `UIFactory.cs`: Manages UI screens
- `SoundManager.cs`: Handles sound effects
- `UserProgressTracker.cs`: Tracks player progress and levels

## How to Run
1. Open the project in Unity Editor.
2. Load the main scene from `Assets/Scenes/`.
3. Press Play to start the game.

## Customization
- Add new card art in `Assets/Art/`
- Modify card prefabs in `Assets/Prefabs/`
- Adjust game logic in `Assets/Scripts/Core/`
- Configure levels and grid data in `Assets/ScriptableObjects/`