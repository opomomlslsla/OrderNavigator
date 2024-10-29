using Aplication.DTO;
using Aplication.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/Orders")]
[ApiController]
public class OrderController(OrderFiltrator orderFiltrator, ILogger<OrderController> logger, IValidator<OrderFilterRequest> validator) : ControllerBase
{
    private readonly ILogger _logger = logger;
    private readonly OrderFiltrator _orderFiltrator = orderFiltrator;
    private readonly IValidator<OrderFilterRequest> _validator = validator;
    [HttpPost("Filter")]
    public async Task<IActionResult> GetFilteredORders(OrderFilterRequest requestData)
    {
        _validator.ValidateAndThrow(requestData);
        var res = await _orderFiltrator.FilterOrders(requestData);
        _logger.LogInformation("Controller method \"Filter\" executed sucsessfully");
        return Ok(res);
    }
}
