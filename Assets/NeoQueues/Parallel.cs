using Neo.Collections;

namespace Neo.Queues{

  public class Parallel : Base{
    private List<IQueueable> List = new List<IQueueable>();
    private int CountDownLatch;

    public Parallel() : base(){
    }

    public Parallel(IQueueable q, params IQueueable[] args) : base(){
      List.Add(q);
      List.AddRange(args);
    }

    public Parallel(IQueueable[] args) : base(){
      List.AddRange(args);
    }

    public void Add(IQueueable q){
      List.Add(q);
    }

    public void Prepend(IQueueable q){
      List.Insert(0, q);
    }

    protected override void Execute(){
      if(List.Count == 0) {
        Finished();
      } else{
        CountDownLatch = 0;
        for(int i=0,imax = List.Count; i<imax; i++) List[i].Play(Done);
      }
    }

    private void Done(){
      CountDownLatch++;
      if(CountDownLatch == List.Count){
        Finished();
      }
    }

  }
}