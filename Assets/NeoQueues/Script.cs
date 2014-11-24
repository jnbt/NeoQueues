using System;
using Neo.Collections;

namespace Neo.Queues{

  public class Script : Base{
    private List<IQueueable> List = new List<IQueueable>();
    private int CurrentIndex;

    public Script() : base(){
    }

    public Script(IQueueable q, params IQueueable[] args) : base(){
      List.Add(q);
      List.AddRange(args);
    }

    public Script(IQueueable[] args) : base(){
      List.AddRange(args);
    }

    public void Add(IQueueable q){
      List.Add(q);
    }

    public void Prepend(IQueueable q){
      List.Insert(0, q);
    }

    protected override void Execute(){
      CurrentIndex = -1;
      Proceed();
    }

    private void Proceed(){
      CurrentIndex++;
      if(CurrentIndex >= List.Count){
        Finished();
      } else{
        IQueueable Next = List[CurrentIndex];
        Next.Play(Proceed);
      }
    }

  }
}