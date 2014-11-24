using System;

namespace Neo.Queues {
  public delegate void QueueTask(QueueCallback done);

  public class Task : IQueueable {
    private QueueTask what;
    public Task(QueueTask what) {
      this.what = what;
    }

    public void Play(QueueCallback done) {
      what(done);
    }

    public void Play() {
      what(emptyDone);
    }

    private void emptyDone() {
      //nothing to do here
    }
  }
}
