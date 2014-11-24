namespace Neo.Queues{

  public class Base : IQueueable{
    public QueueCallback Callback{get; private set;}
    public bool DirectFinishOnPlay{get; protected set;}

    public Base(bool DirectFinishOnPlay = false){
      this.DirectFinishOnPlay = DirectFinishOnPlay;
    }

    public virtual void Play(QueueCallback Callback){
      this.Callback = Callback;
      Execute();
    }

    public virtual void Play(){
      this.Play(null);
    }

    protected virtual void Execute(){
      if(DirectFinishOnPlay) Finished();
    }

    public void Finished(){
      if(Callback != null) Callback();
    }
  }
}