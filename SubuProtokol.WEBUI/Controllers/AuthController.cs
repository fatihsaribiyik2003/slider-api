using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SubuProtokol.WEBUI.Models;
using RestSharp.Authenticators;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SubuProtokol.WEBUI.Service;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace SubuProtokol.WEBUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthController(IApiService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("session_token");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("role");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("username");
            return View();
        }

        [HttpPost]
        public IActionResult Login(ApiUserLoginModel model)
        {
            var result = _apiService.Auth(model);

            if (result.UserRole == "4" || result.UserRole == "3" || result.UserRole == "2")
            {

                return RedirectToAction("ProtokolListAll", "Protokol", new { area = "./" });
            }
            TempData["user"] = "Bilgilerinizi Kontrol Ediniz";
            return RedirectToAction("Login", "Auth");
        }
        public async Task<IActionResult> LogOut()
        {
            return RedirectToAction("Login");
        }
    }
}
