# NeoQueues: A class library for working for callback-driven queues

When scripting UI animations and interactions one often has to work with the execution of different tasks,
animations, tweens, etc.

NeoQueues helps to abstract the handling of these kinds of flows by nested and (re-)combined single queueable items.

It's real power comes from a the very small interface `IQueuable` which allows extending the supported set of
queueable items, but it ships with an API for:

* Calling a synchronous delegates
* Calling and waiting for an asynchronous task
* Starting and waiting for a Unity animation
* Starting and waiting for a [HOTween](http://hotween.demigiant.com) tween and a sequence of tweens
* Waiting for some seconds
* Waiting for Unity's [YieldInstruction](http://docs.unity3d.com/ScriptReference/YieldInstruction.html)
* Processing a list if items distributed over rendered frames
* Combining single items as a sequence (which results in one new queue item)
* Combining single items to be played simultaneously (which results in one new queue item)

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
* Install the *free* [Visual Studio Tools for Unity](http://unityvs.com) and import its Unity-package
* Open `NeoQueues.sln`
* [Build a DLL-File](http://forum.unity3d.com/threads/video-tutorial-how-to-use-visual-studio-for-all-your-unity-development.120327)
* Import the DLL into your Unity3D project

## Dependencies

* [NeoCollections](https://github.com/jnbt/NeoCollections)
* [NeoAsync](https://github.com/jnbt/NeoAsync)
* [HOTween](http://hotween.demigiant.com)

## General Usage

To script a series of animations, tweens and other UI processes one often has to wait for the single effects to finish before starting
the next one. This library helps to create one unique flow when combining these effects.

### Example scenario

Assume the following scenario where the program uses the [HOTween](http://hotween.demigiant.com) library and Unity animations:

```csharp
void effectDying(Action callbackWhenFinished) {
  var legAnimation  = legs.GetComponent<Animation>();
  var headAnimation = head.GetComponent<Animation>();
  var bodyTransform = body.transform;

  // first without any flow but simultaneously
  legAnimation.Play();
  headAnimation.Play();
  // wait for 0.5s is missing here
  var bodyTween = HOTween.To(bodyTransform, 2.5f, new TweenParms().Prop("localScale", new Vector3(0f, 0.1f, 0)));

  //this is directly invoked and isn't a correct callback yet
  callbackWhenFinished()
}
```

The idea is to first play the `legAnimation` together with the `headAnimation` and then wait for both to be finished.
The next effect would be the tween which would be completed by playing the sound effect.

    ----------------     -----------------
    | legAnimation |     | headAnimation |
    ----------------     -----------------
            \                  /
              \              /    wait until both have finished
                \          /
              -----------------
              | wait for 0.5s |
              -----------------
                     |
              ----------------
              |   bodyTween   |
              ----------------
                     |
                     ---> callbackWhenFinished();

### Using a scripted queue

By combining different kind of _queue items_ one single queue can be build and then executed:

```csharp
void effectDying(Action callbackWhenFinished) {
  var legAnimation  = legs.GetComponent<Animation>();
  var headAnimation = head.GetComponent<Animation>();
  var bodyTransform = body.transform;

  Q.Script(
    Q.Parallel(
      Q.Animation(legAnimation),
      Q.Animation(headAnimation)
    ),
    Q.Timeout(0.5f),
    Q.Tween(bodyTransform, 2.5f, new TweenParms().Prop("localScale", new Vector3(0f, 0.1f, 0)))
  ).Play(callbackWhenFinished)
}
```

## Advanced Usage

You can extend NeoQueues by implementing further classes which implement the `IQueuable` interface. Every instance
of this interface can be used in the main queue items `Script` and `Parallel`.

### Example of a further implementation

Assume you want to embed an [AudioSource](http://docs.unity3d.com/ScriptReference/AudioSource.html) into a queue, you need
provide a wrapper instance for the audio clip:

```csharp
var audioSource = gameObject.GetComponent<AudioSource>();

Q.Script(
  someOtherQueueItem,
  new QueuedAudio(audioSource) // QueuedAudio isn't implemented yet
  anotherQueueItem
).Play(() => {
  //called when finished
  Debug.Log("Finished!");
})
```

A simple implementation for `QueuedAudio` could be:

```csharp
using System.Collections;
using UnityEngine;
using Neo.Queues;

public class QueuedAudio : IQueuable {
  private AudioSource audio;
  private QueueCallback callback;

  public QueuedAudio(AudioSource audio) {
    this.audio = audio;
  }

  // This is the main function which will be invoked on all members which are
  // part of a queue
  public void Play(QueueCallback callback) {
    this.callback = callback;
    execute();
  }

  // This function must be implemented for common interface as one might
  // play a queue (or a single queue item) without a callback
  public void Play() {
    this.callback = null; // to be explicit
    execute();
  }

  private void execute() {
    this.audio.Play();
    Async.CoroutineStarter.Instance.Add(waitForAudioSource());
  }

  private void finished() {
    if(this.callback != null) this.callback();
  }

  private IEnumerator waitForAudioSource(){
    do{
      yield return null;
    } while(this.audio.isPlaying);
    finished();
  }
}
```

Every code beside the `Play(...)` functions is only problem specific. So when embedding new kinds
of items into a queue you only have to call the given callback once your new item is finished.
How you determine when it is finished is totally up to you.

## Acknowledgements

It was [headjump](https://github.com/headjump)'s concept and idea to structure any kind of asynchronous
stuff by combining and nesting queue items. I've implemented this for our projects using Unity3D.

## Testing

Use Unity's embedded Test Runner via `Window -> General -> Test Runner`.

## Licenses

For the license of this project please have a look at LICENSE.txt

### HOTween

    HOTween License
    Copyright (c) 2012 Daniele Giardini - DEMIGIANT
    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
