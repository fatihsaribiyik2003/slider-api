using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubuProtokol.API.Helpers;
using SubuProtokol.Models;
using SubuProtokol.Services.EntityFramework.Managers;
using System.Net.Http.Headers;

namespace SubuProtokol.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : MyControllerBase
    {
        private readonly IProtokolUserService _userService;




        public AuthController(IProtokolUserService userservice)
        {

            _userService = userservice;
        }

        [HttpPost]
        [Route ("api/login/{username}/{password}")]
        public async Task< IActionResult> Login(string username, string password, [FromServices] ITokenHelper tokenHelper)
        {

            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://apilogin.subu.edu.tr/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"api/Login?username={username}&password={password}").Result;
            //TODO + ApiLogin kullanıcı adı ve şifresi doğrulanan kullanıcı sisteme giriş yapabilir.
            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
            {
                //TODO + User tablosunda tanımlı aktif kullanıcı sisteme giriş yapabilir.


                char[] karakterler = username.ToCharArray();
                if (karakterler[2].ToString() == "0" || karakterler[2].ToString() == "1" || karakterler[2].ToString() == "2" || karakterler[2].ToString() == "3" || karakterler[2].ToString() == "4" ||
                   karakterler[2].ToString() == "5" || karakterler[2].ToString() == "6" || karakterler[2].ToString() == "7" || karakterler[2].ToString() == "8" || karakterler[2].ToString() == "9")
                {
                    return BadRequest("Kullanıcı tanımlı değildir. ");
                }
                else
                {
                    UserLoginModel user = _userService.Authenticate(username);

                    if (user == null)
                    {
                        string token = tokenHelper.GenerateToken(
                           username, "2");
                        return Ok(new { Token = token });
                    }
                    else
                    {
                        string token = tokenHelper.GenerateToken(
                            user.UserName, user.UserRole);

                        return Ok(new { Token = token });
                    }
                }
                return null;
            }
            return BadRequest("dffff");

        }

         [HttpPost]
        [Route ("api/login2")]
        public async Task< IActionResult> Login2([FromBody] LoginModel model, [FromServices] ITokenHelper tokenHelper)
        {

            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://apilogin.subu.edu.tr/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"api/Login?username={model.Username}&password={model.Password}").Result;
            //TODO + ApiLogin kullanıcı adı ve şifresi doğrulanan kullanıcı sisteme giriş yapabilir.
            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
            {
                //TODO + User tablosunda tanımlı aktif kullanıcı sisteme giriş yapabilir.


                char[] karakterler = model.Username.ToCharArray();
                if (karakterler[2].ToString() == "0" || karakterler[2].ToString() == "1" || karakterler[2].ToString() == "2" || karakterler[2].ToString() == "3" || karakterler[2].ToString() == "4" ||
                   karakterler[2].ToString() == "5" || karakterler[2].ToString() == "6" || karakterler[2].ToString() == "7" || karakterler[2].ToString() == "8" || karakterler[2].ToString() == "9")
                {
                    return BadRequest("Kullanıcı tanımlı değildir. ");
                }
                else
                {
                    UserLoginModel user = _userService.Authenticate(model.Username);

                    if (user == null)
                    {
                        string token = tokenHelper.GenerateToken(
                           model.Username, "2");
                        return Ok(new { Token = token });
                    }
                    else
                    {
                        string token = tokenHelper.GenerateToken(
                            user.UserName, user.UserRole);

                        return Ok(new { Token = token });
                    }
                }
                return null;
            }
            return BadRequest("dffff");

        }


    }
}
