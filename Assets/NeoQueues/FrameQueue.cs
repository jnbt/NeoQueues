using System;
using System.Collections;
using Neo.Collections;
using UnityEngine;

namespace Neo.Queues{

  public class FrameQueue<T> : Base{
    private List<T>       items;
    private Action<T,int> block;
    private int i, imax, initialBoost;

    public FrameQueue(List<T> items, int initialBoost, Action<T,int> block) : base(){
      this.items = items; this.block = block;
      i = 0; imax = items.Count; this.initialBoost = Mathf.Min(initialBoost, imax);
    }

    public FrameQueue(List<T> items, Action<T,int> block) : this(items, 0, block){
    }

    protected override void Execute(){
      while(i < initialBoost) perform();
      if(i < imax) nextItem();
      else Finished();
    }

    private void nextItem(){
      Async.CoroutineStarter.Instance.Add(processNextItem());
    }

    private IEnumerator deferedNextItem(){
      yield return new WaitForEndOfFrame();
      nextItem();
    }

    private IEnumerator processNextItem(){
      yield return new WaitForEndOfFrame();
      perform();
      if(i < imax) Async.CoroutineStarter.Instance.Add(deferedNextItem());
      else Finished();
    }

    private void perform(){
      block(items[i], i);
      i++;
    }
  }
}