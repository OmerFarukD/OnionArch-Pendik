using System.Text.Json;
using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Serilog.Loggers;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _exceptionHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _loggerService;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor,LoggerServiceBase loggerService)
    {
        _next = next;
        _exceptionHandler = new HttpExceptionHandler();
        _httpContextAccessor = httpContextAccessor;
        _loggerService = loggerService;

    }


    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            await LogException(httpContext, e);
            await HandleExceptionAsync(httpContext.Response, e);
        }

    }

    private Task LogException(HttpContext httpContext, Exception exception)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter{Type = httpContext.GetType().Name,Value = exception.ToString()}
        };
        LogDetailWithException logDetail = new LogDetailWithException()
        {
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?",
            MethodName = _next.Method.Name,
            ExceptionMessage = exception.Message
        };
        _loggerService.Error(JsonSerializer.Serialize(logDetail));
        return Task.CompletedTask;
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _exceptionHandler.Response = response;
        return _exceptionHandler.HandleExceptionAsync(exception);
    }
}