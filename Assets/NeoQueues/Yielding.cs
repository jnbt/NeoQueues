using System.Collections;
using UnityEngine;

namespace Neo.Queues {
  /// <summary>
  /// Allows wrapping any Unity-based YieldingInstruction as a queue item.
  /// As soon as the instruction is done the queue item will be finished.
  /// </summary>
  /// <example><![CDATA[
  /// Yielding deferred = new Yielding(new WWW(someURLToLoad));
  /// deferred.Play();
  /// ]]></example>
  public class Yielding : Base {
    private YieldInstruction instruction;

    /// <summary>
    /// Wraps a YieldInstruction as a queue item
    /// </summary>
    /// <param name="instruction">to be waited</param>
    public Yielding(YieldInstruction instruction) {
      this.instruction = instruction;
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      Async.CoroutineStarter.Instance.Add(yielding());
    }

    private IEnumerator yielding() {
      yield return instruction;
      Finished();
    }
  }
}
