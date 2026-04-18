# 🎮 GameDev 2D Arena Combat

> ⚠️ **Note:** This branch contains additional development beyond the submitted version, including sound effects, UI improvements, and overall gameplay polish.

---

## 📌 Description

This project is a **2D top-down arena combat game** developed in Unity.

The player fights waves of enemies in a closed arena using melee combat and dodge mechanics. This branch represents an extended version of the project with improved feedback, audio, and UI systems.

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

---

## 🔊 Sound & Feedback Improvements

This branch introduces additional polish through:

- Background music 
- Game over audio  
- Improved feedback through visuals 
- Better player experience through combined UI and sound  

---

## ✨ Feedback (Juice)

- Visual feedback when taking damage (blinking)  
- UI updates  
- Sound effects for actions and game states  

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
- Audio system  

---

## ⚙️ Controls

| Action | Input |
|--------|-------|
| Move   | WASD  |
| Attack | Left Mouse Button |
| Dodge  | Right Mouse Button |

---

## 👥 Contributors

- Bubberr 
- Ingridksv

---

## 🚀 Improvements (Future Work)

- Settings menu (toggle UI + audio control)  
- More advanced AI (Behavior Trees)  
- Improved boss mechanics  
- Additional visual and audio feedback  

---

## 🧠 Design Considerations

This version focuses on improving **player experience** through:

- Better feedback (audio + visuals)  
- Clear UI communication  
- Responsive combat feel 

---
