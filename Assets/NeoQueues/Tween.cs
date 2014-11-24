using Holoville.HOTween;

namespace Neo.Queues {
  /// <summary>
  /// Allows the usage of a HOTween's tween as a queue item.
  /// See http://hotween.demigiant.com/index.html for futher information about
  /// the tweening engine.
  /// </summary>
  /// <example><![CDATA[
  /// Tween tween = new Tween(someobject, 2f, new TweenParms().Prop("alpha", 1f));
  /// tween.Play();
  /// ]]></example>
  public class Tween : Base {
    private object target;
    private float duration;
    private TweenParms targetParams;
    private Tweener tweener;

    /// <summary>
    /// Wraps a HOTween tween as a queue item
    /// </summary>
    /// <param name="target">of the tween</param>
    /// <param name="duration">of the tween</param>
    /// <param name="targetParams">params of the tween</param>
    public Tween(object target, float duration, TweenParms targetParams)
      : base() {
      this.target = target;
      this.duration = duration;
      this.targetParams = targetParams;
      this.targetParams.OnComplete(Finished);
    }

    /// <summary>
    /// Completes the tween if the execution has already been started
    /// </summary>
    public void Complete() {
      if(tweener != null) tweener.Complete();
    }

    /// <summary>
    /// Kills the tween (if already started) and finishes this queue item
    /// </summary>
    public void Abort() {
      if(tweener != null) tweener.Kill();
      Finished();
    }

    /// <summary>
    /// Executes this queue item
    /// </summary>
    protected override void Execute() {
      tweener = HOTween.To(target, duration, targetParams);
    }
  }
}
