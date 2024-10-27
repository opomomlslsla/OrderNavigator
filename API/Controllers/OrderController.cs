﻿using Aplication.DTO;
using Aplication.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/Orders")]
[ApiController]
public class OrderController(OrderProcessor orderProcessor, ILogger<OrderController> logger, IValidator<OrderFilterRequest> validator) : ControllerBase
{
    private readonly ILogger _logger = logger;
    private readonly OrderProcessor _orderProcessor = orderProcessor;
    private readonly IValidator<OrderFilterRequest> _validator = validator;
    [HttpPost("Filter")]
    public async Task<IActionResult> GetFilteredORders(OrderFilterRequest request)
    {
        _validator.ValidateAndThrow(request);
        var res = await _orderProcessor.GetByTimeStampAsync(request);
        _logger.LogInformation("Controller method \"Filter\" executed sucsessfully");
        return Ok(res);
    }
}
