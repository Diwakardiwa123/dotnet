using Appointment.WebAPI.Model;
using Appointment.WebAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Appointment.WebAPI.Controllers
{
    [Authorize]
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private AppointmentDbContext _dbContext;
        private IUserService _service;

        public UserController(AppointmentDbContext dBContext, IUserService service)
        {
            _dbContext = dBContext;
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetLoginStatus")]
        public bool GetLoginStatus()
        {
            return this.HttpContext.User.Identity is not null && this.HttpContext.User.Identity.IsAuthenticated;
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

            var userID = _service.GetUserID();
            if (!userID.IsNullOrEmpty())
            {
                var user = _dbContext.UserTables.Where(x => x.UserId.ToString() == userID).FirstOrDefault();
                return Ok(user);
            }
            else
            {
                return BadRequest("User is not found");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("PostUser")]
        public async Task<IActionResult> PostUserAsync([FromBody] UserTable user)
        {
            if (user is null) return NotFound("Requested body is null");

            try
            {
                var lastUserID = _dbContext.UserTables.OrderBy(x => x.UserId).Last().UserId;
                user.UserPassword = UserService.HashPassword(user.UserPassword);
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
        [Route("Remove")]
        public async Task<IActionResult> RemoveUserAsync()
        {
            var userID = _service.GetUserID();
            if (userID.IsNullOrEmpty()) return NotFound("User not found");

            try
            {
                var currentUser = _dbContext.UserTables.Where(x => x.UserId.ToString() == userID).FirstOrDefault();
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
    }
}
