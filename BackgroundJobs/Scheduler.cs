using BackgroundJobs.Enums;
using BackgroundJobs.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackgroundJobs;

public class Scheduler
{
    private ServiceType _serviceType;
    private TimeSpan? _delayTime;
    private RecurFrequency? _recurFrequency;
    private DateTime? _exactTime;
    private readonly DateTime? _executionTime = null;
    private bool _isRecuring = false;
    private readonly TimeSpan _period = TimeSpan.Zero;
    public Scheduler(ServiceType serviceType, TimeSpan? delayTime = null, RecurFrequency? recurFrequency = null, DateTime? exactTime = null)
    {
        _serviceType = serviceType;
        _delayTime = delayTime;
        _recurFrequency = recurFrequency;
        _exactTime = exactTime;
        switch (serviceType)
        {
            case ServiceType.FireAndForget:
                _executionTime = exactTime ?? DateTime.UtcNow.AddSeconds(5);
                _isRecuring = false;
                _period = TimeSpan.Zero;
                break;
            case ServiceType.Delayed:
                delayTime ??= TimeSpan.FromMilliseconds(1000);
                _executionTime = DateTime.UtcNow.AddMilliseconds(delayTime.Value.Milliseconds);
                _isRecuring = false;
                _period = TimeSpan.Zero;
                break;
            case ServiceType.Recurring:
                _isRecuring = true;
                _executionTime = exactTime ?? DateTime.UtcNow.AddSeconds(5);
                switch (recurFrequency)
                {
                    case RecurFrequency.Daily:
                        _period = TimeSpan.FromDays(1);
                        break;
                    case RecurFrequency.Hourly:
                        _period = TimeSpan.FromMinutes(2);
                        break;
                    case RecurFrequency.Monthly:
                        _period = TimeSpan.FromSeconds(10);
                        break;
                    case RecurFrequency.Weekly:
                        _period = TimeSpan.FromDays(7);
                        break;
                }
                break;
                
        }
        
    }
    
    public async Task StartEmailService()
    {
        var difference =_executionTime- DateTime.UtcNow;
        if (difference != null)
        {
            Console.WriteLine(difference.Value);
            Console.WriteLine(difference.Value.TotalMilliseconds);
            var timer = new Timer(async o =>
            {
                await Runner.RunEmailService();
            }, null,(long)difference.Value.TotalMilliseconds , (long)_period.TotalMilliseconds);
        }
    }

    public async Task StartLogService()
    {
        var difference =_executionTime- DateTime.UtcNow;
        Console.WriteLine();
        if (difference != null)
        {
            var timer = new Timer(async o =>
            {
                await Runner.RunLogService();
            }, null,(long)difference.Value.TotalMilliseconds , (long)_period.TotalMilliseconds);
        }
    }
    
}
