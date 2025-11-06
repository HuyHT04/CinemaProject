using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Model;

namespace CinemaBookingRazor.Pages.SeatManage
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.Data.CinemaBookingRazorContext _context;

        public DetailsModel(DAL.Data.CinemaBookingRazorContext context)
        {
            _context = context;
        }

        public Seat Seat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _context.Seats.FirstOrDefaultAsync(m => m.Id == id);
            if (seat == null)
            {
                return NotFound();
            }
            else
            {
                Seat = seat;
            }
            return Page();
        }
    }
}
