using Aplication.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Aplication.Jobs;

public class OrderFilterJob(
    IDataSaverFactory factory,
    IRepository<District> districtRepository,
    IRepository<Order> orderRepository,
    ILogger<OrderFilterJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        JobDataMap datamap = context.JobDetail.JobDataMap;
        var data = (OrderFilterRequest)datamap.Get("data");
        await FilterOrders(data);
        logger.LogInformation("Orders Filtered succesfully");
    }
    private async Task FilterOrders(OrderFilterRequest orderFilter)
    {
        var district = await districtRepository.GetFirstByAsync(x => x.Name == orderFilter.DistrictName);
        if (district == null)
        {
            logger.LogInformation($"District {district.Name} not found");
            return;
        }
        var orders = await orderRepository.GetByAsync(order =>
                           order.DeliveryDistrict.Name == district.Name
                           && order.DeliveryDate > orderFilter.StartTime
                           && order.DeliveryDate < orderFilter.EndTime);
        await SaveFilteredDataAsync(orders, orderFilter.StartTime, orderFilter.EndTime, district);
    }
    private async Task SaveFilteredDataAsync(ICollection<Order> orders, DateTime start, DateTime end, District district)
    {
        var dataSavers = factory.GetDataSavers();
        foreach (var sasver in dataSavers)
        {
            await sasver.SaveAsync(orders, start, end, district);
        }
    }
}
