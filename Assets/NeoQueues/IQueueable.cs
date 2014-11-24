namespace Neo.Queues{
  /// <summary>
  /// Callback to be called when a IQueueable is finished
  /// </summary>
  public delegate void QueueCallback();

  /// <summary>
  /// Anything can be an item inside of a queue, when implementing
  /// this interface.
  /// </summary>
  public interface IQueueable{
    /// <summary>
    /// Any implementation of this interface should call the callback when 
    /// its execution is done. (Read: As the last part of the "Play" code path)
    /// </summary>
    /// <param name="callback"></param>
    void Play(QueueCallback callback);
    /// <summary>
    /// Any implemantion should also be playable without a callback
    /// </summary>
    void Play();
  }
}
