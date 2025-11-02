using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class Payment
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? PaymentTime { get; set; }

    public virtual Booking? Booking { get; set; }
}
