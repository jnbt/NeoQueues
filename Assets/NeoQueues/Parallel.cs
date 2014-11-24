using Neo.Collections;

namespace Neo.Queues {
  /// <summary>
  /// A Parallel queue item allow the pseudo-parallel execution of 0..n other queue items.
  /// The queue items "Play" method will be invoked in order, so you might like to 
  /// "Add" or "Prepend" new items. But you should add further items once the execution has been
  /// started.
  /// 
  /// When all items have been called a Parallel is finished.
  /// </summary>
  /// <example><![CDATA[
  /// Parallel queue = new Parallel(
  ///   new Delegation(someLongMethod),
  ///   new Yielding(buildWebRequest())
  /// );
  /// queue.Prepend(buildSomeTweenAnimation());
  /// queue.Play();
  /// ]]></example>
  public class Parallel : Base {
    private List<IQueueable> list = new List<IQueueable>();
    private int countDownLatch;

    /// <summary>
    /// Construct an empty parallel queue
    /// </summary>
    public Parallel()
      : base() {
    }

    /// <summary>
    /// Constructs a Parallel which is bound to 1..n queue items
    /// </summary>
    /// <param name="q">a queue item</param>
    /// <param name="args">further queue items to be executed in parallel</param>
    public Parallel(IQueueable q, params IQueueable[] args)
      : base() {
      list.Add(q);
      list.AddRange(args);
    }

    /// <summary>
    /// Constructs a Parallel from an array of queue items
    /// </summary>
    /// <param name="args">to be executed in parallel</param>
    public Parallel(IQueueable[] args)
      : base() {
      list.AddRange(args);
    }

    /// <summary>
    /// Add another queue item (at the end of the execution list)
    /// </summary>
    /// <param name="q">to be exectued in parallel</param>
    public void Add(IQueueable q) {
      list.Add(q);
    }

    /// <summary>
    /// Add another queue item (at the beginning of the execution list)
    /// </summary>
    /// <param name="q">to be executed in parallel</param>
    public void Prepend(IQueueable q) {
      list.Insert(0, q);
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      if(list.Count == 0) {
        Finished();
      } else {
        countDownLatch = 0;
        for(int i = 0, imax = list.Count; i < imax; i++) list[i].Play(Done);
      }
    }

    private void Done() {
      countDownLatch++;
      if(countDownLatch == list.Count) {
        Finished();
      }
    }
  }
}
