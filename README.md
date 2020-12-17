# Unity week 5: Two-dimensional scene-building and path-finding

A project with step-by-step scenes illustrating how to construct a 2D scene using tilemaps,
and how to do path-finding using the BFS algorithm.


### Q4 - Random map creating  

* An algorithm that receives a TileMap, 3 TileBases, fill percentage, map size, number of simulation steps and stop time - and creates a map with several different types of tiles so that similar tiles are next to each other.

* We extended the function from the lesson in that after each call to the SmoothMap function, we randomly raffled with a probability  of  half wether of replacing the second tile with a third,
In addition - if a particular tile does not have at least one neighbor of the same type, we will replace the tile with the other tile.

* Main Script: 

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/Scripts/4-generation/TilemapCaveGenerator.cs>

* Screenshot:

![](Images/q_4.jpeg)


### Q5 - Quarrying in Mountain

* We added to the player the option to carve in the mountain by clicking on an X and a suitable arrow pointing to a mountain, and the mountain will become grass, thus making sure that the player does not get stuck in a cave.
The quarrying is not done immediately, but after a second.

* We expanded the KeyBoradMover class by adding a Boolean field that checks every time an arrow is pressed, whether the X key is pressed and thus in the KeyboradMoverByTile class in the Update function we checked if the Boolean field is true and the player's new position is in a mountain type tile, and if all conditions are met - 
We will override the mountain tile and replace it with grass tile after a second.

* Main Scripts:

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/Scripts/2-player/KeyboardMoverByTile.cs>

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/Scripts/2-player/KeyboardMover.cs>

* Screenshot:

![](Images/q_5.jpeg)

### Q6 - Infinite number of levels and increasing the TileMap size

* We added a class called TilemapCaseGenerationQ_6 that executes SmoothMap every second and creates a cave.
In addition - we added a player and 2 enemies chasing him to the map (by adding a component that uses the BFS algorithm), so the player uses the keyboard arrows and has to reach the endPoint tile to go through a stage and start a stage with a map 10% larger than the previous one.

* TilemapCaveGeneratorQ_6 class receives TileMap and 2 tiles, fill percentage, size, number of steps and stop time, and creates a cave.
We added a component to the player called LocatingThePlayer that initializes the player's position, and similarly, we added a component to opponents called LocatingTheEnemy that initializes the opponent's position, and similarly to the stage finish tile.
In addition, we added a component called NextLevel that receives at the beginning of the Start function a TilemapCaveGeneratorQ_6 object and size and in the Update function checks whether the player's position is on an endPoint tile, and if so - increases the map size and activates the Start function again

* Main Scripts:

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/NextLevel.cs>

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/LocatingThePlayer.cs>

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/LocatingTheEnemy.cs>

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/TileEnd.cs>

<https://github.com/Ron-Yogev/Ex8-Algorithms/blob/master/Assets/Scripts/4-generation/TilemapCaveGeneratorQ_6.cs>

* Screenshots:

![](Images/q_6_1.jpeg)

![](Images/q_6_2.jpeg)

![](Images/q_6_3.jpeg)
