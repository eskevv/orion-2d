# OrionFramework
Cross-platform Game Framework using XNA and C#

Experimental extension library built for MonoGame.
3D support is already implemented but has not been extended yet for making games with it - soon to be added.
This framework can also be combined with my ECS for a different approach in creating entities.

-- Orion works closely with Tiled to parse your maps and instantiate objects dynamically through a <Key,Type> value pair that you can link to map layers.
This is just one approach and there are more ways to create your levels, for example inheriting from the Scene class and adding that to the scene Manager.

**NOTE: To test and run the included working solution - you need to run the application from the ./Tester directory.
The project already has a reference to the framework so you need to keep all solution paths the same to use the included assets.**

# Using Orion2D
> Orion2D is an extension library not a full engine. You need to to create a MonoGame project first for the source code to work.
1. Create a MonoGame project  
```dotnet new mgdesktopgl```
  install this template with dotnet new --install MonoGame.Templates.CSharp if you don't have it  
2. Add the Orion2D package (you could also easily bring in all the source code)
3. Create a class that inherits from ```Engine``` This class inherits from the MonoGame ```Game``` class and will serve as the core for your application. All entity and initialization is done from here.

## Features ##
* Camera
* Animation
* Scene Manager
* Entity manager
* Level parser(FirstLight)
* Collisions
* Particle System

**Partial**
* Scripts
* UI
* Physics
