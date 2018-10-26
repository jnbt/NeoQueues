using System;
using NUnit.Framework;
using Neo.Collections;
using Neo.Queues;

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

      Assert.IsTrue(calls.IsEmpty);

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

      Assert.IsTrue(calls.IsEmpty);

      t.Play();

      Assert.AreEqual(new List<string>() {
        "one", "two"
      }, calls);
    }
  }
}
