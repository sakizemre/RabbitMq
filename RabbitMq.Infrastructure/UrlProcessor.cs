using Microsoft.Extensions.Logging;
using RabbitMq.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMq
{
    public class UrlProcessor : IUrlProcessor
    {
        private readonly ILogger<UrlProcessor> _logger;

        public UrlProcessor(ILogger<UrlProcessor> logger)
        {
            _logger = logger;
        }

        public async Task ProcessUrl(string url)
        {
            try
            {
                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Head, url);
                var response = await client.SendAsync(request);
                var log = new LogModel
                {
                    ServiceName = "RabbitListener",
                    Url = url,
                    StatusCode = (int)response.StatusCode
                };
                _logger.LogInformation(JsonSerializer.Serialize(log)); // console ekranına log yazdırır
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
        }
    }
}
