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

namespace HairSalon.Web.Pages.Admin.Picture
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(PictureService pictureService)
        {
            PictureService = pictureService;
        }

        [BindProperty]
      public PictureDetails PictureDetails { get; set; } = default!;
        public PictureService PictureService { get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picturedetails = await PictureService.GetPictureDetalsAsync(id.Value);

            if (picturedetails == null)
            {
                return NotFound();
            }
            else 
            {
                PictureDetails = picturedetails;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var picturedetails = await PictureService.GetPictureDetalsAsync(id.Value);

            if (picturedetails != null)
            {
                await PictureService.DelletPictureAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
