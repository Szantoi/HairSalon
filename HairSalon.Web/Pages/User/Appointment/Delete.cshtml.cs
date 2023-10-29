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

namespace HairSalon.Web.Pages.User.Appointment
{
    public class DeleteModel : PageModel
    {

        public DeleteModel(AppointmentService appointmentService)
        {
            AppointmentService = appointmentService;
        }

        [BindProperty]
      public AppointmentDetails AppointmentDetails { get; set; } = default!;
        public AppointmentService AppointmentService { get; }

        private Task LoadAsync()
        {
            throw new NotImplementedException();
        }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointmentdetails = await AppointmentService.GetAppointmentDetalsAsync(id.Value);

            if (appointmentdetails != null)
            {
                await AppointmentService.DelletAppointmentAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
