using Domain.Entities;

namespace Domain.Interfaces;

public interface IDataSaver
{
    Task SaveAsync(ICollection<Order> orders, DateTime start, DateTime end, District district);
}
