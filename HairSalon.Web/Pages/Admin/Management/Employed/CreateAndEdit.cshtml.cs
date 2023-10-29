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

namespace HairSalon.Web.Pages.Admin.Employed
{
    public class EditModel : PageModel
    {
        public EditModel(EmployedService employedService)
        {
            EmployedService = employedService;
        }

        [BindProperty]
        public EmployedDetails EmployedDetails { get; set; } = default!;
        public EmployedService EmployedService { get; }
        public List<SelectListItem> EmployedTypeSelectList { get; private set; }

        private void Load()
        {
            EmployedTypeSelectList = Enum.GetValues(typeof(EmployedType))
                .Cast<EmployedType>()
                .Select(kt => new SelectListItem { Value = kt.ToString(), Text = kt.ToString() })
                .ToList();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                Load();
                return Page();
            }

            var employeddetails = await EmployedService.GetEmployedDetalsAsync(id.Value);
            if (employeddetails == null)
            {
                return NotFound();
            }

            EmployedDetails = employeddetails;

            Load();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (EmployedDetails == null)
            {
                Load();
                return Page();
            }

            await EmployedService.AddOrUopdateEmployedAsync(EmployedDetails);

            return RedirectToPage("./Index");
        }

    }
}
