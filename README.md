# Monster Survivors

<img width="1920" height="1080" alt="Thumbnail_Monster_Survivors" src="https://github.com/user-attachments/assets/76e5e302-8afc-4a4e-a135-2a8daec14b68" />

## Overview

Monster Survivors is casual rogue-like survival shoot'em up game where you must waves of monsters that keep on spawning more & more to take you down. Choose your weapons & upgrade them to become stronger to fight the waves of enemies and survive as long as you can!
The project uses design patterns like Service Locator, MVC, Object Pooling & Observer Pattern.

---

## Features

- Roguelike Mechanics: Engage in action-packed shoot'em up with different characters with various types of weapons & health upgrades.
- Three Playable Characters :
  1. Ash - Medium Health & Speed,
  2. Brock - Low Speed & Large Health &
  3. Misty - High Speed & Small Health
- Three Weapons:
1. Radial Reap - Area weapon that continuously grows & shrinks to deal damage at intervals based on the cooldown  
2. Orbital Fury - Spawns balls around the player that revolves around the player to deal damage at intervals based on the cooldown  
3. Scatter Shot - Shoot projectiles in different directions at intervals based on the cooldown

- Power-ups/Upgrade System: Collect power-ups that increase your strength of your weapon & health to survive longer.
  
- Dynamic Waves: Enemies spawn in waves, with each subsequent wave being more challenging.

- Enemy AI: A variety of enemies with different behaviors and attack patterns:
            1. Bomber - The Bomber enemy is fast & low health enemy that chases the player & explodes when comes into contact.
            2. Stalker - The Stalker is slower enemy with a bit more health & it keeps pursuing the player to deal continous damage when comes in contact.
            3. Shooter - The Shooter enemy uses ranged attacks by shooting projectiles periodically in 4 different directions from a distance. It has slow speed but it still follows player.
            4. Boss - Boss enemies are tankier and significantly more powerful than regular enemies. They are a bit fast who can pursue player & also periodically shoots projectiles around in 8 directions

- High Score System: Track your highest score for replayability across different levels.

- UI Elements: Displays health, score, and other important information across different UI.

- Visual Effects: Explosions, damage numbers, smooth player, weapons, & enemy animations to enhance the combat experience.
     
---

## Implementation Details

### Design Patterns

**Singleton Pattern**: Used for managing global systems like the game's state (e.g., score tracking) and audio management. This ensures there's only one instance handling these operations across the game.

**Factory Pattern**: Applied in the creation of game objects like enemies and weapons. This allows for flexible extension, as new enemy types or weapon variants can be added without altering existing code.

**Observer Pattern**: Used for event-driven interactions, such as health updates, enemy deaths, and score changes. Components can "subscribe" to events (like the player's health changing) and automatically update themselves when these events occur.

**Object Pooling**: Optimized performance by reusing objects like projectiles, enemies, and vfx animations instead of frequently instantiating and destroying them. This reduces the performance hit from memory allocations and garbage collection, especially during intense gameplay moments.

**Service Locator Pattern**: Centralized management of game services like player, enemy, wave, weapon, audio, UI updates. The Service Locator helps decouple the game components, making it easier to manage services and dependencies across different systems.

### Scriptable Objects

Weapon Data: Scriptable Objects store weapon attributes such as prefab, attack power, radius, life time, cycle time etc. along with respecitve power up scriptable object that contains correspondin information. This allows for easy customization and modification of weapon properties directly from the Unity Editor without needing to modify code.

Enemy Data: Contains attributes for different types of enemies, such as health, attack patterns, and movement behavior. Using Scriptable Objects for enemies makes it easier to balance and tweak enemy behaviors without touching the core logic.

Power-up Data: Defines power-ups and their effects (e.g., increasing health or damage). Power-up objects are stored as Scriptable Objects, allowing for easy addition of new power-ups by simply creating new instances in the Unity editor.

Audio Data: Contains attributes for different audio types & their respective sound clips.

Exp To Level Up Data: Contains attributes for levels as indexes & the respective experience points. This is used by the player to level up & unlock the next power-up/upgrade.


---

## How to Play

- Start the Game: Launch the game, and the player spawns in the center of the level map.

- Explore: Move around the map and earn initial Exp to level up and choose a weapon to activate.

- Fight Enemies: Combat is real-time, requiring the player to dodge and aim projectiles while defeating waves of enemies.

- Level Up & Collect Power-ups : Defeat enemies to earn Exp, unlock upgrades for weapons or health, and enhance stats for longer survival.

- Survive Waves: Enemies spawn in increasingly difficult waves with higher numbers & shoter intervals as you level up.

- Game Over: The game ends if player dies. 

- Goal is to reach level Exp Level 25 to complete a level. High scores are recorded per level.
  
---

## Architecture Block Diagram

![MonsterSurvivors - Roguelike Survival Game](https://github.com/user-attachments/assets/c0fc9d68-4a13-4e5c-9084-2de6e0b242ae)

---

## Playable build

https://outscal.com/iamdeep75/game/play-monster-survivors-roguelike-survival-game-v11-game

---

## Gameplay Video

https://youtu.be/mdneP7PNX-I


