using Aplication.DTO;
using Aplication.Jobs;
using Quartz;

namespace Aplication.Services;

public class OrderScheduler(ISchedulerFactory factory)
{
    private readonly ISchedulerFactory _factory = factory;
    public async Task ScheduleOrderFilterJob(OrderFilterRequest filterRequestData)
    {
        var jobId = Guid.NewGuid().ToString();
        IJobDetail job = JobBuilder.Create<OrderFilterJob>()
            .WithIdentity(jobId)
            .UsingJobData(new() { { "data", filterRequestData } })
            .Build();

        ITrigger trigger = TriggerBuilder.Create()
            .StartNow()
            .Build();
        var scheduler = await _factory.GetScheduler();
        await scheduler.ScheduleJob(job, trigger);
    }
}
