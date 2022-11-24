
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Buffers;
using System.Text;

namespace SS.Blog.Middleware.Logging
{
    public static class LogingMiddlewareExtension
    {
        public static IApplicationBuilder UseBlogAppLoging(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<RequestAndResponseLog>();
        }

        public static async Task<string> ReadRequestBodyAsync(this HttpRequest request)
        {
            var bodyAsText = string.Join("", await GetListOfStringsFromStreamMoreEfficient(request.Body));

            return bodyAsText;
        }

        public static async Task<string> ReadResponseBodyAsync(this HttpResponse response)
        {
            var bodyAsText = string.Join("", await GetListOfStringsFromStreamMoreEfficient(response.Body));

            return bodyAsText;
        }

        private static async Task<List<string>> GetListOfStringsFromStreamMoreEfficient(Stream body)
        {
            StringBuilder builder = new StringBuilder();
            byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
            List<string> results = new List<string>();

            while (true)
            {
                var bytesRemaining = await body.ReadAsync(buffer, offset: 0, buffer.Length);

                if (bytesRemaining == 0)
                {
                    results.Add(builder.ToString());
                    break;
                }

                // Instead of adding the entire buffer into the StringBuilder
                // only add the remainder after the last \n in the array.
                var prevIndex = 0;
                int index;
                while (true)
                {
                    index = Array.IndexOf(buffer, (byte)'\n', prevIndex);
                    if (index == -1)
                    {
                        break;
                    }

                    var encodedString = Encoding.UTF8.GetString(buffer, prevIndex, index - prevIndex);

                    if (builder.Length > 0)
                    {
                        // If there was a remainder in the string buffer, include it in the next string.
                        results.Add(builder.Append(encodedString).ToString());
                        builder.Clear();
                    }
                    else
                    {
                        results.Add(encodedString);
                    }

                    // Skip past last \n
                    prevIndex = index + 1;
                }

                var remainingString = Encoding.UTF8.GetString(buffer, prevIndex, bytesRemaining - prevIndex);
                builder.Append(remainingString);
            }

            ArrayPool<byte>.Shared.Return(buffer);

            return results;
        }
    }
}
