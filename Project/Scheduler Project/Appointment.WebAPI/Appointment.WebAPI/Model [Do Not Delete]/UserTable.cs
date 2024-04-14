using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Appointment.WebAPI.Model
{
    [Table("UserTable")]
    public partial class UserTable
    {
        public UserTable()
        {
            //this.Appointments = new HashSet<AppointmentModel>();
        }

        [Key]
        public int UserID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string UserPassword { get; set; }

        [Column(TypeName = "text")]
        public string UserAddress { get; set; }

        [StringLength(50)]
        public string MobileNumber { get; set; }

        [Column(TypeName = "text")]
        public string Email { get; set; }

        //public ICollection<AppointmentModel> Appointments { get; set; }
    }
}
