using System.Data.Common;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Serilog;
using FluentValidation;

namespace Infrastructure.Middleware;

public class ExeptionHandler(RequestDelegate requestDelegate, ILogger logger)
{
    private readonly ILogger _logger = logger;
    private readonly RequestDelegate _requestDelegate = requestDelegate;
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch(ValidationException ex)
        {
            _logger.Information(ex.Message);
            string msg = "Введенные данные некорректны";
            await HandleExeptionAsync(httpContext, msg, HttpStatusCode.BadRequest);
        }
        catch (ArgumentException ex)
        {
            _logger.Error($"{ex.Message} \n {ex.InnerException} \n {ex.StackTrace} \n");
            string msg = $"{ex.Message}";
            await HandleExeptionAsync(httpContext, msg, HttpStatusCode.BadRequest);
        }
        catch (DbException ex)
        {
            _logger.Error($"{ex.Message} \n {ex.InnerException} \n {ex.StackTrace} \n");
            string msg = $"{ex.Message} \n {ex.InnerException} \n {ex.StackTrace} \n";
            await HandleExeptionAsync(httpContext, msg, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.Error($"{ex.Message} \n {ex.InnerException} \n {ex.StackTrace} \n");
            string msg = "Internal server error";
            await HandleExeptionAsync(httpContext, msg, HttpStatusCode.InternalServerError);

        }
    }

    private async Task HandleExeptionAsync(HttpContext httpContext, string msg, HttpStatusCode statusCode)
    {
        HttpResponse response = httpContext.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)statusCode;
        await response.WriteAsJsonAsync(JsonSerializer.Serialize(msg));
    }

}
