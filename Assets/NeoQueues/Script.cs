using System;
using System.Collections.Generic;

namespace Neo.Queues {
  /// <summary>
  /// A script queue is a queue item which executed 0..n other queue items
  /// after another. When one item has be finished the next item is played
  /// until no further items are available. You can add or prepend further items
  /// before the execution starts.
  /// 
  /// The Script item finishes when all queue items has been finished.
  /// </summary>
  /// <example><![CDATA[
  /// Script queue = new Script(
  ///   buildFirstTween(),
  ///   new Delegation(someLongMethod)
  /// );
  /// queue.Prepend(new QAnimation(findUnityAnimation()));
  /// queue.Play();
  /// ]]></example>
  public class Script : Base {
    private readonly List<IQueueable> list = new List<IQueueable>();
    private int currentIndex;

    /// <summary>
    /// Constructs an empty queue
    /// </summary>
    public Script()
      : base() {
    }

    /// <summary>
    /// Constructs a queue bound to 1..n queue items
    /// </summary>
    /// <param name="item">a queue item to execute</param>
    /// <param name="items">further items to be executed sequentially</param>
    public Script(IQueueable item, params IQueueable[] items)
      : base() {
      list.Add(item);
      list.AddRange(items);
    }

    /// <summary>
    /// Constructs a queue from an array of queue items
    /// </summary>
    /// <param name="items">to be executed sequentially</param>
    public Script(IQueueable[] items)
      : base() {
        list.AddRange(items);
    }

    /// <summary>
    /// Adds (appends) a futher queue item
    /// </summary>
    /// <param name="item">to be appended</param>
    public void Add(IQueueable item) {
      list.Add(item);
    }

    /// <summary>
    /// Prepends a further queue item
    /// </summary>
    /// <param name="item">to be prepended</param>
    public void Prepend(IQueueable item) {
      list.Insert(0, item);
    }

    /// <inheritdoc />
    protected override void Execute() {
      currentIndex = -1;
      Proceed();
    }

    private void Proceed() {
      currentIndex++;
      if(currentIndex >= list.Count) {
        Finished();
      } else {
        IQueueable Next = list[currentIndex];
        Next.Play(Proceed);
      }
    }

  }
}
