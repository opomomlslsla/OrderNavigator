using Domain.Entities;
using Domain.ValueObjects;

namespace Aplication.DTO;

public class OrderFilterRequestResult
{
    public District District { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<OrderData> Orders { get; set; }
}
