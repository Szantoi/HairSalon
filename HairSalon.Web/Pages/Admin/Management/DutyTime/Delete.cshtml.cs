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

namespace HairSalon.Web.Pages.Admin.DutyTime
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(DutyTimeService dutyTimeService)
        {
            DutyTimeService = dutyTimeService;
        }

        [BindProperty]
      public DutyTimeDetails DutyTimeDetails { get; set; } = default!;
        public DutyTimeService DutyTimeService { get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutytimedetails = await DutyTimeService.GetDutyTimeDetalsAsync(id.Value);

            if (dutytimedetails == null)
            {
                return NotFound();
            }
            else 
            {
                DutyTimeDetails = dutytimedetails;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dutytimedetails = await DutyTimeService.GetDutyTimeDetalsAsync(id.Value);

            if (dutytimedetails != null)
            {
                await DutyTimeService.DelletDutyTimeAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
