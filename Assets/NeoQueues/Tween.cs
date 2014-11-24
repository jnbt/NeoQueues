using Holoville.HOTween;

namespace Neo.Queues{

  public class Tween : Base{
    private object Target;
    private float Duration;
    private TweenParms Params;
    private Tweener Tweener;

    public Tween(object Target, float Duration, TweenParms Params) : base(){
      this.Target   = Target;
      this.Duration = Duration;
      this.Params   = Params;
      this.Params.OnComplete(Finished);
    }

    protected override void Execute(){
      Tweener = HOTween.To(Target, Duration, Params);
    }

    public void Complete(){
      if(Tweener != null) Tweener.Complete();
    }

    public void Abort(){
      if(Tweener != null) Tweener.Kill();
      Finished();
    }
  }
}
