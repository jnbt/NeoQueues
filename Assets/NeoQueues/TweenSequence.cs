using Holoville.HOTween;

namespace Neo.Queues{

  public class TweenSequence : Base{
    private Sequence sequence;
    private bool running = false;

    public TweenSequence(Sequence sequence) : base(){
      this.sequence = sequence;
      this.sequence.AppendCallback(Finished);
    }

    protected override void Execute(){
      running = true;
      this.sequence.Play();
    }

    public void Complete(){
      if(running) sequence.Complete();
    }

    public void Abort(){
      if(running) sequence.Kill();
      Finished();
    }

  }
}