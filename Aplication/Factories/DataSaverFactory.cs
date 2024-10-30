using Aplication.Helpers;
using Aplication.Services.Options;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Aplication.Factories;

public class DataSaverFactory(IOptions<StorageOptions> options, IEnumerable<IDataSaver> dataSavers) : IDataSaverFactory
{
    private readonly StorageOptions _options = options.Value;
    private readonly IEnumerable<IDataSaver> _dataSavers = dataSavers;
    public ICollection<IDataSaver> GetDataSavers()
    {
        var savers = new List<IDataSaver?>();
        if (_options.UseDatabase)
            savers.Add(_dataSavers.SingleOrDefault(s => s is DbDataSaver));
        if (_options.UseFile)
            savers.Add(_dataSavers.SingleOrDefault(s => s is FileDataSaver));
        return savers;
    }

}
