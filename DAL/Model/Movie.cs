using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Genre { get; set; }

    public int? Duration { get; set; }

    public decimal? Price { get; set; }

    public string? PosterUrl { get; set; }

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
