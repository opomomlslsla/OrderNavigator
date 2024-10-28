using Aplication.Helpers;
using Aplication.Services.Options;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aplication.Factories;

public class DataSaverFactory(IOptions<StorageOptions> options, IServiceProvider serviceProvider) : IDataSaverFactory
{
    private readonly StorageOptions _options = options.Value;

    public ICollection<IDataSaver> GetDataSavers()
    {
        var savers = new List<IDataSaver>();
        if (_options.UseDatabase)
            savers.Add(serviceProvider.GetRequiredService<DbDataSaver>());
        if (_options.UseFile)
            savers.Add(serviceProvider.GetRequiredService<FileDataSaver>());
        return savers;
    }

}
