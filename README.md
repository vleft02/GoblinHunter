## About

This project introduces a First-Person dungeon crawler developed using Unity and C#. In terms of visual aesthetics, the game draws inspiration from the iconic Doom(1993), infusing elements like 8-directional sprites/animations, along with a 2.5D visual style. The game incorporates a variety of technologies with some notable mentions being the procedural map generation, the State Machine design pattern and the Save System. Please note that this project is designed for educational purposes only and not intended for commercial distribution.

The game comprises three distinct areas, with the Dungeon being one of them. The Dungeon, featuring procedural generation through a custom Depth First Search algorithm, which guarantees a unique layout for every playthrough. This implementation significantly enhances the replay value of the game, offering players a fresh and distinctive experience with each exploration of the dungeon.
## Features

- **Dungeon Generator:** Implementation of custom Depth First Search algorithm, to introduce procedural generation of the Dungeon map, along with dynamic placement of enemies & specific items.
- **Save System:** Serialization of player stats/progress and save to JSON file.
- **Load Sytem:** Deserialization of saved file and player stats load.
- **State Machines:** Custom state machine implementation to handle the animation and behaviour of the AIs, in consultation with Unity's framework.
- **Custom Shaders:** Implementation of custom shaders, using Blender and Unity, to construct a world that is consistent and well thought out.
- **Terrain:** Utilization of [Unity Terrain Tools](https://assetstore.unity.com/packages/tools/terrain/terrain-tools-64852#releases) for level design.

## Structure
- Scripts
```
📦Scripts
 ┣ 📂Animation
 ┃ ┣ 📜AnimationAspectManager.cs
 ┃ ┣ 📜AnimationStateManager.cs
 ┃ ┣ 📜BaseAnimationState.cs
 ┣ 📂Dungeon
 ┃ ┣ 📜DungeonGenerator.cs
 ┃ ┣ 📜RoomBehaviour.cs
 ┣ 📂Effect
 ┃ ┣ 📜CameraFade.cs
 ┃ ┣ 📜Effects.cs
 ┣ 📂Enemy
 ┃ ┣ 📂EnemyAnimationStates
 ┃ ┃ ┣ 📜EnemyAttackState.cs
 ┃ ┃ ┣ 📜EnemyHitState.cs
 ┃ ┃ ┣ 📜EnemyIdleState.cs
 ┃ ┃ ┣ 📜EnemyToDeathState.cs
 ┃ ┃ ┣ 📜EnemyWalkState.cs
 ┃ ┣ 📂EnemyStates
 ┃ ┃ ┣ 📜EnemyAttackLogicState.cs
 ┃ ┃ ┣ 📜EnemyChaseState.cs
 ┃ ┃ ┣ 📜EnemyDeathState.cs
 ┃ ┃ ┣ 📜EnemyPatrolState.cs
 ┃ ┣ 📜EnemyAnimationFSM.cs
 ┃ ┣ 📜EnemyController.cs
 ┃ ┣ 📜EnemyStateMachine.cs
 ┣ 📂Interactables
 ┃ ┣ 📂InteractionEvent
 ┃ ┃ ┣ 📜FamilyCookedEvent.cs
 ┃ ┃ ┣ 📜InteractionEvent.cs
 ┃ ┃ ┣ 📜ScreamEvent.cs
 ┃ ┣ 📜AreaChangeInteractable.cs
 ┃ ┣ 📜AxeInteractable.cs
 ┃ ┣ 📜DaggersInteractable.cs
 ┃ ┣ 📜HealingInteractable.cs
 ┃ ┣ 📜Interactable.cs
 ┃ ┣ 📜KeyInteractable.cs
 ┃ ┣ 📜OpenCloseObject.cs
 ┃ ┣ 📜SwordInteractable.cs
 ┣ 📂Menu
 ┃ ┣ 📜Credits.cs
 ┃ ┣ 📜CreditsMenu.cs
 ┃ ┣ 📜EquipMenu.cs
 ┃ ┣ 📜LoadMenu.cs
 ┃ ┣ 📜MainMenu.cs
 ┃ ┣ 📜MenuInitializer.cs
 ┃ ┣ 📜NewGameMenu.cs
 ┃ ┣ 📜OptionsMenu.cs
 ┃ ┣ 📜PauseMenu.cs
 ┃ ┣ 📜SaveSlot.cs
 ┣ 📂Player
 ┃ ┣ 📂PlayerStates
 ┃ ┃ ┣ 📜AttackState.cs
 ┃ ┃ ┣ 📜DeathState.cs
 ┃ ┃ ┣ 📜EquipState.cs
 ┃ ┃ ┣ 📜HitState.cs
 ┃ ┃ ┣ 📜IdleState.cs
 ┃ ┃ ┣ 📜UnequipState.cs
 ┃ ┣ 📜Player.cs
 ┃ ┣ 📜PlayerAnimationFSM.cs
 ┃ ┣ 📜PlayerController.cs
 ┃ ┣ 📜PlayerInteract.cs
 ┃ ┣ 📜PlayerLogic.cs
 ┃ ┣ 📜PlayerRotate.cs
 ┃ ┣ 📜PlayerRotateSmooth.cs
 ┃ ┣ 📜PlayerSFX.cs
 ┃ ┣ 📜PlayerUI.cs
 ┃ ┣ 📜PlayerVFX.cs
 ┣ 📂SaveSystem
 ┃ ┣ 📂Data
 ┃ ┃ ┣ 📜AreaData.cs
 ┃ ┃ ┣ 📜EnemyData.cs
 ┃ ┃ ┣ 📜GameData.cs
 ┃ ┃ ┗ 📜PlayerData.cs
 ┃ ┣ 📜GameRuntimeManager.cs
 ┃ ┣ 📜PlayerProfile.cs
 ┃ ┣ 📜SaveFinder.cs
 ┃ ┣ 📜SaveManager.cs
 ┣ 📂States
 ┃ ┣ 📜BaseState.cs
 ┃ ┣ 📜StateManager.cs
 ┣ 📂Weapon
 ┃ ┣ 📜Axe.cs
 ┃ ┣ 📜Daggers.cs
 ┃ ┣ 📜Hands.cs
 ┃ ┣ 📜Sword.cs
 ┃ ┣ 📜Weapon.cs
 ┃ ┣ 📜WeaponManager.cs
 ┣ 📜EventManager.cs
 ┣ 📜GameIntro.cs
 ┣ 📜Hittable.cs
 ┣ 📜OutllneEffect.cs
 ┣ 📜SpriteBillboard.cs
 ┗ 📜Utils.cs

```
<iframe src="https://docs.google.com/gview?url=https://raw.githubusercontent.com/Panattack/GoblinHunter/main/Goblin_Hunter_Report.pdf&embedded=true" style="width:100%; height:600px;" frameborder="0"></iframe>
