using HairSalon.Bll.Models;
using HairSalon.Bll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace HairSalon.Web.Pages.User.Appointment.CreateAndEdit
{
    public class EditModel : PageModel
    {

        public EditModel(AppointmentService appointmentService, EmployedService employedService, DutyTimeService dutyTimeService)
        {
            AppointmentService = appointmentService;
            EmployedService = employedService;
            DutyTimeService = dutyTimeService;
        }

        [BindProperty]
        public AppointentInputModel InputModel { get; set; } = default!;

        public AppointmentService AppointmentService { get; }
        public EmployedService EmployedService { get; }
        public DutyTimeService DutyTimeService { get; }

        public List<SelectListItem> DutyTimeListItems { get; private set; }

        private async Task LoadAsync()
        {
            var employmeds = await EmployedService.GetEmployedsDetalsAsync();

            DutyTimeListItems = (await DutyTimeService.GetDutyTimesDetalsAsync())
                .Where(d=> d.TimeStart>DateTime.Now.AddHours(-1))
                .OrderBy(d=>d.TimeStart)
                .Select(d => 
                new SelectListItem {
                    Value = d.Id.ToString(),
                    Text = $"{employmeds.Where(e=> e.Id == d.EnployedId).Select(e=>e.Name).Single()} |" +
                    $"{d.TimeStart:MM.dd/HH:mm} | {d.TimeEnd:dd/HH:mm}" 
                })
                .ToList();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            byte[] inputModelBytes;
            if (HttpContext.Session.TryGetValue("AppointentUserInputModel", value: out inputModelBytes))
            {
                var inputModelJson = Encoding.UTF8.GetString(inputModelBytes);
                var inputModel = JsonConvert.DeserializeObject<AppointentInputModel>(inputModelJson);

                if (id != null && inputModel == null)
                {
                    var appointmentdetails = await AppointmentService.GetAppointmentDetalsAsync(id.Value);

                    if (appointmentdetails == null)
                        return NotFound();

                    InputModel.AppointmentDetails = appointmentdetails;
                }
                else if (inputModel == null) { }
                else if (inputModel.EmployedDetails != null && inputModel.AppointmentDetails == null)
                    InputModel = inputModel;
                else if (inputModel.AppointmentDetails != null && inputModel.EmployedDetails == null)
                    InputModel = inputModel;


                HttpContext.Session.Remove("AppointentUserInputModel");
            }

            await LoadAsync();
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (InputModel.AppointmentDetails == null)
            {
                await LoadAsync();
                return Page();
            }

            await AppointmentService.AddOrUopdateAppointmentAsync(InputModel.AppointmentDetails);

            return RedirectToPage("../Index");
        }


    }
}
