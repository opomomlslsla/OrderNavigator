using Aplication.DTO;
using Aplication.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/Orders")]
[ApiController]
public class OrderController(
    ILogger<OrderController> logger, 
    IValidator<OrderFilterRequest> validator,
    OrderScheduler scheduler) : ControllerBase
{
    private readonly ILogger _logger = logger;
    private readonly IValidator<OrderFilterRequest> _validator = validator;
    [HttpPost("Filter")]
    public async Task<IActionResult> GetFilteredOrders(OrderFilterRequest requestData)
    {
        _validator.ValidateAndThrow(requestData);
        await scheduler.ScheduleOrderFilterJob(requestData);
        _logger.LogInformation("Controller method \"Filter\" executed sucsessfully");
        return Accepted();
    }
}
