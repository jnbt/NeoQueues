using System;

namespace Neo.Queues{
  /// <summary>
  /// A lazy queue item is an item which execution isn't fixed at the 
  /// time of creation, but at the time of execution.
  /// When this item is executed first the generate is called to retrieve
  /// the actual IQueuable which then will be executed.
  /// This way this class can also be seen as a kind of proxy object to a 
  /// real IQueueable which will be determined on execution time.
  /// </summary>
  /// <example><![CDATA[
  /// Lazy l = new Lazy(() => {
  ///   return buildComplexOtherQueueable();
  /// });
  /// l.Play();
  /// ]]></example>
  public sealed class Lazy : IQueueable{
    private Func<IQueueable> generator;

    /// <summary>
    /// Construct a lazy queuable bound to a generate which will be used
    /// at execution time to get the "real" IQueueable to execute
    /// </summary>
    /// <param name="generator">to get the "real" IQueuable</param>
    public Lazy(Func<IQueueable> generator){
      this.generator = generator;
    }

    /// <summary>
    /// Genereate the "real" IQueuable and play it by sending the callback
    /// </summary>
    /// <param name="callback">to be called by the "real" IQueuable</param>
    public void Play(QueueCallback callback){
      IQueueable q = generator();
      q.Play(callback);
    }

    /// <summary>
    /// Calls the "real" IQueuable without any callback
    /// </summary>
    public void Play(){
      IQueueable q = generator();
      q.Play();
    }
  }
}
