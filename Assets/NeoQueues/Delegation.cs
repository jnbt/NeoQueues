namespace Neo.Queues {
  /// <summary>
  /// Empty delegate to be called as a queue item
  /// </summary>
  public delegate void QueueableDelegate();

  /// <summary>
  /// Allows a delegate to be part of a queue. The delegate will be called
  /// when this queue item is played. After the delegate is finished this item
  /// will finish.
  /// </summary>
  public class Delegation : Base {
    private QueueableDelegate what;

    /// <summary>
    /// Construct a queue item which is bound to a delegation
    /// </summary>
    /// <param name="what">to be called on execution</param>
    public Delegation(QueueableDelegate what)
      : base() {
      this.what = what;
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      if(what != null) what();
      Finished();
    }
  }
}
