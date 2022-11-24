using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using SS.Blog.Models.Settings;
using SS.Blog.Queue.Abstractions;
using System.Text;
using Newtonsoft.Json;

namespace SS.Blog.Queue.Concretes
{
    public class RabbitMqConsumer : IConsumer
    {
        public readonly RabbitMqSettings _rabbitMqSettings;

        public RabbitMqConsumer(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public async Task CatchMessage(string topicName, Action<string> operation, CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password
            };
            var connection = factory.CreateConnection();
            //using 
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            channel.QueueDeclare(topicName, exclusive: false);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body)).ToString();
                try
                {
                    operation(message);
                    channel.BasicAck(eventArgs.DeliveryTag, true);
                }
                catch
                {
                    channel.BasicReject(eventArgs.DeliveryTag, true);
                }
            };
            channel.BasicConsume(queue: topicName, autoAck: false, consumer: consumer);
        }
    }
}
