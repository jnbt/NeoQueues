using System;

namespace Neo.Queues{
  /// <summary>
  /// This class allows shortcuts for creating queue items. See the separate classes
  /// for further documentation.
  /// </summary>
  /// <example><![CDATA[
  /// Q.Script(
  ///   Q.Parallel(
  ///     Q.Tween(gameObject.transform.DOMove(targetPorision, 1.5f)),
  ///     Q.Animation(gameObject.GetComponent<Animation>())
  ///   ),
  ///   Q.Delegation(invokeAfterAnimationAndTween)
  /// );
  /// ]]></example>
  public static class Q{
    /// <summary>
    /// Builds a simple (empty) queue item
    /// </summary>
    /// <param name="directFinishOnPlay"></param>
    /// <returns>A new Base queue item</returns>
    public static Base Base(bool directFinishOnPlay = false){
      return new Base(directFinishOnPlay);
    }

    /// <summary>
    /// Builds a sequential queue
    /// </summary>
    /// <param name="items">to be queued</param>
    /// <returns>A new sequential queue</returns>
    public static Script Script(params IQueueable[] items){
      return new Script(items);
    }

    /// <summary>
    /// Builds a (pseudo)-parallel queue
    /// </summary>
    /// <param name="items">to be queued</param>
    /// <returns>A new parallel queue</returns>
    public static Parallel Parallel(params IQueueable[] items){
      return new Parallel(items);
    }

    /// <summary>
    /// Wraps a delegate
    /// </summary>
    /// <param name="what">to be queued</param>
    /// <returns>A new queue item for the delegation</returns>
    public static Delegation Delegation(QueueableDelegate what){
      return new Delegation(what);
    }


    /// <summary>
    /// Wraps a DOTween tween into a queue item
    /// </summary>
    /// <param name="tween">to be queued</param>
    /// <returns>A new queue item from the tween</returns>
    public static DotTween Tween(DG.Tweening.Tween tween) {
      return new DotTween(tween);
    }

    /// <summary>
    /// Wraps a Unity animation as a queue item
    /// </summary>
    /// <param name="animation">to be queued</param>
    /// <returns>A new queue item for the animation</returns>
    public static QAnimation Animation(UnityEngine.Animation animation){
      return new QAnimation(animation);
    }

    /// <summary>
    /// Builds a time-based queue item
    /// </summary>
    /// <param name="seconds">to be waited</param>
    /// <returns>A new queue item which will wait the given seconds</returns>
    public static Timeout Timeout(float seconds){
      return new Timeout(seconds);
    }

    /// <summary>
    /// Wraps any Unity YieldingInstruction as a queue item.
    /// </summary>
    /// <param name="instruction">to be wrapped</param>
    /// <returns>A new queue item for the instruction</returns>
    public static Yielding Yielding(UnityEngine.YieldInstruction instruction){
      return new Yielding(instruction);
    }

    /// <summary>
    /// Allow short creation of a Lazy queue item which "real" queue content
    /// will be determined at time of execution.
    /// </summary>
    /// <param name="generator">to build the "real" queue item</param>
    /// <returns>A new lazy queue item</returns>
    public static Lazy Lazy(Func<IQueueable> generator){
      return new Lazy(generator);
    }

    /// <summary>
    /// Wraps any task as a queue item
    /// </summary>
    /// <param name="what">to be queued</param>
    /// <returns>A new queued task item</returns>
    public static Task Task(QueueTask what) {
      return new Task(what);
    }
  }
}
