# Roguelike-Game

## Overview

This is rogue-like survival game where you must waves of monsters that keep on spawning more & more to take you down. Choose your weapons & upgrades them to become stronger and survive as long as you can!
The project uses design patterns like Service Locator, MVC, Object Pooling & Observer Pattern.

---

## Features

- Roguelike Mechanics: Engage in action-packed shoot'em up with different characters with various types of weapons & health upgrades.
  - 3 Player Characters :
    Ash - Medium Health & Speed, Brock - Low Speed & Large Health & Misty - High Speed & Small Health
  - 3 Weapons:
    Radial Reap - Area weapon that continuously grows & shrinks to deal damage at intervals based on the cooldown
    Orbital Fury - Spawns balls around the player that revolves around the player to deal damage at intervals based on the cooldown
    Scatter Shot - Shoot projectiles in different directions at intervals based on the cooldown

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

1. Start the Game: Launch the game and the player will be placed in the middle of the level map.

2. Explore: Move around the level map & get the first Exp for free to level up to choose a weapon.

3. Fight Enemies : Combat occurs in real time, and the player must defeat waves of enemies by dodging & aiming the projectiles by moving the player
   
4. Level Up: As you progress, your character gets stronger by defeating more enemies, gaining new abilities & upgrades with the weapons.

5. Collect Power-ups: Defeat waves of enemies to gain more Exp to Level Up & upgrade your weapons or health to boost your stats & the chance to survive longer.

6. Survive Waves: Enemies come in waves, with each wave being more difficult. The spawn frequency & spawn interval of enemies will change. The enemies will appear more in number & more frequently as you level up.

7. Game Over: The game ends when the player dies or reaches the final Exp Level (25 for now). High scores are recorded.
  
---

## Architecture Block Diagram


---

## Playable build


---

## Gameplay Video




