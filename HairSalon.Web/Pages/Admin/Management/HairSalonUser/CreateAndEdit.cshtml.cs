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

namespace HairSalon.Web.Pages.Admin.HairSalonUser
{
    public class EditModel : PageModel
    {
        public EditModel(HairSalonUserService hairSalonUserService)
        {
            HairSalonUserService = hairSalonUserService;
        }

        [BindProperty]
        public HairSalonUserDetails HairSalonUserDetails { get; set; } = default!;
        public HairSalonUserService HairSalonUserService { get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var hairsalonuserdetails =  await HairSalonUserService.GetHairSalonUserDetalsAsync(id.Value);
            if (hairsalonuserdetails == null)
            {
                return NotFound();
            }

            HairSalonUserDetails = hairsalonuserdetails;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (HairSalonUserDetails == null)
                return Page();

            await HairSalonUserService.AddOrUopdateHairSalonUserAsync(HairSalonUserDetails);

            return RedirectToPage("./Index");
        }
    }
}
