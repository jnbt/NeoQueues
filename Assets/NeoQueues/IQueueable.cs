namespace Neo.Queues{
  public delegate void QueueCallback();

  public interface IQueueable{
    void Play(QueueCallback Callback);
    void Play();
  }
}