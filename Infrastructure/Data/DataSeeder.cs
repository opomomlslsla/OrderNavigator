using Domain.Entities;

namespace Infrastructure.Data;

public static class DataSeeder
{
    public static void Seed(Context? context)
    {
        var date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        if (context != null && !context.Orders.Any())
        {
            context.Orders.AddRange(new List<Order>()
            {
                new Order
                {
                    DeliveryDistrict = new District { Name = "Central" },
                    Weight = 5,
                    DeliveryDate = DateTime.Now.AddDays(-2),
                    OrderNumber = 1,
                },
                new Order
                {
                    OrderNumber = 2,
                    DeliveryDistrict = new District { Name = "North" },
                    Weight = 10,
                    DeliveryDate = DateTime.Now.AddDays(-3)
                },
                new Order
                {
                    OrderNumber = 3,
                    DeliveryDistrict = new District { Name = "South" },
                    Weight = 7,
                    DeliveryDate = DateTime.Now.AddDays(-1)
                },
                new Order
                {
                    OrderNumber = 4,
                    DeliveryDistrict = new District { Name = "East" },
                    Weight = 12,
                    DeliveryDate = DateTime.Now.AddDays(-5)
                },
                new Order
                {
                    OrderNumber = 5,
                    DeliveryDistrict = new District { Name = "West" },
                    Weight = 8,
                    DeliveryDate = DateTime.Now.AddDays(-4)
                },
                new Order
                {
                    OrderNumber = 6,
                    DeliveryDistrict = new District { Name = "Central" },
                    Weight = 5,
                    DeliveryDate = DateTime.Now.AddDays(-1)
                },
                new Order
                {
                    OrderNumber = 7,
                    DeliveryDistrict = new District { Name = "North" },
                    Weight = 10,
                    DeliveryDate = DateTime.Now.AddDays(-2)
                },
                new Order
                {
                    OrderNumber = 8,
                    DeliveryDistrict = new District { Name = "South" },
                    Weight = 7,
                    DeliveryDate = DateTime.Now.AddDays(-1)
                },
                new Order
                {
                    OrderNumber = 9,
                    DeliveryDistrict = new District { Name = "East" },
                    Weight = 12,
                    DeliveryDate = DateTime.Now.AddDays(-5)
                },
                new Order
                {
                    OrderNumber = 10,
                    DeliveryDistrict = new District { Name = "West" },
                    Weight = 8,
                    DeliveryDate = DateTime.Now.AddDays(-4)
                }
            });
            context.SaveChanges();
        }
    }
}

