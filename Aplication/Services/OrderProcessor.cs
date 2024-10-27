using Aplication.Services.Options;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Text;
using Mapster;
using Domain.ValueObjects;
using Aplication.DTO;


namespace Aplication.Services;


public class OrderProcessor(IOptions<StorageOptions> options, IRepository<District>  districtRepository, IRepository<Order> orderRepository, IRepository<FilterResult> filterResultRepo)
{
    private readonly IRepository<District> _districtRepository = districtRepository;
    private readonly IRepository<Order> _orderRepository = orderRepository;
    private readonly IRepository<FilterResult> _FilterResultRepository= filterResultRepo;
    private readonly StorageOptions _options = options.Value;
    public async Task<OrderFIlterRequestResult> GetByTimeStampAsync(OrderFilterRequest requestData)
    {
        var district = await _districtRepository.GetOneByAsync(x => x.Name == requestData.DistrictName);
        if (district == null) { throw new ArgumentException("District not found"); }
        var orders = await _orderRepository.GetByAsync(x => x.DeliveryDistrict.Name == district.Name && x.DeliveryDate > requestData.StartTime && x.DeliveryDate < requestData.EndTime);
        if (_options.UseDatabase)
            await SaveResultToDbAsync(orders, requestData.StartTime, requestData.EndTime, district);
        if (_options.UseFile)
            await SaveResultToFileAsync(orders, requestData.StartTime, requestData.EndTime, district);
        var result  = new OrderFIlterRequestResult() { StartTime = requestData.StartTime, EndTime = requestData.EndTime, Orders = orders.Adapt<List<OrderData>>(), District = district };
        return result;
    }

    private async Task SaveResultToDbAsync(ICollection<Order> orders, DateTime start, DateTime end, District district )
    {
        var filterResult = new FilterResult() { StartTime = start, EndTime = end, ResultData = orders.Adapt<List<OrderData>>(), District = district };
        await _FilterResultRepository.AddAsync(filterResult);
        await _FilterResultRepository.SaveChangesAsync();
    }

    private async Task SaveResultToFileAsync(ICollection<Order> orders, DateTime start, DateTime end, District district)
    {
        var storageFile = $"{options.Value.FilePath}\\Result_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";
        var filterResult = new FilterResult() { StartTime = start, EndTime = end, ResultData = orders.Adapt<List<OrderData>>(), District = district };
        using (StreamWriter writer = new StreamWriter(storageFile, true, Encoding.UTF8))
        {
            await writer.WriteLineAsync(filterResult.ToString());
        }
    }
}

