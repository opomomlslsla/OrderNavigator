using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Mapster;

namespace Aplication.Helpers;

public class DbDataSaver(IRepository<FilterResult> filterRepository) : IDataSaver
{
    private readonly IRepository<FilterResult> _filterRepository = filterRepository;
    public async Task SaveAsync(ICollection<Order> orders, DateTime start, DateTime end, District district)
    {
        var filterResult = new FilterResult() { StartTime = start, EndTime = end, ResultData = orders.Adapt<List<OrderData>>(), District = district };
        await _filterRepository.AddAsync(filterResult);
        await _filterRepository.SaveChangesAsync();
    }
}
