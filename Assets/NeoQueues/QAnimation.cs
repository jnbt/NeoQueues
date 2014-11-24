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
    private UnityEngine.Animation Ani;

    /// <summary>
    /// Construct a queue item bound to a Unity animation
    /// </summary>
    /// <param name="Ani"></param>
    public QAnimation(UnityEngine.Animation Ani)
      : base() {
      this.Ani = Ani;
    }

    /// <summary>
    /// Stops the animation and finishes this queue item
    /// </summary>
    public void Abort() {
      Ani.Stop();
      Finished();
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      Ani.Play();
      Async.CoroutineStarter.Instance.Add(waitForAnimation(Ani));
    }

    private IEnumerator waitForAnimation(UnityEngine.Animation ani) {
      do {
        yield return null;
      } while(ani.isPlaying);
      Finished();
    }
  }
}
