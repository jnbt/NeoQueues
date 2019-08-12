using System;
using NUnit.Framework;
using Neo.Queues;

namespace Tests.Neo.Queues{

  [TestFixture]
  public class TestScript{

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
    public void CallesAllItems(){
      DummyQ[] items = new DummyQ[]{
        new DummyQ(), new DummyQ()
      };

      Script q = new Script(items[0], items[1]);
      bool called = false;

      q.Play(() => {
        called = true;
      });

      Assert.True(called);
      foreach(DummyQ item in items){
        Assert.True(item.Played);
      }
    }

    [Test]
    public void CallesAllItemsAsArray(){
      DummyQ[] items = new DummyQ[]{
        new DummyQ(), new DummyQ()
      };

      Script q = new Script(items);
      bool called = false;

      q.Play(() => {
        called = true;
      });

      Assert.True(called);
      foreach(DummyQ item in items){
        Assert.True(item.Played);
      }
    }


  }
}