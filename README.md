# Pixion Dev Exam - Ability System

My simple Documentation of scripts and flow can be seen in this miro board: https://miro.com/app/board/uXjVIVbuKSk=/?share_link_id=953182407423


## Ability Related Scripts

**Ability**
- Responsible for handling cooldown and input, and triggering the ability behaviour.

Methods:
- OnTriggerAbility -> Triggers the ability Behaviour, checks if it's on cooldown and if other ability is executing
- RunCooldownTimer -> Async Task that performs the cooldown timer of the ability
- ResetCooldown -> resets the cooldown to false
- GetNormalizedRemainingTime() -> calculates the remaining normalized time for the cooldown

**Base Ability**
- Base Scriptable Oibject script containts ID and AbilityEnum

**IActivateable**
- interface for abilities that can be triggered

**ICooldown**
- interface for abilities that has a cooldown

**AbilityBehaviour**
- responsible for what happens when the ability is triggered. Will contain a list of AbilityPhases and control the transition from one to the next when they are completed.

Methods:
- ExecuteBehaviour -> Handles the execution of the phases in an async unitask

**AbilityPhase**
- sets up a timer with a duration (stat)  and is completed as soon as the timer ends. 

Methods:
- ExecutePhase -> Goes through the dictionary of timing and consequences, runs a timer for the duration of the list of consequences and executes them if the timing condition is met

**Consequence**
- base script for the consequences, they implement specific gameplay effects and can also trigger chained consequences. 
- Uses chain of responsibility pattern to execute the next consequenjce

Methods:
- ExecuteConsequence -> Triggers the condition for this current consequence
- ExecuteNextConsequence -> Triggers the next consequence if there is any

**RectOverlapConsequence**
- handles the consequence where in a physics overlap box is being made

Methods:
- Execute Consequence -> Spawns a physics overlap box and gets the list of enemy colliders
- SpawnVisualizer -> spawns the visualizer for the rect overlap box


**SphereOverlapConsequence**
- handles the consequence where in a physics overlap sphere is being made

Methods:
- Execute Consequence -> Spawns a physics overlap sphere and gets the list of enemy colliders
- SpawnVisualizer -> spawns the visualizer for the overlap sphere


**DealDamageConsequence**
- handles the damaging of the list of targets

Methods:
- Execute Consequence -> Gets the health component of the target and apply damage

**AbilityExtendableEnum**
- extendable scriptable obejcts that acts like an enum that will be used as keys for the different abilities of the player

**AbilityList**
- Scriptable object that contains a dictionary of Ability Extendable Enum (Key) and Ability (Value)

Methods:
- InitializeAbilities -> makes sure the abilities cooldowns are reset

**AbilityParameterExtendableEnum**
- extendable scriptable objects that acts like an enum that will be used as keys for the different parameters related to ability

**AbilityParameterHandler**
- scriptable object that contains the dictionary of parameters relating to ability

Methods:
- Initialize -> initializes the values for this script
- StartAbility -> Triggers the values of different parameters for when an ability is started
- FinishAbility -> Triggers the values of different parameters for when an ability is finished
- AbilityIsStillExecuting -> Triggers when an ability is still executing
- Set Parameter -> Adds a new parameter value pair to the dictionary
- Get Parameter -> Gets the parameter and cast it based on the type that is needed
- Remove Parameter -> Removes the key value pair in the dictionary



## Player Related Scripts

**PlayerController**
- Script that handles movement of the player

Methods:
- HandleMovement -> calculates the normalized direction and calls the rotate and move function
- Rotate -> takes care of player rotation
- Move -> takes care of player movement
- OnAbilityCast -> triggers when an ability is casted

**IMovable**
- interface used for any entity that can be moved

**IRotatable**
- interface used for any entity that can be rotated

**IAbilityCastable**
- interface used for any entity that can cast an Ability

**MovementStats**
-  Object that contains data for movement and rotation 

**PlayerInputreader**
- script that handles the different methods of Unity's Input actions
- contains the different subject observer variables that can be used by the player 

Methods:
- OnMove -> reads the value for the movement WASD
- OnAbility1 -> reads and triggers an event when Ability 1 is casted
- OnAbility2 -> reads and triggers an event when Ability 2 is casted
- OnAbility3 -> reads and triggers an event when Ability 3 is casted




## Camera Scripts

**CameraController**
- handles the following of the camera to the player

**CameraSettings**
- scriptable object method that containts different camera parameters like smooth speet, position offset, rotation offset




## Misc Scripts

**Health**
- handles health points of any entity

Methods:
- ApplyDamage -> Subsctracts damage value to the current HP

**IDamageable**
- Interface that is implemented when an entity is damageable

**Stat**
- scriptable object wrapper that contains a float




##UI View Scripts

**HUDManager** (Note: Used for debugging purposes)
- responsible for showing the ability icon bar and pop up texts

Methods:
- OnAbilityStillExecuting -> Shows an error text that an ability is still executing
- OnAbilityStarted -> Starts the Cooldown UI of an ability
- StartCooldownUI -> Runs a timer that fills a radial image of a cooldown UI


**HealthBar** (Note: Used for debugging purposes)
- handles the update of the entity's healthbar


## Utiliy Scripts

**Utility **
- Utility class contains static methods that can be used for different purpose, such as camera direction calculation.

Variables:
-  HALF_VALUE -> Constant float value to calculate the half-extents

Methods:
- CalculateCameraDirection -> Calculate the camera's normalized direction and ignores the isometric tilt of the camera
- GetNormalizeCameraDirection -> normalizes the camera's value and removes the tilt of the camera in the Y axis
- CalculateHalfExtents -> Calculates a Vector3 value of half-extents given the width, height and depth
- CalculateCorners -> Calculate the corners of a box given the center and half extents
- GetBoxEdges -> Define the edges of the box as pairs of corner indices

**MonoExt**
- Extension of the Mono Behaviour Script
- Handles automatic disposal of IDisposables, in this case used for the event listeners
- Has generic virtual methods to override and organize the way things are being called

**MonoExtScriptCreator**
- Editor script to allows you to create a templatized script that already inherits monoext and implements the methods of it


**RectOverlapConsequenceVisualizer** (Note: Used for debugging purposes)
- Draws the Rectangle Box in game view

Methods
- Initialize -> assigns the necessary values for the script
- OnRenderObject -> uses GL to draw the box's shape in game view


**SphereOverlapConsequenceVisualizer** (Note: Used for debugging purposes)
- Draws the Sphere in game view

Methods:
- Initialize -> assigns the necessary values for the script
- OnRenderObject -> uses GL to draw the sphere's shape in game view
- DrawCircle -> Draws a circle to approximate the sphere's shape


**VisualizerData**
- A scriptable object that contains the necessary data for the visualizers
