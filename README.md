# Streetgears-Datamanager
This is a tool to extract and create resource files for street gears.
It runs on Windows, Mac & Linux.

[![Build Status](https://travis-ci.org/itsexe/Streetgears-Datamanager.svg)](https://travis-ci.org/itsexe/Streetgears-Datamanager)

## Features
+ Viewing contents of res files
+ Extracting res files
+ Create new res files

## Usage
Since this is a dotnet core application, you obviously need to install dotnet core. (https://www.microsoft.com/net/core)
To execute the application simply run: "dotnet run"

Even tough this is a dotnet core application, the code is still compatible with the regular .NET Framework. (Just copy over the code and everything should be fine)

## About the file format
The street gears client uses 201 files to store data in.
The first file (res.000) is the index file. It contains the offset, the encrypted file name and the file size.

### Index File (res.000)
This file contains all file names, sizes and offsets of the files stored in the res.001 - res.200 files.

Internal structure of an entry in the index file (res.000):

Description | Length
--- | -----------
Length of the encrypted file name   | 1 Byte
Encrypted file name  | X Bytes
Offset (Encrypted)  | 4 Bytes
File size (Encrypted)   | 4 Bytes

### Data Files (res.001 - res.200)
All binary data of the files specified in the res.000 is stored in the data files.
The files are not separated by anything. The game knows by the offset and file size where exactly the file begins an where it ends.
Some file types must be encrypted before writing them in the data file:

File | Encrypted | Category | Description
--- | -----------|------ |-------------
map   | Yes | Map | Contains the location of 3D models etc
item  | Yes | Map | Contains the location of items on the maps
trick   | Yes | Map | Contains locations of trick areas on the maps
npc   | Yes | Map | Contains the location of npcs on the maps
nui   | Yes | UI | Definitions of ingame windows (window sizes, button locations etc.)
nus   | Yes | UI | Scripts for ingame windows written in lua
ttf   | Yes | UI | Font files
spr   | Yes | UI | Defines background images for buttons and windows
tga   | No | UI | Images (Mostly for ui stuff)
bmp   | Yes | Images | Images
jpg  | Yes | Images | More Images
dds   | No | Images | Textures
jtv   | Yes | Videos | Videos (Intro etc.)
naf   | Yes | 3D | Has something todo with the nx3 files (Maybe extentions like hair or something)
nx3   | Yes | 3D | 3D Models (can be edited in blender: https://github.com/glandu2/BTRFdom)
rsg   | Yes | - | Maybe effects for tricks (?)
txt   | Yes | - | Contains various data like item definitions, localizations, blacklisted words for the chat etc.
wav   | Yes | - | sound files
lua   | Yes | - | Scripts for animations, missions etc.
ffe   | No | - | ?
fx   | No | - | effetcs (?)


<p align="center">
  <img src="https://raw.githubusercontent.com/itsexe/Streetgears-Datamanager/master/unpack.png" alt="Logo"/>
</p>
