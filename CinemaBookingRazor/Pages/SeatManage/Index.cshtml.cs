using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Model;
using BLL.Interfaces;

namespace CinemaBookingRazor.Pages.SeatManage
{
    public class IndexModel : PageModel
    {
        private readonly ISeatService _seatService;

        public IndexModel(ISeatService seatService)
        {
            _seatService = seatService;
        }

        public IList<Seat> Seat { get; set; } = new List<Seat>();

        public async Task OnGetAsync()
        {
            var response = await _seatService.GetAllAsync();

            if (response.Success && response.Data != null)
            {
                Seat = response.Data.ToList();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
