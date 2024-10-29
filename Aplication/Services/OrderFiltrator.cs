using Domain.Entities;
using Domain.Interfaces;
using Mapster;
using Domain.ValueObjects;
using Aplication.DTO;

namespace Aplication.Services;

public class OrderFiltrator(IDataSaverFactory factory, IRepository<District> districtRepository, IRepository<Order> orderRepository)
{
    private readonly IDataSaverFactory _dataSaverFactory = factory;
    private readonly IRepository<District> _districtRepository = districtRepository;
    private readonly IRepository<Order> _orderRepository = orderRepository;
    public async Task<OrderFilterRequestResult> FilterOrders(OrderFilterRequest requestData)
    {
        var district = await _districtRepository.GetFirstByAsync(x => x.Name == requestData.DistrictName) ?? throw new ArgumentException("District not found");
        var orders = await _orderRepository.GetByAsync(x => 
                           x.DeliveryDistrict.Name == district.Name 
                           && x.DeliveryDate > requestData.StartTime
                           && x.DeliveryDate < requestData.EndTime);
        var result = new OrderFilterRequestResult() { StartTime = requestData.StartTime, EndTime = requestData.EndTime, Orders = orders.Adapt<List<OrderData>>(), District = district };
        await SaveFilteredDataAsync(orders, requestData.StartTime, requestData.EndTime, district);
        return result;
    }
    private async Task SaveFilteredDataAsync(ICollection<Order> orders, DateTime start, DateTime end, District district)
    {
        var factories = _dataSaverFactory.GetDataSavers();
        foreach (var item in factories)
        {
            await item.SaveAsync(orders, start, end, district);
        }
    }
}

