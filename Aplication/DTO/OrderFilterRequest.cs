namespace Aplication.DTO;

public class OrderFilterRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string DistrictName { get; set; }
}
