using Aplication.DTO;
using Aplication.Jobs;
using Quartz;

namespace Aplication.Services;

public class OrderScheduler(ISchedulerFactory factory)
{
    public async Task ScheduleOrderFilterJob(OrderFilterRequest filterRequestData)
    {
        var jobId = Guid.NewGuid().ToString();
        var job = JobBuilder.Create<OrderFilterJob>()
            .WithIdentity(jobId)
            .UsingJobData(new() { { "data", filterRequestData } })
            .Build();

        var trigger = TriggerBuilder.Create()
            .StartNow()
            .Build();
        var scheduler = await factory.GetScheduler();
        await scheduler.Start();
        await scheduler.ScheduleJob(job, trigger);
    }
}
