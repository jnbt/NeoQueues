using System;
using NUnit.Framework;
using Neo.Queues;

namespace Tests.Neo.Queues{

  [TestFixture]
  public class TestParallel{

    private class DummyQ : IQueueable {
      public bool Played {get; set;}
      public bool Continue{get; set;}

      public DummyQ(bool Continue = true){
        this.Continue = Continue;
        Played = false;
      }

      public void Play(QueueCallback Callback){
        Played = true;
        if(Continue) Callback();
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

      Parallel q = new Parallel(items[0], items[1]);
      bool called = false;

      q.Play(() => {
        called = true;
      });

      foreach(DummyQ item in items){
        Assert.True(item.Played);
      }

      Assert.True(called);
    }

    [Test]
    public void CallesAllItemsAsArray(){
      DummyQ[] items = new DummyQ[]{
        new DummyQ(), new DummyQ()
      };

      Parallel q = new Parallel(items);
      bool called = false;

      q.Play(() => {
        called = true;
      });

      foreach(DummyQ item in items){
        Assert.True(item.Played);
      }

      Assert.True(called);
    }

    [Test]
    public void WaitsForAllItems(){
      DummyQ[] items = new DummyQ[]{
        new DummyQ(false), new DummyQ()
      };

      Parallel q = new Parallel(items[0], items[1]);
      bool called = false;

      q.Play(() => {
        called = true;
      });

      foreach(DummyQ item in items){
        Assert.True(item.Played);
      }

      Assert.False(called);
    }
  }
}