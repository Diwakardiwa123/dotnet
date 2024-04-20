using Appointment.WebAPI.Model;
using Appointment.WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Appointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AppointmentDbContext _dbContext;
        private IConfiguration _configuration;

        public LoginController(AppointmentDbContext dBContext, IConfiguration configuration)
        {
            _dbContext = dBContext;
            _configuration = configuration;
        }

        // This is authenticating process.
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginAsync([FromBody] UserLogin user)
        {
            if (!this.ModelState.IsValid || user is null) return BadRequest("Please enter proper Username and password");

            var hashedPass = UserService.HashPassword(user.Password);
            // we authorize the user and send the user to GetToken to generate token with userid as a payload.
            var requiredUser = _dbContext.UserTables.Where(x => x.UserName == user.UserName && x.UserPassword == hashedPass).FirstOrDefault();
            if (requiredUser == null) return BadRequest("Please enter correct Username and password");

            var token = this.GetToken(requiredUser);
            return Ok(token);
        }

        public string GetToken(UserTable requiredUser)
        {
            // Creating the JWT Token.
            var key = Encoding.ASCII.GetBytes(_configuration["JWTSettings:Key"]!);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, requiredUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, requiredUser.UserId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
