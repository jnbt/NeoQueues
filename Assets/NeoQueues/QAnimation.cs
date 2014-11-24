using System;
using System.Collections;
using Neo.Async;

namespace Neo.Queues{

  public class QAnimation : Base{
    private UnityEngine.Animation Ani;

    public QAnimation(UnityEngine.Animation Ani) : base(){
      this.Ani = Ani;
    }

    protected override void Execute(){
      Ani.Play();
      Async.CoroutineStarter.Instance.Add(waitForAnimation(Ani));
    }

    public void Abort(){
      Ani.Stop();
      Finished();
    }

    private IEnumerator waitForAnimation(UnityEngine.Animation ani){
      do{
        yield return null;
      } while(ani.isPlaying);
      Finished();
    }


  }
}