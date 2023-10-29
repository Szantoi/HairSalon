using HairSalon.Bll.Models;
using HairSalon.Bll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HairSalon.Web.Pages.Admin.ShopServicis
{
    public class EditModel : PageModel
    {
        public EditModel(ShopServiceService shopServiceService)
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
                return Page();
            }

            var shopservicedetails = await ShopServiceService.GetShopServiceDetalsAsync(id.Value);
            if (shopservicedetails == null)
            {
                return NotFound();
            }

            ShopServiceDetails = shopservicedetails;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ShopServiceDetails == null)
                return Page();

            await ShopServiceService.AddOrUopdateShopServiceAsync(ShopServiceDetails);

            return RedirectToPage("./Index");
        }
    }
}
