using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace BackgroundJobs.Services;

public class LogService : BackgroundService
{
    private readonly IModel _channel;
    private readonly IHostApplicationLifetime _lifetime;

    public LogService(IHostApplicationLifetime lifetime)
    {
        _lifetime = lifetime;
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
        };
        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(Publish,stoppingToken);
    }

    private void Publish()
    {
        var basicProperties = _channel.CreateBasicProperties();
        basicProperties.Persistent = true;
        var message = Encoding.UTF8.GetBytes("Hello From BackgroundService");
        _channel.BasicPublish(exchange: "backgroundExchange", routingKey: "back.Log", body: message,
            basicProperties: basicProperties);
        _lifetime.StopApplication();
    }
}

