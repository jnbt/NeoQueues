# NeoQueues: A class library for working for callback-driven queues

When scripting UI animations and interactions one often has to work with the execution of different tasks,
animations, tweens, etc.

NeoQueues helps to abstract the handling of these queueable items.


## Installation

If you don't have access to [Microsoft VisualStudio](http://msdn.microsoft.com/de-de/vstudio) you can just use Unity3D and its compiler.
Or use your VisualStudio installation in combination with [Visual Studio Tools for Unity](http://unityvs.com) to compile a DLL-file, which
can be included into your project.

### Using Unity3D

* Clone the repository
* Copy the files from `Assets\NeoQueues` into your project

### Using VisualStudio

* Clone the repository
* Open the folder as a Unity3D project
* Install the *free* [Unity Testing Tools](https://www.assetstore.unity3d.com/#/content/13802) from the AssetStore
* Install the *free* [Visual Studio Tools for Unity](http://unityvs.com) and import its Unity-package
* Open `NeoQueues.sln`
* [Build a DLL-File](http://forum.unity3d.com/threads/video-tutorial-how-to-use-visual-studio-for-all-your-unity-development.120327)
* Import the DLL into your Unity3D project

## Dependencies

* [NeoCollections](https://github.com/jnbt/NeoCollections)
* [NeoAsync](https://github.com/jnbt/NeoAsync)
* [HOTween](http://hotween.demigiant.com/index.html)

## Usage

A description of usage will follow soon. For now please have a look at the documentation included in the source code.

## Licenses

For the license of this project please have a look at LICENSE.txt

### HOTween

    HOTween License
    Copyright (c) 2012 Daniele Giardini - DEMIGIANT
    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.