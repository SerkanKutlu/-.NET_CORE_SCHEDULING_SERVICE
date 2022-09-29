using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace BackgroundServices.Services;

public class LogService : BackgroundService
{
    private IModel channel;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
        };
        var connection = connectionFactory.CreateConnection();
        channel = connection.CreateModel();
        await Task.Run(Publish,stoppingToken);
    }

    private void Publish()
    {
        var basicProperties = channel.CreateBasicProperties();
        basicProperties.Persistent = true;
        var message = Encoding.UTF8.GetBytes("Hello From BackgroundService");
        channel.BasicPublish(exchange: "backgroundExchange", routingKey: "back.Log", body: message,
            basicProperties: basicProperties);
    }
}

