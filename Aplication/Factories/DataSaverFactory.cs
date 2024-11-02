using Aplication.Helpers;
using Aplication.Services.Options;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Aplication.Factories;

public class DataSaverFactory(IOptions<StorageOptions> options, IEnumerable<IDataSaver> dataSavers) : IDataSaverFactory
{
    private readonly StorageOptions _options = options.Value;
    public ICollection<IDataSaver> GetDataSavers()
    {
        var savers = new List<IDataSaver?>();
        if (_options.UseDatabase)
            savers.Add(dataSavers.Single(s => s is DbDataSaver));
        if (_options.UseFile)
            savers.Add(dataSavers.Single(s => s is FileDataSaver));
        return savers;
    }
}