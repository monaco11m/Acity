using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification.Application.Dtos;
using Notification.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Application.Services
{
    public class RabbitNotificationConsumer : BackgroundService, INotificationConsumer
    {
        private readonly IConfiguration _config;
        private readonly IServiceScopeFactory _scopeFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _queue;

        public RabbitNotificationConsumer(
            IConfiguration config,
            IServiceScopeFactory scopeFactory
        )
        {
            _config = config;
            _scopeFactory = scopeFactory;

            _queue = _config["RabbitMQ:QueueFinished"];

            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:Host"],
                UserName = _config["RabbitMQ:User"],
                Password = _config["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: _queue,
                durable: true,
                exclusive: false,
                autoDelete: false
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (_, ea) =>
            {
                using var scope = _scopeFactory.CreateScope();

                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var httpFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<CargaFinalizadaMessage>(json);

                if (message == null)
                    return;

                await emailService.SendEmailAsync(
                    message.Usuario,
                    "Carga finalizada",
                    $"La carga #{message.CargaId} ha terminado correctamente."
                );

                var http = httpFactory.CreateClient();

                var controlUrl = $"{config["ControlService:BaseUrl"]}/api/cargaarchivo/actualizar";

                await http.PostAsJsonAsync(controlUrl, new
                {
                    id = message.CargaId,
                    estado = 3
                });
            };

            _channel.BasicConsume(
                queue: _queue,
                autoAck: true,
                consumer: consumer
            );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
