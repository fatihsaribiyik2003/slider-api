using Microsoft.AspNetCore.Mvc.Filters;

namespace SubuProtokol.API.Filters
{
    public class AuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
