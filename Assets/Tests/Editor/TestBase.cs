using System;
using NUnit.Framework;
using Neo.Queues;

namespace Tests.Neo.Queues{

  [TestFixture]
  public class TestBase{

    [Test]
    public void AllowsEmptyInitialization(){
      Base q = new Base();
      bool called = false;
      Assert.NotNull(q);
      Assert.False(q.DirectFinishOnPlay);

      q.Play( () => {
        called = true;
      });

      Assert.False(called);
    }

    [Test]
    public void AllowsInitializationWithDirectFinishOnPlay(){
      Base q = new Base(true);
      bool called = false;
      Assert.NotNull(q);
      Assert.True(q.DirectFinishOnPlay);

      q.Play( () => {
        called = true;
      });
      Assert.True(called);
    }

  }
}