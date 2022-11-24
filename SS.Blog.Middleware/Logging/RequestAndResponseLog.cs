using Microsoft.AspNetCore.Http;
using SS.Blog.Services.Abstractions;
using System.Text.Json;

namespace SS.Blog.Middleware.Logging
{
    public class RequestAndResponseLog
    {
        private readonly RequestDelegate _next;
        public RequestAndResponseLog(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IBlogLoging logService)
        {
            var keyId = await logService.RequestLog(new Models.Dtos.ApiLogDto()
            {
                //Content = await context.Request.ReadRequestBodyAsync(),
                Content = "",
                Date = DateTime.Now,
                RequestHttpType = context.Request.Method,
                RequestUrl = context.Request.Path
            }, CancellationToken.None);

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await logService.ExceptionLog(e.Message, context.Request.Path, CancellationToken.None);
                var response = context.Response;
                response.ContentType = "application/json";
                await response.WriteAsync(JsonSerializer.Serialize(new { Result = "Sistem Hatası!" }));
            }

            await logService.ResponseLog(new Models.Dtos.ApiLogDto()
            {
                //Content = await context.Response.ReadResponseBodyAsync(),
                Content = "",
                Date = DateTime.Now,
                RequestHttpType = context.Request.Method,
                RequestUrl = context.Request.Path
            }, keyId, CancellationToken.None);

        }
    }
}
