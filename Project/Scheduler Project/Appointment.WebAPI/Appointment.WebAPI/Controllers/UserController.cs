using Appointment.WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Appointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AppointmentDbContext _dbContext;

        public UserController(AppointmentDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        [Route("")]
        [Route("GetUser")]
        public IActionResult Get()
        {
            if (this.HttpContext.User.Identity is null || !this.HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            //var userID = GetUserIDFromJWT();
            //var user = _dbContext.UserTables.Where(x => x.UserId == userID).FirstOrDefault();
            //if (user is null) return NotFound("Requested data is not available");

            var identity = this.HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return BadRequest();

            var userID = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.UserTables.Where(x => x.UserId.ToString() == userID).FirstOrDefault();
            return Ok(user);
        }

        [HttpPost]
        [Route("PostUser")]
        public async Task<IActionResult> PostUserAsync([FromBody] UserTable user)
        {
            if (user is null) return NotFound("Requested body is null");

            try
            {
                var lastUserID = _dbContext.UserTables.Last().UserId;
                user.UserId = lastUserID + 1;

                _dbContext.UserTables.Add(user);
                var result = await _dbContext.SaveChangesAsync();
                return result == 1 ? Ok() : BadRequest("There is some problem while creating user");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove/{id:int}")]
        public async Task<IActionResult> RemoveUserAsync(int id)
        {
            try
            {
                var currentUser = _dbContext.UserTables.Where(x => x.UserId == id).FirstOrDefault();
                if (currentUser is null) return NotFound("User is not found for the mentioned id");

                _dbContext.UserTables.Remove(currentUser);
                var result = await _dbContext.SaveChangesAsync();
                return result == 1 ? Ok("User deleted successfully") : BadRequest("There is some problem while creating user");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserTable user)
        {
            try
            {
                var currentUser = _dbContext.UserTables.Where(x => x.UserId == user.UserId).FirstOrDefault();
                if (currentUser is null) return NotFound("User is not found for the mentioned id");

                currentUser.UserName = user.UserName;
                currentUser.UserPassword = user.UserPassword;
                currentUser.UserAddress = user.UserAddress;
                currentUser.MobileNumber = user.MobileNumber;
                currentUser.Email = user.Email;

                var result = await _dbContext.SaveChangesAsync();
                return result == 1 ? Ok("User updated") : BadRequest("There is some problem while creating user");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //private int GetUserIDFromJWT()
        //{
        //    var jwtToken = this.Request.Headers["Authorization"].ToString().Split(" ")[1];
        //    var payloadStr = jwtToken.ToString().Split(".")[1];

        //    var jsonString = Base64UrlEncoder.Decode(payloadStr);
        //    var id = JsonConvert.DeserializeObject<UserDetail>(jsonString)!.NameID;

        //    return int.Parse(id);
        //}
    }
}
