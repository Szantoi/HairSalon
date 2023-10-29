using HairSalon.Bll.Models;
using HairSalon.Bll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace HairSalon.Web.Pages.Admin.Appointment.CreateAndEdit
{
    public class SelectEmployedModel : PageModel
    {
        public SelectEmployedModel(EmployedService employedService, AppointmentService appointmentService, DutyTimeService dutyTimeService)
        {
            EmployedService = employedService;
            AppointmentService = appointmentService;
            DutyTimeService = dutyTimeService;
        }

        [BindProperty]
        public AppointentInputModel InputModel { get; set; } = default!;
        public EmployedService EmployedService { get; }
        public AppointmentService AppointmentService { get; }
        public DutyTimeService DutyTimeService { get; }
        public List<SelectListItem> EmploymedListItems { get; private set; }

        private async Task LoadAsync()
        {
            EmploymedListItems = (await EmployedService.GetEmployedsDetalsAsync())
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                await LoadAsync();
                return Page();
            }

            var appointmentdetails = await AppointmentService.GetAppointmentDetalsAsync(id.Value);

            if (appointmentdetails != null)
            {
                var dutyTimeDetals = await DutyTimeService.GetDutyTimeDetalsAsync(appointmentdetails.DutyTimeId);
                var employedDetails = await EmployedService.GetEmployedDetalsAsync(dutyTimeDetals.EnployedId);

                if (employedDetails == null)
                {
                    return NotFound();
                }

                var imputModel = new AppointentInputModel();

                imputModel.Id = id;
                imputModel.EmployedDetails = employedDetails;
                imputModel.AppointmentDetails = appointmentdetails;

                InputModel = imputModel;

                await LoadAsync();
                return Page();
            }

            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (InputModel.EmployedDetails == null)
            {
                await LoadAsync();
                return Page();
            }

            HttpContext.Session.Set("AppointentInputModel", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(InputModel)));
            return RedirectToPage("./SelectDutyetime");
        }
    }
}
