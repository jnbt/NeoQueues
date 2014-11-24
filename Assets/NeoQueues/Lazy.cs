using System;

namespace Neo.Queues{
  public sealed class Lazy : IQueueable{
    private Func<IQueueable> generator;

    public Lazy(Func<IQueueable> generator){
      this.generator = generator;
    }

    public void Play(QueueCallback callback){
      IQueueable q = generator();
      q.Play(callback);
    }

    public void Play(){
      IQueueable q = generator();
      q.Play();
    }
  }
}
