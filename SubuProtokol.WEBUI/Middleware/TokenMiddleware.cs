using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class TokenMiddleware
{
    
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }

            await _next(context);
        }
    
}



//namespace SubuProtokol.WEBUI.Middleware
//{
//    public class TokenMiddleware
//    {
//    }
//}
