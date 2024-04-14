using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Appointment.WebAPI.Model
{
    [Table("Appointments")]
    public partial class AppointmentModel
    {
        public int? UserID { get; set; }

        [Key]
        public int AppointmentNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AppointmentDate { get; set; }

        public TimeSpan? AppointmentTime { get; set; }

        [StringLength(100)]
        public string AppointmentName { get; set; }

        [Column(TypeName = "text")]
        public string Descriptions { get; set; }
    }
}
