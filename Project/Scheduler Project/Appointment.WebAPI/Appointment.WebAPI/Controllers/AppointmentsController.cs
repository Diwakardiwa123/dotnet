using Appointment.WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using AppointmentModel = Appointment.WebAPI.Model.Appointment;

namespace Appointment.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private AppointmentDbContext _dbContext;

        public AppointmentsController(AppointmentDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            UserTable user = this.GetCurrentUser();
            if (user is null) return BadRequest();

            var appointmentList = _dbContext.Appointments.Where(x => x.UserId == user.UserId).AsNoTracking().ToList(); // AsNoTracking increase performance
            if (appointmentList is null) return NotFound("Requested data is not available");

            return Ok(appointmentList);
        }

        [HttpPost]
        [Route("PostAppointment")]
        public async Task<IActionResult> PostAppointmentAsync([FromBody] object obj)
        {
            if (obj is null) return NotFound("Requested body is null");

            UserTable user = this.GetCurrentUser();
            if (user is null) return BadRequest();

            var appointment = JsonConvert.DeserializeObject<AppointmentModel>(obj.ToString());
            if (appointment is null) return BadRequest();

            try
            {
                var lastAppointmentID = _dbContext.Appointments.OrderBy(x => x.AppointmentNumber).Last().AppointmentNumber;
                appointment.AppointmentNumber = lastAppointmentID + 1;
                appointment.UserId = user.UserId;
                appointment.AppointmentTime = appointment.AppointmentDate!.Value.Date + appointment.AppointmentTime!.Value.TimeOfDay;

                _dbContext.Appointments.Add(appointment);
                var result = await _dbContext.SaveChangesAsync();
                return result == 1 ? Ok() : BadRequest("There is some problem while creating appointment");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveAppointmentAsync([FromBody] AppointmentModel appointment)
        {
            if (appointment is null) return NotFound("Requested body is null");

            UserTable user = this.GetCurrentUser();
            if (user is null || user.UserId != appointment.UserId) return BadRequest();

            try
            {
                var currentAppointment = _dbContext.Appointments.Where(x => x.AppointmentNumber == appointment.AppointmentNumber).FirstOrDefault();
                if (currentAppointment is null) return NotFound("Appointment is not found for the mentioned id");

                _dbContext.Appointments.Remove(currentAppointment);
                var result = await _dbContext.SaveChangesAsync();
                return result == 1 ? Ok("Appointment deleted successfully") : BadRequest("There is some problem while creating user");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveAll")]
        public async Task<IActionResult> RemoveAllAppointmentAsync([FromBody] AppointmentModel appointment)
        {
            if (appointment is null) return NotFound("Requested body is null");

            UserTable user = this.GetCurrentUser();
            if (user is null || user.UserId != appointment.UserId) return BadRequest();

            try
            {
                var currentAppointments = _dbContext.Appointments.Where(x => x.UserId == appointment.UserId);
                if (currentAppointments is null) return NotFound("Appointment is not found for the mentioned id");

                _dbContext.Appointments.RemoveRange(currentAppointments);
                var result = await _dbContext.SaveChangesAsync();
                return result == 1 ? Ok("Appointments deleted successfully") : BadRequest("There is some problem while creating user");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAppointmentAsync([FromBody] AppointmentModel appointment)
        {
            if (appointment is null) return NotFound("Requested body is null");

            UserTable user = this.GetCurrentUser();
            if (user is null || user.UserId != appointment.UserId) return BadRequest();

            try
            {
                var currentAppointment = _dbContext.Appointments.Where(x => x.AppointmentNumber == appointment.AppointmentNumber).FirstOrDefault();
                if (currentAppointment is null) return NotFound("Appointment is not found for the mentioned id");

                currentAppointment.AppointmentDate = appointment.AppointmentDate;
                currentAppointment.AppointmentTime = appointment.AppointmentTime;
                currentAppointment.AppointmentName = appointment.AppointmentName;
                currentAppointment.Descriptions = appointment.Descriptions;

                var result = await _dbContext.SaveChangesAsync();
                return result == 1 ? Ok("Appointment updated") : BadRequest("There is some problem while creating user");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private UserTable GetCurrentUser()
        {
            var identity = this.HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return null;

            var userID = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return _dbContext.UserTables.Where(x => x.UserId.ToString() == userID).FirstOrDefault()!;
        }
    }
}
