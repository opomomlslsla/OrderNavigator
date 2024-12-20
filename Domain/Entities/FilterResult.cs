﻿using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities;

public class FilterResult : BaseEntity
{
    public District District { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    [NotMapped]
    public List<OrderData> ResultData { get; set; } = [];

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"District: {District.Name}");
        sb.AppendLine($"StartTIme: {StartTime}");
        sb.AppendLine($"EndTime: {EndTime}");
        sb.AppendLine("Orders: \n{");
        foreach (var item in ResultData)
        {
            sb.AppendLine(item.ToString());
        }
        sb.AppendLine("}");
        return sb.ToString();
    }
}