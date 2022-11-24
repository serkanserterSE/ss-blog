using System;

namespace SS.Blog.Queue.Abstractions
{
    public interface IConsumer
    {
        Task CatchMessage(string topicName, Action<string> operation, CancellationToken cancellationToken);
    }
}
