using NUnit.Framework;
using Neo.Queues;
using System.Collections.Generic;

namespace Tests.Neo.Queues {
  [TestFixture]
  public class TestTask {
    [Test]
    public void InvokesTaskWithCallback() {
      List<string> calls = new List<string>();

      Task t = new Task((done) => {
        calls.Add("one");
        done();
        calls.Add("two");
      });

      Assert.IsTrue(calls.Count == 0);

      t.Play(() => {
        calls.Add("play");
      });

      Assert.AreEqual(new List<string>() {
        "one", "play", "two"
      }, calls);
    }

    [Test]
    public void InvokesTaskWithoutCallback() {
      List<string> calls = new List<string>();

      Task t = new Task((done) => {
        calls.Add("one");
        done();
        calls.Add("two");
      });

      Assert.IsTrue(calls.Count == 0);

      t.Play();

      Assert.AreEqual(new List<string>() {
        "one", "two"
      }, calls);
    }
  }
}
