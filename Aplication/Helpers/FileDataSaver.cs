using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Mapster;
using System.Text;

namespace Aplication.Helpers;

public class FileDataSaver(string filepath) : IDataSaver
{
    private readonly string _filepath = filepath;
    public async Task SaveAsync(ICollection<Order> orders, DateTime start, DateTime end, District district)
    {
        var storageFile = $"{filepath}\\Result_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";
        var filterResult = new FilterResult() { StartTime = start, EndTime = end, ResultData = orders.Adapt<List<OrderData>>(), District = district };
        using (StreamWriter writer = new StreamWriter(storageFile, true, Encoding.UTF8))
        {
            await writer.WriteLineAsync(filterResult.ToString());
        }
    }
}
