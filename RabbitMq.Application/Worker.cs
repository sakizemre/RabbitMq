using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RabbitMq
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnectionProvider _connectionProvider;
        private readonly IUrlProcessor _urlProcessor;

        public Worker(ILogger<Worker> logger, IConnectionProvider connectionProvider, IUrlProcessor urlProcessor)
        {
            _logger = logger;
            _connectionProvider = connectionProvider;
            _urlProcessor = urlProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connectionProvider.GetChannel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await _urlProcessor.ProcessUrl(message);
            };
            channel.BasicConsume(queue: "urls",
                                 autoAck: true,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
