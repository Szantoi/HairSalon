using HairSalon.Bll.Models;
using HairSalon.Bll.Services;
using HairSalon.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace HairSalon.Web.Pages.Admin.Appointment.CreateAndEdit
{
    public class SelectDutyetimeModel : PageModel
    {

        [BindProperty]
        public AppointentInputModel InputModel { get; set; } = default!;

        public AppointmentService AppointmentService { get; }
        public EmployedService EmployedService { get; }
        public DutyTimeService DutyTimeService { get; }
        public ShopServiceService ShopServiceService { get; }

        public List<SelectListItem> DutyTimeListItems { get; private set; }
        public List<SelectListItem> ShopServiceListItems { get; private set; }

        public SelectDutyetimeModel(AppointmentService appointmentService, EmployedService employedService, DutyTimeService dutyTimeService, ShopServiceService shopServiceService)
        {
            AppointmentService = appointmentService;
            EmployedService = employedService;
            DutyTimeService = dutyTimeService;
            ShopServiceService = shopServiceService;
        }

        private async Task LoadAsync()
        {

            var employmeds = await EmployedService.GetEmployedsDetalsAsync();
            if (InputModel == null)
                return;
            else if (InputModel.EmployedDetails == null)
            {

                DutyTimeListItems = (await DutyTimeService.GetDutyTimesDetalsAsync())
                    .Where(d =>
                                d.EnployedId == InputModel.EmployedDetails.Id &&
                                d.TimeStart > DateTime.Now.AddHours(-1))
                    .OrderBy(d => d.TimeStart)
                    .Select(d =>
                    new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{employmeds.Where(e => e.Id == d.EnployedId).Select(e => e.Name).Single()} |" +
                        $"{d.TimeStart:MM.dd/HH:mm} | {d.TimeEnd:dd/HH:mm}"
                    })
                    .ToList();

                ShopServiceListItems = (await ShopServiceService.GetShopServicesDetalsAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList();
            }
            else
            {

                DutyTimeListItems = (await DutyTimeService.GetDutyTimesDetalsAsync())
                    .Where(d =>
                                d.TimeStart > DateTime.Now.AddHours(-1))
                    .OrderBy(d => d.TimeStart)
                    .Select(d =>
                    new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{employmeds.Where(e => e.Id == d.EnployedId).Select(e => e.Name).Single()} |" +
                        $"{d.TimeStart:MM.dd/HH:mm} | {d.TimeEnd:dd/HH:mm}"
                    })
                    .ToList();

                ShopServiceListItems = (await ShopServiceService.GetShopServicesDetalsAsync())
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .ToList();
            }
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            byte[] inputModelBytes;
            if (HttpContext.Session.TryGetValue("AppointentInputModel", value: out inputModelBytes))
            {
                var inputModelJson = Encoding.UTF8.GetString(inputModelBytes);
                var inputModel = JsonConvert.DeserializeObject<AppointentInputModel>(inputModelJson);

                if (id != null  && inputModel == null)
                {
                    var appointmentdetails = await AppointmentService.GetAppointmentDetalsAsync(id.Value);

                    if (appointmentdetails == null)
                        return NotFound();

                    var imputModel = new AppointentInputModel();

                    imputModel.Id = id;
                    imputModel.AppointmentDetails = appointmentdetails;

                    InputModel = imputModel;
                }
                else if (inputModel == null) { }
                else if (inputModel.EmployedDetails != null && inputModel.AppointmentDetails == null)
                    InputModel = inputModel;
                else if (inputModel.AppointmentDetails != null && inputModel.EmployedDetails == null)
                    InputModel = inputModel;


                HttpContext.Session.Remove("AppointentInputModel");
            }

            await LoadAsync();
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (InputModel.AppointmentDetails == null)
            {
                await LoadAsync();
                return Page();
            }

            HttpContext.Session.Set("AppointentInputModel", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(InputModel)));

            return RedirectToPage("./CreateAndEdit");
        }
    }
}
