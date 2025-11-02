using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
