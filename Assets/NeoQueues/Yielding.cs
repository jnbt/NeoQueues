using System.Collections;
using UnityEngine;

namespace Neo.Queues{
  public class Yielding : Base{
    private YieldInstruction instruction;

    public Yielding(YieldInstruction instruction){
      this.instruction = instruction;
    }

    protected override void Execute(){
      Async.CoroutineStarter.Instance.Add(yielding());
    }

    private IEnumerator yielding(){
      yield return instruction;
      Finished();
    }
  }
}