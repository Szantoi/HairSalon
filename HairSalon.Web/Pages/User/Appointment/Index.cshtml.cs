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
    public class IndexModel : PageModel
    {

        public IndexModel(AppointmentService appointmentService)
        {
            AppointmentService = appointmentService;
        }

        public IList<AppointmentDetails> AppointmentDetails { get; set; } = default!;
        public AppointmentService AppointmentService { get; }

        private async Task LoadAsync() 
        {
            AppointmentDetails = await AppointmentService.GetAppointmentsDetalsAsync();
        }

        public async Task OnGetAsync()
        {
            await LoadAsync();
        }
    }
}
