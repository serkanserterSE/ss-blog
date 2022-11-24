using SS.Blog.Models.Dtos;

namespace SS.Blog.Services.Abstractions
{
    public interface IBlogLoging
    {
        Task<Guid> RequestLog(ApiLogDto apiLog, CancellationToken cancellationToken);
        Task ResponseLog(ApiLogDto apiLog, Guid keyId, CancellationToken cancellationToken);
        Task ExceptionLog(string exeption, string url, CancellationToken cancellationToken);
    }
}
