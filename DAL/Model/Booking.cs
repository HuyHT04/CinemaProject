using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class Booking
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int? ShowtimeId { get; set; }

    public string? SelectedSeats { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Showtime? Showtime { get; set; }

    public virtual AspNetUser? User { get; set; }
}
