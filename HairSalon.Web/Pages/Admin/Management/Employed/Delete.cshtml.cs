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

namespace HairSalon.Web.Pages.Admin.Employed
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(EmployedService employedService)
        {
            EmployedService = employedService;
        }

        [BindProperty]
      public EmployedDetails EmployedDetails { get; set; } = default!;
        public EmployedService EmployedService { get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeddetails = await EmployedService.GetEmployedDetalsAsync(id.Value);

            if (employeddetails == null)
            {
                return NotFound();
            }
            else 
            {
                EmployedDetails = employeddetails;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeddetails = await EmployedService.GetEmployedDetalsAsync(id.Value);

            if (employeddetails != null)
            {
                await EmployedService.DelletEmployedAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
