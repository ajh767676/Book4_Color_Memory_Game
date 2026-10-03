# Color Memory Game

A Unity color memory game that saves completed game results to a local MySQL database.

## Requirements

- Unity 6
- MAMP
- Apache and MySQL running

## Database Setup

1. Start Apache and MySQL in MAMP.
2. Open phpMyAdmin.
3. Import `ServerFiles/memory.sql`.
4. The database name is `memory`.
5. The MySQL username is `root`.
6. The MySQL password is `root`.
7. Copy `saveMemory.php` and `getMemories.php` from `ServerFiles` into `C:\MAMP\htdocs`.

## Running the Game

1. Open the project in Unity.
2. Open `Assets/Scenes/intro`.
3. Press Play.
4. Enter a player name.
5. Select the number of colors and the time limit.
6. Repeat each displayed color sequence.
7. Successfully completing five rounds saves the result to the database.

Press ESC during the game to pause or resume.

## Scenes

- intro
- preferences
- chapter2
- chapter2_lose
- exit