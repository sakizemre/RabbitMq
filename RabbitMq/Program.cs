using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMq;


using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.AddSingleton<IConfiguration>(configuration);

                services.AddLogging(); // ILogger<Worker> için
                services.AddSingleton<IConnectionProvider, ConnectionProvider>(); // IConnectionProvider için
                services.AddSingleton<IUrlProcessor, UrlProcessor>(); // IUrlProcessor için
                services.AddHostedService<Worker>(); // Worker servisi için
            })
            .Build();

host.RunAsync();

Console.ReadKey();