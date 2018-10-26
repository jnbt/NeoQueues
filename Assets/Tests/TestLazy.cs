using System;
using NUnit.Framework;
using Neo.Queues;

namespace Tests.Neo.Queues {

  [TestFixture]
  public class TestLazy {

    [Test]
    public void PlayWithCallback(){
      bool generatorCalled = false;
      bool lazyCalled = false;

      Func<IQueueable> generator = () => {
        generatorCalled = true;
        return new Base(true);
      };
    
      Assert.IsFalse(generatorCalled);
      Assert.IsFalse(lazyCalled);
      Lazy lazy = new Lazy(generator);
      lazy.Play(() => lazyCalled = true);
    }

    [Test]
    public void PlayWithoutCallback() {
      bool generatorCalled = false;

      Func<IQueueable> generator = () => {
        generatorCalled = true;
        return new Base(true);
      };

      Assert.IsFalse(generatorCalled);
      Lazy lazy = new Lazy(generator);
      lazy.Play();
    }
  }
}