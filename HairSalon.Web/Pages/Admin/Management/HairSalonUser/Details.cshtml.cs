﻿using System;
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
    public class DetailsModel : PageModel
    {
        public DetailsModel(HairSalonUserService hairSalonUserService)
        {
            HairSalonUserService = hairSalonUserService;
        }

      public HairSalonUserDetails HairSalonUserDetails { get; set; } = default!;
        public HairSalonUserService HairSalonUserService { get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hairsalonuserdetails = await HairSalonUserService.GetHairSalonUserDetalsAsync(id.Value);

            if (hairsalonuserdetails == null)
            {
                return NotFound();
            }
            else 
            {
                HairSalonUserDetails = hairsalonuserdetails;
            }
            return Page();
        }
    }
}
