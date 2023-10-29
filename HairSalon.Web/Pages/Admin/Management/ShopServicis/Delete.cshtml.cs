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

namespace HairSalon.Web.Pages.Admin.ShopServicis
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(ShopServiceService shopServiceService)
        {
            ShopServiceService = shopServiceService;
        }

        [BindProperty]
      public ShopServiceDetails ShopServiceDetails { get; set; } = default!;
        public ShopServiceService ShopServiceService { get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopservicedetails = await ShopServiceService.GetShopServiceDetalsAsync(id.Value);

            if (shopservicedetails == null)
            {
                return NotFound();
            }
            else 
            {
                ShopServiceDetails = shopservicedetails;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var shopservicedetails = await ShopServiceService.GetShopServiceDetalsAsync(id.Value);

            if (shopservicedetails != null)
            {
                await ShopServiceService.DelletShopServiceAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
