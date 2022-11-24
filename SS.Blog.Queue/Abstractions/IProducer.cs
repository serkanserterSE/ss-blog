namespace SS.Blog.Queue.Abstractions
{
    public interface IProducer
    {
        Task SendMessage(string message, string topicName, CancellationToken cancellationToken);
    }
}
