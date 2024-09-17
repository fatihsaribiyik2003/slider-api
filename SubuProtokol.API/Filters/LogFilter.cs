using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SubuProtokol.API.Filters
{

    public class LogFilter<T> : IActionFilter
    {
        private readonly ILogger<T> _logger;

        public LogFilter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // ..
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string message = "OnActionExecuting.. Controller : {Controller} - Action : {Action} - Username : {Username} - Roles : {@Roles} - Arguments : {@Arguments}";

            string controllerName = context.ActionDescriptor.RouteValues["controller"];

            string actionName = context.ActionDescriptor.RouteValues["action"];

            string username = context.HttpContext.User.Identity.Name;

            string[] roles = context.HttpContext.User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray();

            var arguments = context.ActionArguments.Select(x => new
            {
                Name = x.Key,
                Type = x.Value.GetType().Name,
                Data = x.Value
            }).ToList();

            _logger.LogInformation(message, controllerName, actionName, username, roles, arguments);
        }
    }
}
