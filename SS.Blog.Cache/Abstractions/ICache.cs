namespace SS.Blog.Cache.Abstractions
{
    public interface ICache
    {
        Task SetString(string key, string value, CancellationToken cancellationToken);
        Task<string> GetString(string key, CancellationToken cancellationToken);
    }
}
