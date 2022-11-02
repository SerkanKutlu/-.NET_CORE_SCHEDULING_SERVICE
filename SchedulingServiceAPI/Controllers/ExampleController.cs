using BackgroundJobs;
using BackgroundJobs.Enums;
using Microsoft.AspNetCore.Mvc;
namespace SchedulingServiceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Example()
    {
        var scheduler = new Scheduler(ServiceType.Recurring,recurFrequency:RecurFrequency.Hourly);
        await scheduler.StartEmailService();
        //var sc = new Scheduler(serviceType:ServiceType.FireAndForget,exactTime:)//Any date can be entered here.
        var scheduler2 = new Scheduler(ServiceType.Recurring,recurFrequency:RecurFrequency.Monthly);
        await scheduler2.StartLogService();
        return Ok("dummy result");
    }
    
}