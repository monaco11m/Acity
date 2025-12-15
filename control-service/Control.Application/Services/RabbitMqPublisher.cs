using Control.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Control.Application.Services
{
    public class RabbitMqPublisher : IRabbitPublisher
    {
        private readonly IConfiguration _config;

        public RabbitMqPublisher(IConfiguration config)
        {
            _config = config;
        }

        public Task PublishAsync(object message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:Host"],
                Port = int.Parse(_config["RabbitMQ:Port"]),
                UserName = _config["RabbitMQ:UserName"],
                Password = _config["RabbitMQ:Password"]
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            string exchange = _config["RabbitMQ:Exchange"];
            string queue = _config["RabbitMQ:Queue"];
            string routingKey = _config["RabbitMQ:RoutingKey"];

            channel.ExchangeDeclare(exchange, ExchangeType.Direct, durable: true);

            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            channel.QueueBind(queue, exchange, routingKey);

            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: body
            );

            return Task.CompletedTask;
        }
    }
}
