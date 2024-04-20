using Appointment.WebAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Appointment.WebAPI.Middleware
{
    public class ValidateMiddleware
    {
        RequestDelegate _next;
        IConfiguration _configuration;
        IUserService _service;

        public ValidateMiddleware(RequestDelegate next, IConfiguration configuration, IUserService service)
        {
            _next = next;
            _configuration = configuration;
            _service = service;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token is not null && token is not "Bearer")
                ValidateToken(token);

            await _next(context);
        }

        private void ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTSettings:Key"]);

            tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidIssuer = _configuration["JWTSettings:Issuer"],
                ValidAudience = _configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWTSettings:Key"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            if (validatedToken != null && validatedToken is JwtSecurityToken validToken)
            {
                var userID = validToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
                _service.SetUserID(userID);
            }
        }
    }
}
