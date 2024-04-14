using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Appointment.WebAPI.Model
{
    public class AppointmentDBContext : DbContext
    {
        public DbSet<UserTable> Users { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }

        public AppointmentDBContext(DbContextOptions<AppointmentDBContext> option) : base(option) { }
    }
}
