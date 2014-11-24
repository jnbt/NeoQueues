using System;
using NUnit.Framework;
using Neo.Queues;

namespace Tests.Neo.Queues{

  [TestFixture]
  public class TestDelegation{

    [Test]
    public void CallesDelegation(){
      bool called = false;
      bool delegateCalled = false;

      Delegation d = new Delegation(() => {
        delegateCalled = true;
      });

      d.Play(() => {
        called = true;
      });

      Assert.True(called);
      Assert.True(delegateCalled);
    }
  }
}
