using Microsoft.IdentityModel.Tokens;
using SubuProtokol.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SubuProtokol.API.Helpers
{
    public interface ITokenHelper
    {
        string GenerateToken(string username, string userrole);
    }

    public class TokenHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;

        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username, string userrole)
        {
            string secret = _configuration.GetValue<string>("AppSettings:Secret");
            byte[] key = Encoding.UTF8.GetBytes(secret);
            //string UserRole = null;
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //foreach (int item in Enum.GetValues(typeof(EnumUserRole)))
            //{
            //    if(item == userrole)
            //    {
            //        UserRole =  Enum.GetName(typeof(EnumUserRole), item);
            //    }
            //}
            List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim("username", username));
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim(ClaimTypes.Role, userrole.ToString()));
            claims.Add(new Claim("roles", userrole.ToString()));

            //foreach (string role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            JwtSecurityToken securityToken =
                new JwtSecurityToken(
                    signingCredentials: credentials,
                    claims: claims,
                    expires: DateTime.Now.AddDays(3));

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
