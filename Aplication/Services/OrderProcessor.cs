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
    public async Task<OrderFIlterRequestResult> GetByTimeStampAsync(OrderFilterRequest request)
    {
        var district = await _districtRepository.GetOneByAsync(x => x.Name == request.DistrictName);
        if (district == null) { throw new ArgumentException("District not found"); }
        var orders = await _orderRepository.GetByAsync(x => x.DeliveryDistrict.Name == district.Name && x.DeliveryDate > request.StartTime && x.DeliveryDate < request.EndTime);
        await SaveResultAsync(orders, request.StartTime, request.EndTime, district);
        var result  = new OrderFIlterRequestResult() { StartTime = request.StartTime, EndTime = request.EndTime, Orders = orders.Adapt<List<OrderData>>(), District = district };
        return result;
    }

    private async Task SaveResultAsync(ICollection<Order> result, DateTime start, DateTime end, District district )
    {
        if (options.Value.UseDatabase)
        {
            var filterResult = new FilterResult() { StartTime = start, EndTime = end, ResultData = result.Adapt<List<OrderData>>(), District = district };
            await _FilterResultRepository.AddAsync(filterResult);
            await _FilterResultRepository.SaveChangesAsync();
        }
        if (options.Value.UseFile) 
        {
            var storageFile =$"{options.Value.FilePath}\\Result_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";
            var filterResult = new FilterResult() { StartTime = start, EndTime = end, ResultData = result.Adapt<List<OrderData>>(), District = district };
            using (StreamWriter writer = new StreamWriter(storageFile,true, Encoding.UTF8))
            {
                await writer.WriteLineAsync(filterResult.ToString());
            }
        }
    }
}
