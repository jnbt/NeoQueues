using DG.Tweening;

namespace Neo.Queues {
  /// <summary>
  /// Allows the usage of a DOTween's tween as a queue item.
  /// The given tween will automatically be paused until the queue item is 
  /// started.
  /// </summary>
  /// <example><![CDATA[
  /// DotTween tween = new DotTween(myTransform.DOMove(Vector3.zero, 2f));
  /// tween.Play();
  /// ]]>
  /// </example>
  public class DotTween : Base {
    private readonly Tween tween;
    private bool isFinished;
    
    /// <summary>
    /// Wraps a DOTween as a queue item
    /// </summary>
    /// <param name="tween">to be queued</param>
    public DotTween(Tween tween) {
      this.tween = tween;
      tween.Pause();
    }

    /// <summary>
    /// Completes the tween if the execution has already been started
    /// </summary>
    public void Complete() {
      if(tween.IsComplete() || !tween.IsPlaying()) return;
      tween.Complete();
    }

    /// <summary>
    /// Kills the tween and finishes this item
    /// </summary>
    public void Abort() {
      tween.Kill();
      onTweenComplete();
    }

    protected override void Execute() {
      isFinished = false;
      tween.OnComplete(onTweenComplete);
      tween.Play();
    }

    private void onTweenComplete() {
      if (isFinished) return;
      isFinished = true;
      Finished();
    }
  }
}
