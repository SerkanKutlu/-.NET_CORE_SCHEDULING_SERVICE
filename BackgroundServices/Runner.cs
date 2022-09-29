using BackgroundServices.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackgroundServices;

public static class Runner
{
    public static async Task RunEmailService()
    {
        var emailServiceBuilder = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHostedService<EmailService>();
            });

        await emailServiceBuilder.RunConsoleAsync();
    }

    public static async Task RunLogService()
    {
        var logServiceBuilder = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddHostedService<LogService>();
            });

        await logServiceBuilder.RunConsoleAsync();
    }
}