using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class Seat
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public bool? IsAvailable { get; set; }

    public int? RoomId { get; set; }

    public virtual Room? Room { get; set; }
}
