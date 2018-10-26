using UnityEngine;
using NUnit.Framework;
using Neo.Queues;
using UnityEngine.TestTools;
using System.Collections;
using DG.Tweening;

namespace Tests.Neo.Queues {
  [TestFixture]
  public class TestTween {
    [UnityTest]
    public IEnumerator WaitsForTween() {
      bool finished = false;
      GameObject go = new GameObject("TestObject");
      go.transform.position = Vector3.zero;
      var tween = Q.Tween(go.transform.DOMove(Vector3.one, 2f));
      Assert.AreEqual(Vector3.zero, go.transform.position);
      Assert.IsFalse(finished);
      yield return null;
      Assert.AreEqual(Vector3.zero, go.transform.position);
      Assert.IsFalse(finished);
      tween.Play(() => finished = true);
      Assert.IsFalse(finished);
      yield return new WaitForSeconds(2.1f); // wait a little bit longer then the tween
      Assert.AreEqual(Vector3.one, go.transform.position);
      Assert.IsTrue(finished);
    }
  }
}