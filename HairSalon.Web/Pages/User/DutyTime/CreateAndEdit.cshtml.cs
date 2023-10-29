using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HairSalon;
using HairSalon.Bll.Models;
using HairSalon.Bll.Services;
using System.Runtime.InteropServices;

namespace HairSalon.Web.Pages.User.DutyTime
{
    public class EditModel : PageModel
    {
        public EditModel(DutyTimeService dutyTimeService, EmployedService employedService)
        {
            DutyTimeService = dutyTimeService;
            EmployedService = employedService;
        }

        [BindProperty]
        public DutyTimeDetails DutyTimeDetails { get; set; } = default!;
        public DutyTimeService DutyTimeService { get; }
        public EmployedService EmployedService { get; }
        public List<SelectListItem> EmployedServiceListItems { get; private set; }
        public List<SelectListItem> DutyTimeTypeSelectList { get; private set; }

        private async Task LoadAsync()
        {
            EmployedServiceListItems = (await EmployedService.GetEmployedsDetalsAsync())
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

            DutyTimeTypeSelectList = Enum.GetValues(typeof(DutyTimeType))
                .Cast<DutyTimeType>()
                .Select(kt => new SelectListItem { Value = kt.ToString(), Text = kt.ToString() })
                .ToList();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                await LoadAsync();
                return Page();
            }

            var dutytimedetails =  await DutyTimeService.GetDutyTimeDetalsAsync(id.Value);
            if (dutytimedetails == null)
            {
                return NotFound();
            }

            DutyTimeDetails = dutytimedetails;

            await LoadAsync();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (DutyTimeDetails == null)
            {
                await LoadAsync();
                return Page();
            }

            await DutyTimeService.AddOrUopdateDutyTimeAsync(DutyTimeDetails);

            return RedirectToPage("./Index");
        }

    }
}
