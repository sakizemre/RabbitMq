using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq
{
    public class ConnectionProvider : IConnectionProvider, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ConnectionProvider(IConfiguration config)
        {
            // temp mail ile cloudamqp adresinde bir rabbitmq deneme hesabı oluşturuldu
            // istenirse localhost üzerinden istenirse sunucu üzerinden test edilebilir
            // localhost için HostName kısmına localhost yazılabilir, sunucu ile test edilecekse bağlantı bilgileri sabit kalabilir

            // sunucu kullanıcı login bilgileri aşağıdadır
            // https://rat.rmq2.cloudamqp.com/ adresine giderek aşağıdaki bilgiler ile giriş yapabilirsiniz
            // username: ujmuusca
            // password: ih7pE94qPn4v3aTGJm5L_MU4hiRKm2dL
            var factory = new ConnectionFactory()
            {
                HostName = config["RabbitMQ:HostName"],
                UserName = config["RabbitMQ:UserName"],
                Password = config["RabbitMQ:Password"],
                Port = int.TryParse(config["RabbitMQ:Port"], out int parsedPort) ? parsedPort : 5672,
                VirtualHost = config["RabbitMQ:VirtualHost"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public IModel GetChannel()
        {
            return _channel;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
