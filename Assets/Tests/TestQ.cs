using System;
using NUnit.Framework;
using Neo.Queues;

namespace Tests.Neo.Queues{

  [TestFixture]
  public class TestQ{

    private class DummyQ : IQueueable {
      public bool Played {get; set;}

      public DummyQ(){
        Played = false;
      }

      public void Play(QueueCallback Callback){
        Played = true;
        Callback();
      }

      public void Play(){
        //not in use
      }
    }

    [Test]
    public void ReturnsBase(){
      Base q = Q.Base();
      Assert.NotNull(q);

      Base q2 = Q.Base(true);
      Assert.NotNull(q2);
    }

    [Test]
    public void ReturnsScript(){
     Script q = Q.Script(new DummyQ(), new DummyQ());
     Assert.NotNull(q);
    }

    [Test]
    public void ReturnsParallel(){
     Parallel q = Q.Parallel(new DummyQ(), new DummyQ());
     Assert.NotNull(q);
    }

    [Test]
    public void ReturnsDelegation(){
     Delegation q = Q.Delegation(() => {

     });
     Assert.NotNull(q);
    }

    [Test]
    public void ReturnsLazy(){
      Lazy l = Q.Lazy(() => new Base(true));
      Assert.NotNull(l);
    }

    [Test]
    public void ReturnsTask() {
      Task t = Q.Task((done) => done());
      Assert.NotNull(t);
    }
  }
}
