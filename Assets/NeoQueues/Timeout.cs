using System.Collections;
using UnityEngine;

namespace Neo.Queues{

  public sealed class Timeout : Base{
    private float seconds;

    public Timeout(float seconds){
      this.seconds = seconds;
    }

    protected override void Execute(){
      Async.CoroutineStarter.Instance.Add(yielding());
    }

    private IEnumerator yielding(){
      yield return new WaitForSeconds(seconds);
      Finished();
    }
  }
}