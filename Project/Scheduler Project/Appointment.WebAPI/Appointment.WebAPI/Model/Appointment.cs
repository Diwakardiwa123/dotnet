﻿using System;
using System.Collections.Generic;

namespace Appointment.WebAPI.Model;

public partial class Appointment
{
    public int? UserId { get; set; }

    public int AppointmentNumber { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public DateTime? AppointmentTime { get; set; }

    public string? AppointmentName { get; set; }

    public string? Descriptions { get; set; }

    public virtual UserTable? User { get; set; }
}
