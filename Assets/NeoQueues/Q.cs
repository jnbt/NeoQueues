using System;

namespace Neo.Queues{
  public sealed class Q{

    public static Base Base(bool DirectFinishOnPlay = false){
      return new Base(DirectFinishOnPlay);
    }

    public static Script Script(params IQueueable[] args){
      return new Script(args);
    }

    public static Parallel Parallel(params IQueueable[] args){
      return new Parallel(args);
    }

    public static Delegation Delegation(QueueableDelegate What){
      return new Delegation(What);
    }

    public static Tween Tween(object Target, float Duration, Holoville.HOTween.TweenParms Params){
      return new Tween(Target, Duration, Params);
    }

    public static TweenSequence TweenSequence(Holoville.HOTween.Sequence sequence){
      return new TweenSequence(sequence);
    }

    public static QAnimation Animation(UnityEngine.Animation ani){
      return new QAnimation(ani);
    }

    public static Timeout Timeout(float seconds){
      return new Timeout(seconds);
    }

    public static Yielding Yielding(UnityEngine.YieldInstruction instruction){
      return new Yielding(instruction);
    }
  
    public static Lazy Lazy(Func<IQueueable> generator){
      return new Lazy(generator);
    }

    public static Task Task(QueueTask what) {
      return new Task(what);
    }
  }
}
