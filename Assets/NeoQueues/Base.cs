namespace Neo.Queues {
  /// <summary>
  /// A basic no-op implemenation of the IQueueable pattern.
  /// If DirectFinishOnPlay is set this queuable will directly notify the
  /// queue about its completion.
  /// 
  /// This queue item can be used to realize Null-Pattern of a queue item.
  /// </summary>
  /// <example><![CDATA[
  /// Base b = new Base(true);
  /// b.Play(() => {
  ///   // this callback will directly be called when play is invoked 
  /// });
  /// ]]></example>
  public class Base : IQueueable {
    /// <summary>
    /// Callback to be called when executed
    /// </summary>
    public QueueCallback Callback { get; private set; }
    /// <summary>
    /// If set the callback will be directly called on execution,
    /// otherwise you must manually call "Finished"
    /// </summary>
    public bool DirectFinishOnPlay { get; protected set; }

    /// <summary>
    /// Construct a basic queue item, which can directly be finished on execution
    /// </summary>
    /// <param name="directFinishOnPlay">to directly finish on execution</param>
    public Base(bool directFinishOnPlay = false) {
      this.DirectFinishOnPlay = directFinishOnPlay;
    }

    /// <summary>
    /// Start this queue item 
    /// </summary>
    /// <param name="callback">to be called on finish</param>
    public virtual void Play(QueueCallback callback) {
      this.Callback = callback;
      Execute();
    }

    /// <summary>
    /// Start this queue item without a callback
    /// </summary>
    public virtual void Play() {
      this.Play(null);
    }

    /// <summary>
    /// Finish this queue item
    /// </summary>
    public void Finished() {
      if(Callback != null) Callback();
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected virtual void Execute() {
      if(DirectFinishOnPlay) Finished();
    }
  }
}
