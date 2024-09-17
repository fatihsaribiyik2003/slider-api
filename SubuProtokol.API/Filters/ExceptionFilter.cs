using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SubuProtokol.Models;
using System.Security.Claims;

namespace SubuProtokol.API.Filters
{
    public class ExceptionFilter<T> : IExceptionFilter
    {
        private readonly ILogger<T> _logger;

        public ExceptionFilter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            string message = "Controller : {Controller} - Action : {Action} - Username : {Username} - Roles : {@Roles} - Exception : {@Exception}";

            string controllerName = context.ActionDescriptor.RouteValues["controller"];

            string actionName = context.ActionDescriptor.RouteValues["action"];

            string username = context.HttpContext.User.Identity.Name;

            string[] roles = context.HttpContext.User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray();


            _logger.LogError(message, controllerName, actionName, username, roles, context.Exception);


            //var response = new ResponseResult<object>
            //{
            //    Success = false
            //};

            //response.Messages.Add(context.Exception.Message);
            //response.Messages.Add(context.Exception.StackTrace);

            //context.Result = new BadRequestObjectResult(response);


            context.Result = new BadRequestObjectResult(
                new
                {
                    Error = true,
                    Message = context.Exception.Message,
                    StackTrace = context.Exception.StackTrace
                });
        }
    }
}
