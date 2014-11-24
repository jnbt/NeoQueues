using System;

namespace Neo.Queues {
  /// <summary>
  /// A delegate which calls the done callback when its finished
  /// </summary>
  /// <param name="done">to be called on finish</param>
  public delegate void QueueTask(QueueCallback done);

  /// <summary>
  /// A task is a queue item which acts as an interface to other asynchronous 
  /// functions which don't have a IQueuable implementation. You only have to call
  /// the "done"-callback once your function has finished.
  /// </summary>
  /// <example><![CDATA[
  /// QueueTask task = new QueueTask(someMethodWhichCallsDone);
  /// task.Play();
  /// ]]></example>
  public class Task : IQueueable {
    private QueueTask func;

    /// <summary>
    /// Construct a QueueTask which wraps the given function
    /// </summary>
    /// <param name="func">to be used for execution</param>
    public Task(QueueTask func) {
      this.func = func;
    }

    /// <summary>
    /// Play this queue item by execution of the wrapped function
    /// </summary>
    /// <param name="done">send to the wrapped function</param>
    public void Play(QueueCallback done) {
      func(done);
    }

    /// <summary>
    /// Plays this queue item by execution of the wrapped function
    /// with an empty callback
    /// </summary>
    public void Play() {
      func(emptyDone);
    }

    private void emptyDone() {
      //nothing to do here
    }
  }
}
