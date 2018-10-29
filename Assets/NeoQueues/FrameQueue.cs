using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neo.Queues {
  /// <summary>
  /// A frame queue allows the process of items distributed over some
  /// rendered frames. An initial boost can be set to process n items when the execution starts.
  /// </summary>
  /// <example><![CDATA[
  /// FrameQueue<string> frames = new FrameQueue<string>(someUids, 5, (uid) => {
  ///   someHeavyOperationPerUid(uid);
  /// });
  /// frames.Play();
  /// ]]></example>
  /// <typeparam name="T">The item type</typeparam>
  public class FrameQueue<T> : Base {
    private readonly List<T> items;
    private readonly Action<T, int> block;
    private int i;
    private readonly int imax;
    private readonly int initialBoost;

    /// <summary>
    /// Constructs a FrameQueue for the given list of items
    /// </summary>
    /// <param name="items">to be processed</param>
    /// <param name="initialBoost">number of items to process directly on exection</param>
    /// <param name="block">the processor per item</param>
    public FrameQueue(List<T> items, int initialBoost, Action<T, int> block)
      : base() {
      this.items = items; 
      this.block = block;
      i = 0; 
      imax = items.Count; 
      this.initialBoost = Mathf.Min(initialBoost, imax);
    }

    /// <summary>
    /// Constructs a FrameQueue for the given list of items without any initial boost
    /// </summary>
    /// <param name="items">to be processed</param>
    /// <param name="block">the processor per item</param>
    public FrameQueue(List<T> items, Action<T, int> block)
      : this(items, 0, block) {
    }

    /// <inheritdoc />
    protected override void Execute() {
      while(i < initialBoost) perform();
      if(i < imax) nextItem();
      else Finished();
    }

    private void nextItem() {
      Async.CoroutineStarter.Instance.Add(processNextItem());
    }

    private IEnumerator deferedNextItem() {
      yield return new WaitForEndOfFrame();
      nextItem();
    }

    private IEnumerator processNextItem() {
      yield return new WaitForEndOfFrame();
      perform();
      if(i < imax) Async.CoroutineStarter.Instance.Add(deferedNextItem());
      else Finished();
    }

    private void perform() {
      block(items[i], i);
      i++;
    }
  }
}
