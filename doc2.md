# PROJECT DOCUMENTATION
## Project name: **Purple Normandy Schnautzer** 

## Project theme
Implementation of the game Civilization (FreeCiv) with 2D visualization, loading map and game parameters from the database, support for many users on the network
bonus: client program for at least two different platforms or 3D visualization

## Game rules
Each player has an immovable castle and basic army, divided into branches (infantry, archers, pikemen etc.). Armies conquer the surrounding areas, gather materials (wood, gold etc.) and fight battles between each other. Players expand their castles and army, using the collected materials. 
The aim of the game is to conquer as many surrounding villages as possible and, of course, to defeat the opponent.

## Programming methodology
Non-rigorous interpretation of Scrum methodology, based on weekly meetings and more frequent online meetups 

## Technology & tools
Unity combined with Github and .json files

## Programming language
C#

## Task assignment
As in the initial part of creating our project it is not possible to work simultanously on similar tasks, for the begginning every member of our group does one part of the project to finally combine them together. The parts are further described below in the numered list.

## Version control system
Shared Github folder containing every element of the project with documentation. Each member uploads his/her own tasks marked with proper labels

## Scheduling of the project

### Division to stages of similar workload & time-consumption

1. Creating the scene
2. Map loading
3. Rendering map & objects
4. Single player game:
   
   4.1 camera movement

   4.2 units movement

   4.3 ability to place buildings

   4.4 buildings functionality
5. Multiplayer game & bug fixes     
   
## Software architectural pattern
The architectural pattern we've chosen is a client-server pattern. In our case, player 1 would act as a client as well as the server, and player 2 would be the client. Player 2, as a client, will be attached to the first player (server).

In addition, within the project we follow the SOLID rules of object-oriented programming.

## Data structure
Our database will be saved in JSON files.

