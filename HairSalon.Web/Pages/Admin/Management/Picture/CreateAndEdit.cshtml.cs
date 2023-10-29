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

namespace HairSalon.Web.Pages.Admin.Picture
{
    public class EditModel : PageModel
    {

        public EditModel(PictureService pictureService)
        {
            PictureService = pictureService;
        }

        [BindProperty]
        public PictureDetails PictureDetails { get; set; } = default!;
        public PictureService PictureService { get; }
        public List<SelectListItem> PictureTypeSelectList { get; private set; }

        private void Load()
        {
            PictureTypeSelectList = Enum.GetValues(typeof(PictureType))
                .Cast<PictureType>()
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

            var picturedetails = await PictureService.GetPictureDetalsAsync(id.Value);
            if (picturedetails == null)
            {
                return NotFound();
            }

            PictureDetails = picturedetails;

            Load();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (PictureDetails == null)
            {
                Load();
                return Page();
            }

            await PictureService.AddOrUopdatePictureAsync(PictureDetails);

            return RedirectToPage("./Index");
        }
    }
}
