using Holoville.HOTween;

namespace Neo.Queues {
  /// <summary>
  /// Allows the usage of a HOTween's tween sequence as a single queue item.
  /// See http://hotween.demigiant.com/index.html for futher information about
  /// the tweening engine.
  /// </summary>
  /// <example><![CDATA[
  /// TweenSequence seq = new TweenSequence(buildSomeTweenSequence());
  /// seq.Play();
  /// ]]></example>
  public class TweenSequence : Base {
    private Sequence sequence;
    private bool running = false;

    /// <summary>
    /// Wrapps a Hotween tween sequence as a queue item
    /// </summary>
    /// <param name="sequence">to be executed</param>
    public TweenSequence(Sequence sequence)
      : base() {
      this.sequence = sequence;
      this.sequence.AppendCallback(Finished);
    }

    /// <summary>
    /// Completes the tween sequence if already started
    /// </summary>
    public void Complete() {
      if(running) sequence.Complete();
    }


    /// <summary>
    /// Kills the tween sequeuence if already started and
    /// finishes this queue item
    /// </summary>
    public void Abort() {
      if(running) sequence.Kill();
      Finished();
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      running = true;
      this.sequence.Play();
    }
  }
}
