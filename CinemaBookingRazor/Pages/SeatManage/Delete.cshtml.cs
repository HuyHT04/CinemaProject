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
    public class DeleteModel : PageModel
    {
        private readonly DAL.Data.CinemaBookingRazorContext _context;

        public DeleteModel(DAL.Data.CinemaBookingRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _context.Seats.FindAsync(id);
            if (seat != null)
            {
                Seat = seat;
                _context.Seats.Remove(Seat);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
