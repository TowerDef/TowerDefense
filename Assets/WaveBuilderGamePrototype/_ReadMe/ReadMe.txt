

1] Thank you for purchasing this simple little framework. 

2] I will be constantly updating this package once I find time to do so. 
If you have suggestions for mechanics and / or specific features you wish to see, feel free to contact me at wouterversloot@gmail.com and I will try to implement the requested features and update this package for no extra charge.

3] Special thanks to Sou Chen Ki, FlyingTeapot and PolyNext for uploading the models freely to the Unity Asset Store. I did not make the models myself, as they are only used for testing purposes. Please contact the authors of these assets to gain permission for use in commercial use as I can not give you that permission.
#1: The Earthborn Troll - Link: https://www.assetstore.unity3d.com/en/#!/content/13541 - Author:  Sou Chen Ki
#2: Red Samurai - Link: https://www.assetstore.unity3d.com/en/#!/content/4331 - Author: Sou Chen Ki
#3: FT_CaveWorm - Link: https://www.assetstore.unity3d.com/en/#!/content/3317 - Author: FlyingTeapot
#4: Spider Green - Link: https://www.assetstore.unity3d.com/en/#!/content/11869 - Author: PolyNext

4] The scripts are pretty basic and should help you on your way to creating your very own Tower Defense-like game! 
- Inside the hierarchy there is a component [Grid] that holds GridSlot GameObjects. Each GameObject has the Grid Slot component that checks if that single piece of grid space can be built upon.

- Inside the hierarchy there is a component [Spawners] that hold two spawn pools for units to be spawned out of. The spawners each hold a CreepSpawner component which is basically an array that is filled with CreepWave components.
	+ Each CreepWave component each hold their own respective array that hold GameObjects. These GameObjects are Unit prefabs located inside the Resources/Prefabs folder.
	
- Inside the hierarchy there is a component [GameManager] that holds the GameManager component. This class holds stats like the current wave, the next wave, the players gold and life points.

- Inside the hierarchy there is a component [WayPointManager] that holds WayPointPath components. Like the Spawner Component, this class also functions as an array that each hold their own respective WayPoint components. 
	+ These WayPoint components act as nodes where the Units will work towards. Beginning from the first {0} index, each unit walks to the next node once it gets into range of its current node. This process is continued until the last node has been reached. At the last node, the Units will switch to attacking mode, damaging the players base.
	
- Inside the hierarchy there is a component Main Camera that holds the CameraMovement component and the Grid Selector component.
	+ The CameraMovement component allows the player to scroll up and down the screen.
	+ The Grid Selector component allows the player to see which grid slot is currently being hovered upon.

- Each Unit Prefab holds a couple of classes and components. An Animation component, a UnitProperties component, a UnitAI component and a Capsule Collider. 
	+ The Animation component holds all the animations for that prefab.
	+ The UnitProperties component holds various values for the unit like Movement Speed, Rotation Degrees Per Second, Health Points, Attack Speed, Attack Damage and its Team status.
	+ The UnitAI component holds its current State like Walking, Attacking, Dying or Idling. It also holds all the models animations in arrays so that once an animation has finished playing, the UnitAI component will randomly pick a new animation to play that belongs to its current state. This means you can insert multiple attack animations and the Unit itself will randomly pick between these animations to play its next animation.
		+ The UnitAI is in close communication with the UnitProperties component, keep these two together at all times.
	+ The Capsule Collider allows for collision and keeping the units on the playing field.

- Each Tower Prefab holds one of a few example Tower Components. Currently there is an ArchTower, a LaserTower and a MachineTower component. 
	+ Each component can be customized in the inspector to give it unique values and behaviour.