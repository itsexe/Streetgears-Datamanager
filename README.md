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

File | Encrypted
--- | -----------
bmp   | Yes
item  | Yes
jpg  | Yes
jtv   | Yes
lua   | Yes
map   | Yes
naf   | Yes
npc   | Yes
nui   | Yes
nus   | Yes
nx3   | Yes
rsg   | Yes
spr   | Yes
trick   | Yes
ttf   | Yes
txt   | Yes
wav   | Yes
tga   | No
dds   | No
ffe   | No
fx   | No


<p align="center">
  <img src="https://raw.githubusercontent.com/itsexe/Streetgears-Datamanager/master/unpack.png" alt="Logo"/>
</p>
