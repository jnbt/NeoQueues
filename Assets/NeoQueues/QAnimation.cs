using System;
using System.Collections;
using Neo.Async;

namespace Neo.Queues {
  /// <summary>
  /// Wraps any Unity-base animation into a queue item which will finished
  /// as soon as the animation has finished.
  /// </summary>
  /// <example><![CDATA[
  /// QAnimation qAni = new QAnimation(gameObject.GetComponent<Animation>());
  /// ]]></example>
  public class QAnimation : Base {
    private UnityEngine.Animation ani;

    /// <summary>
    /// Construct a queue item bound to a Unity animation
    /// </summary>
    /// <param name="ani"></param>
    public QAnimation(UnityEngine.Animation ani)
      : base() {
      this.ani = ani;
    }

    /// <summary>
    /// Stops the animation and finishes this queue item
    /// </summary>
    public void Abort() {
      ani.Stop();
      Finished();
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      ani.Play();
      Async.CoroutineStarter.Instance.Add(waitForAnimation(ani));
    }

    private IEnumerator waitForAnimation(UnityEngine.Animation ani) {
      do {
        yield return null;
      } while(ani.isPlaying);
      Finished();
    }
  }
}
