# Sinbad Studio - Flappy Bird

A space-themed Flappy Bird clone built in Unity, featuring a spaceship navigating through asteroid fields with dynamic physics and beautiful parallax backgrounds. This is just a test project for Sinbad Studio

## Unity Version

**Unity 6000.1.13f1**

## Unity Modules

- **WebGL**

## Key Features

### 🚀 **Enhanced Physics System**

- **Dynamic Gravity**: Gravity increases the longer the ship falls, creating more realistic flight mechanics
- **Smooth Rotation**: Ship rotates based on movement direction with configurable limits
- **Flap Mechanics**: Mouse click or spacebar to apply upward thrust

### 🎮 **Gameplay Mechanics**

- **Asteroid Spawning**: Procedurally generated asteroids with random sizes and angles
- **Score System**: Points earned by passing through asteroids
- **Best Score Tracking**: Automatic high score persistence
- **Game States**: Start screen, active gameplay, and restart functionality

### 🏗️ **Technical Architecture**

- **Singleton Pattern**: Centralized level management
- **Component-Based Design**: Modular systems for easy maintenance
- **Interface Implementation**: IDamageable interface for consistent damage handling
- **Event-Driven Architecture**: Action-based communication between systems

## Project Structure

```
Assets/
├── Animation/         # Mostly ship animation files
├── Animator/          # Animator controllers
├── Materials/         # Shader materials for backgrounds
├── Prefabs/           # Reusable game objects (Asteroid, SceneManager)
├── Scenes/            # Game scenes (Initialize, Game)
├── Script/            # All C# scripts organized by functionality
│   ├── Interface/     # IDamageable interface
│   ├── Managers/      # LevelManager for game state
│   ├── Obstacles/     # Obstacles like asteroids spawning and behavior
│   ├── Ship/          # Player spaceship and scoring
│   ├── UI/            # User interface management
│   └── Utils/         # Utilities (Parallax, Singleton)
├── Settings/          # Unity render pipeline and build settings
└── Textures/          # Art assets (backgrounds, ships, obstacles, UI)
```

## Core Scripts

### **SpaceShip.cs**

- Main player controller with physics-based movement
- Dynamic gravity system that increases during falls
- Smooth rotation based on velocity
- Animation integration with movement states

### **LevelManager.cs**

- Singleton managing overall game state
- Coordinates between all game systems
- Handles game start, restart, and reset functionality

### **AsteroidSpawner.cs**

- Procedural asteroid generation
- Configurable spawn rates and variations
- Automatic cleanup and pooling

### **PointsCounter.cs**

- Score tracking and best score management
- Collision detection for scoring points
- Event-driven score updates

## Controls

- **Mouse Click** or **Spacebar**: Flap/Apply upward thrust
- **UI Buttons**: Start and restart game

## Getting Started

1. **Open in Unity**: Load the project in Unity 6000.1.13f1 or compatible version
2. **Scene Setup**: Open the Game scene first
3. **Play**: Hit the play button and click "Play" button to begin

## Game Flow

2. **Start Screen**: Player clicks start button to begin
3. **Active Gameplay**: Navigate spaceship through asteroid field
4. **Collision/Death**: Game over state with restart option
5. **Restart**: Reset all systems and return to active gameplay

## Credits

Developed by Kaids for Sinbad Studio as assessment task

---
