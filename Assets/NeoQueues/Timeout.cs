using System.Collections;
using UnityEngine;

namespace Neo.Queues {
  /// <summary>
  /// Represents a simple time-based queue item which can be used
  /// in any queue.
  /// </summary>
  /// <example><![CDATA[
  /// Script queue = new Script(
  ///   new Delegation(method1),
  ///   new Timeout(2.5f), //seconds
  ///   new Delegation(method2) //started 2.5 seconds after method1 finishes
  /// );
  /// queue.Play();
  /// ]]></example>
  public sealed class Timeout : Base {
    private float seconds;

    /// <summary>
    /// Builds a queue item which waits for the given amount of seconds
    /// </summary>
    /// <param name="seconds">to wait</param>
    public Timeout(float seconds) {
      this.seconds = seconds;
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      Async.CoroutineStarter.Instance.Add(yielding());
    }

    private IEnumerator yielding() {
      yield return new WaitForSeconds(seconds);
      Finished();
    }
  }
}
