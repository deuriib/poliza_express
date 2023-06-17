using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PolizaExpress.Infrastructure.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    public ExceptionHandlerMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Un error ha ocurrido");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ErrorResponse();
        
        switch (exception)
        {
            case ApplicationException ex:
                if (ex.Message.Contains("invalid_token"))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorResponse.Message = "";
                    break;
                }

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = ex.Message;
                break;
            case InvalidOperationException ex:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = ex.Message;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal Server errors. Check Logs!";
                break;
        }
        
        errorResponse.Code = response.StatusCode;

        _logger.LogError(exception, "Un error ha ocurrido");
        
        var result = JsonSerializer.Serialize(errorResponse);
        
        await context.Response.WriteAsync(result);
    }
}