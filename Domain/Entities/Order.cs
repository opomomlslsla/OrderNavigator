﻿namespace Domain.Entities;

public class Order : BaseEntity
{
    public District DeliveryDistrict { get; set; }
    public int Weight { get; set; }
    public DateTime DeliveryDate { get; set; }
    public double OrderNumber { get; set; }

}
