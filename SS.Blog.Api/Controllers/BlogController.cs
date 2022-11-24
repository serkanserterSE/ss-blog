using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SS.Blog.Cache.Abstractions;
using SS.Blog.Queue.Abstractions;

namespace SS.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IProducer _producer;
        private readonly IConsumer _consumer;
        private readonly ICache _cache;

        public BlogController(IProducer producer, IConsumer consumer, ICache cache)
        {
            _producer = producer;
            _consumer = consumer;
            _cache = cache;

            StartConsumer();
        }

        [HttpGet("TestProducer")]
        public async Task<IActionResult> TestProducer(int val)
        {
            //throw new Exception("TEST exception");
            await _producer.SendMessage(val.ToString(), "TEST", CancellationToken.None);
            return Ok();
        }

        private async void StartConsumer()
        {
            await _consumer.CatchMessage("TEST", async (message) =>
            {
                int count = Convert.ToInt32(message);
                var cacheCount = await _cache.GetString("test", CancellationToken.None);
                count = count + Convert.ToInt32(cacheCount);
                await _cache.SetString("test", count.ToString(), CancellationToken.None);
            }, CancellationToken.None);
        }
    }
}
