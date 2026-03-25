# 🎮 GameDev 2D Arena Combat

> ⚠️ Note: Active development has continued in the `SoundEffects` branch, which includes additional features such as sound effects, UI improvements, and gameplay polish that are not yet merged into `main`.

---

## 📌 Description

This project is a **2D top-down arena combat game** developed in Unity.

The player fights waves of enemies in a closed arena using melee combat and dodge mechanics. The goal is to survive increasingly difficult waves and defeat the final boss.

---

## 🎮 Core Gameplay Loop

- Fight enemies  
- Dodge attacks  
- Survive waves  
- Progress to next wave  
- Defeat boss  

---

## ⚔️ Features

### Combat System
- Melee combat using hitboxes  
- Collision detection with triggers  
- HashSet used to prevent multi-hit issues  

### Enemy AI
- State-based AI (FSM-inspired)  
- States: Idle, Chase, Attack  
- Based on distance and cooldown  

### Wave System
- Enemies spawn in waves  
- Difficulty increases over time  
- Final wave includes a boss  

### Health System
- Event-based system (`onHurt`, `onDeath`)  
- Decoupled architecture using events  

### UI System
- Health display  
- Score system  
- Game Over / Win screens  
- Dynamic updates via events  

### Feedback (Juice)
- Visual feedback when taking damage (blinking)  
- UI updates  
- Sound effects  

---

## 🧱 Architecture

The project is built using Unity’s **component-based architecture**:

- Scripts are separated by responsibility (Combat, Health, AI, UI)  
- Prefabs are used for enemies and reusable objects  
- Singleton pattern is used for GameManager  

---

## 🔧 Technologies

- Unity (2D)  
- C#  
- Unity Events  
- Coroutines (IEnumerator)  
- Physics (Colliders & Triggers)  

---

## ⚙️ Controls

| Action | Key |
|------|-----|
| Move | WASD |
| Attack | Left Mouse Button |
| Dodge | Right Mouse Button |

---

## 👥 Contributors

- Bubberr
- Ingridksv

---

## 🧠 Design Considerations

The project focuses on:

- Simple but solid mechanics  
- Clear gameplay loop  
- Responsive player feedback  

We chose simple systems (e.g. FSM over Behavior Trees) to ensure a stable and polished experience within the project scope.

---
