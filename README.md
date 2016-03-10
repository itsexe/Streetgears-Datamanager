# Streetgears-Datamanager
This is a tool to manage the res.xxx files.
It allows you to unpack an pack the res.000 - res.199 files.

[![Build Status](https://travis-ci.org/itsexe/Streetgears-Datamanager.svg)](https://travis-ci.org/itsexe/Streetgears-Datamanager)

## Features
+ Viewing contents of res files
+ Extracting the res files (and sort the content by the file type)
+ Create new res files

## Quick documentation about the file format
res.000 is the index file. It contains the offset, hash of the filename and the filesize.

res.001 - res.200 are containing all gamefiles (textures and stuff). The gamefiles are encrypted except tga, dds, ffe and fx files.

Which file has to be stored in which res file is calculated trough the hash of the filename. Look at the Code (StreetGearsDataCipher) for more details.



<p align="center">
  <img src="https://raw.githubusercontent.com/itsexe/Streetgears-Datamanager/master/unpack.png" alt="Logo"/>
</p>
