using Appointment.WebAPI.Model;
using Appointment.WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IO;
using Files = System.IO.File;

namespace Appointment.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            try
            {
                if (!this.ModelState.IsValid || user is null) return BadRequest("Please enter proper Username and password");

                var hashedPass = UserService.HashPassword(user.Password);
                // we authorize the user and send the user to GetToken to generate token with userid as a payload.
                var requiredUser = _dbContext.UserTables.Where(x => x.UserName == user.UserName && x.UserPassword == hashedPass).FirstOrDefault();
                if (requiredUser == null) return BadRequest("Please enter correct Username and password");

                var token = this.GetToken(requiredUser);
                return Ok(token);
            }
            catch (Exception ex)
            {

                string fileName = @"C:\Diwakar\git\Project\Scheduler Project\ErrorLog.txt";
                try
                {
                    // Check if file already exists. If yes, delete it.
                    if (Files.Exists(fileName))
                    {
                        Files.Delete(fileName);
                    }

                    // Create a new file
                    using (FileStream fs = Files.Create(fileName))
                    {
                        // Add some text to file
                        Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                        fs.Write(title, 0, title.Length);
                    }

                    // Open the stream and read it back.
                    using (StreamWriter sw = Files.CreateText(fileName))
                    {
                        sw.WriteLine(ex.InnerException);
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
                return BadRequest(ex.Message);
            }
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
