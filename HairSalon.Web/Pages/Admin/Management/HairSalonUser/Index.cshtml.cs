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

namespace HairSalon.Web.Pages.Admin.HairSalonUser
{
    public class IndexModel : PageModel
    {
        public IndexModel(HairSalonUserService hairSalonUserService)
        {
            HairSalonUserService = hairSalonUserService;
        }

        public IList<HairSalonUserDetails> HairSalonUserDetails { get;set; } = default!;
        public HairSalonUserService HairSalonUserService { get; }

        public async Task OnGetAsync()
        {
            HairSalonUserDetails = await HairSalonUserService.GetHairSalonUsersDetalsAsync();
        }
    }
}
