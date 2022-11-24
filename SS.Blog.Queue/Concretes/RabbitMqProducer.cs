using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SS.Blog.Models.Settings;
using SS.Blog.Queue.Abstractions;
using System.Text;

namespace SS.Blog.Queue.Concretes
{
    public class RabbitMqProducer : IProducer
    {
        public readonly RabbitMqSettings _rabbitMqSettings;

        public RabbitMqProducer(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public async Task SendMessage(string message, string topicName, CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password
            };
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            channel.QueueDeclare(topicName, exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: topicName, body: body);
        }
    }
}
