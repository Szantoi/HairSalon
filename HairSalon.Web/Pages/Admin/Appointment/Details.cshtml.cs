using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HairSalon;
using HairSalon.Bll.Models;
using HairSalon.Bll.Services;

namespace HairSalon.Web.Pages.Admin.Appointment
{
    public class DetailsModel : PageModel
    {

        public DetailsModel(AppointmentService appointmentService)
        {
            AppointmentService = appointmentService;
        }

        public AppointmentDetails AppointmentDetails { get; set; } = default!;
        public AppointmentService AppointmentService { get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentdetails = await AppointmentService.GetAppointmentDetalsAsync(id.Value);
            if (appointmentdetails == null)
            {
                return NotFound();
            }
            else
            {
                AppointmentDetails = appointmentdetails;
            }
            return Page();
        }
    }
}
