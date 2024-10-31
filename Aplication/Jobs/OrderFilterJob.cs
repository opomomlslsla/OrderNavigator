using Aplication.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Mapster;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Aplication.Jobs;

public class OrderFilterJob(
    IDataSaverFactory factory,
    IRepository<District> districtRepository,
    IRepository<Order> orderRepository,
    ILogger<OrderFilterJob> logger) : IJob
{
    private readonly IDataSaverFactory _dataSaverFactory = factory;
    private readonly IRepository<District> _districtRepository = districtRepository;
    private readonly IRepository<Order> _orderRepository = orderRepository;
    private readonly ILogger<OrderFilterJob> _logger = logger;
    public async Task FilterOrders(OrderFilterRequest requestData)
    {
        var district = await _districtRepository.GetFirstByAsync(x => x.Name == requestData.DistrictName);
        if (district == null)
        {
            _logger.LogInformation("District not found");
            return;
        }
        var orders = await _orderRepository.GetByAsync(x =>
                           x.DeliveryDistrict.Name == district.Name
                           && x.DeliveryDate > requestData.StartTime
                           && x.DeliveryDate < requestData.EndTime);
        await SaveFilteredDataAsync(orders, requestData.StartTime, requestData.EndTime, district);
    }
    private async Task SaveFilteredDataAsync(ICollection<Order> orders, DateTime start, DateTime end, District district)
    {
        var factories = _dataSaverFactory.GetDataSavers();
        foreach (var item in factories)
        {
            await item.SaveAsync(orders, start, end, district);
        }
    }
    public async Task Execute(IJobExecutionContext context)
    {
        JobDataMap datamap = context.JobDetail.JobDataMap;
        var data = (OrderFilterRequest) datamap.Get("data");
        await FilterOrders(data);
    }
}
