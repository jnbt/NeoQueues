namespace Neo.Queues{
  public delegate void QueueableDelegate();

  public class Delegation : Base{
    private QueueableDelegate What;

    public Delegation(QueueableDelegate What) : base(){
      this.What = What;
    }

    protected override void Execute(){
      What();
      Finished();
    }

  }
}