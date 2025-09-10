using Microsoft.AspNetCore.Mvc.Filters;

namespace MinimalApi.Middlewares;

public class LogActionFilter: ActionFilterAttribute
{
    private readonly ILogger<LogActionFilter> _logger;
    public LogActionFilter(ILogger<LogActionFilter> logger) => _logger = logger;


    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Executing action {action}", context.ActionDescriptor.DisplayName);
    }


    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Executed action {action}", context.ActionDescriptor.DisplayName);
        if (context.Exception != null)
            _logger.LogError(context.Exception, "Action {action} threw an exception", context.ActionDescriptor.DisplayName);
    }

}