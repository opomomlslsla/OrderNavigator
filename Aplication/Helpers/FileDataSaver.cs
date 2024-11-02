using Aplication.Services.Options;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Mapster;
using Microsoft.Extensions.Options;
using System.Text;

namespace Aplication.Helpers;

public class FileDataSaver(IOptions<StorageOptions> options) : IDataSaver
{
    private readonly string _filepath = options.Value.FilePath;
    public async Task SaveAsync(ICollection<Order> orders, DateTime start, DateTime end, District district)
    {
        var storageFile = $"{_filepath}\\Result_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";
        var filterResult = new FilterResult() 
        {
            StartTime = start, EndTime = end, 
            ResultData = orders.Adapt<List<OrderData>>(), 
            District = district 
        };
        await using var writer = new StreamWriter(storageFile, true, Encoding.UTF8);
        await writer.WriteLineAsync(filterResult.ToString());
    }
}
