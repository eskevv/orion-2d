# OrionFramework
Cross-platform Game Framework using XNA and C#

Experimental and working game framework built on top of MonoGame primarily for 2D games.
3D support is already implemented but has not been extended yet for making games with it - soon to be added.

This framework can also be combined with my ECS for a different approach in creating entities.

-- Orion works closely with Tiled to parse your maps and instantiate objects dynamically through a <Key,Type> value pair that you can link to map layers.
This is just one approach and there are more ways to create your levels, for example inheriting from the Scene class and adding that to the scene Manager.

**NOTE: To test and run the included working solution - you need to run the application from the ./Tester directory.
The project already has a reference to the framework so you need to keep all solution paths the same to use the included assets.**

## Features ##
* Camera
* Animation
* Scene Manager
* Entity system
* Level parser(FirstLight)
* Scripts
* Collisions
* Particle System
* Extensible User Interface
* More physics coming soon

--Work in progress
