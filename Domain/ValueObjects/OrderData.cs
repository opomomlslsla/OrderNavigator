using Domain.Entities;

namespace Domain.ValueObjects;

public class OrderData
{
    public int Weight { get; set; }
    public DateTime DeliveryDate { get; set; }
    public double OrderNumber { get; set; }

    public override string ToString()
    {
        return $"DeliveryDate: {DeliveryDate}  \nWeight: {Weight}  \nNumber: {OrderNumber}";
    }
}
