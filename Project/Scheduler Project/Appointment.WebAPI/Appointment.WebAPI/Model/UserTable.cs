using System;
using System.Collections.Generic;

namespace Appointment.WebAPI.Model;

public partial class UserTable
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public string? UserAddress { get; set; }

    public string? MobileNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
