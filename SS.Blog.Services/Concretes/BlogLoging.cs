using SS.Blog.DataAccesses.Contexts;
using SS.Blog.DataAccesses.Entities.LogEntities;
using SS.Blog.DataAccesses.Enums;
using SS.Blog.Models.Dtos;
using SS.Blog.Services.Abstractions;

namespace SS.Blog.Services.Concretes
{
    public class BlogLoging : IBlogLoging
    {
        private readonly LogDbContext _logDbContext;

        public BlogLoging(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
        }

        public async Task ExceptionLog(string exeption, string url, CancellationToken cancellationToken)
        {
            var keyId = Guid.NewGuid();
            var newEx = new ExceptionLog()
            {
                Date = DateTime.Now,
                ExceptionMessage = exeption,
                KeyId = keyId,
                Url = url,
            };
            _logDbContext.ExceptionLog.Add(newEx);
            await _logDbContext.SaveChangesAsync();
        }

        public async Task<Guid> RequestLog(ApiLogDto apiLog, CancellationToken cancellationToken)
        {
            var keyId = Guid.NewGuid();
            var newReqLog = new ApiLog()
            {
                Content = apiLog.Content,
                Date = apiLog.Date,
                MethodType = EMethod.Request,
                MethodTypeName = "Request",
                RequestHttpType = apiLog.RequestHttpType,
                RequestUrl = apiLog.RequestUrl,
                KeyId = keyId
            };

            _logDbContext.ApiLog.Add(newReqLog);
            await _logDbContext.SaveChangesAsync();
            return keyId;
        }

        public async Task ResponseLog(ApiLogDto apiLog, Guid keyId, CancellationToken cancellationToken)
        {
            var newLog = new ApiLog()
            {
                Content = apiLog.Content,
                Date = apiLog.Date,
                MethodType = EMethod.Response,
                MethodTypeName = "Response",
                RequestHttpType = apiLog.RequestHttpType,
                RequestUrl = apiLog.RequestUrl,
                KeyId = keyId
            };

            _logDbContext.ApiLog.Add(newLog);
            await _logDbContext.SaveChangesAsync();
        }
    }
}
