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

namespace HairSalon.Web.Pages.Admin.Picture
{
    public class IndexModel : PageModel
    {

        public IndexModel(PictureService pictureService)
        {
            PictureService = pictureService;
        }

        public IList<PictureDetails> PictureDetails { get;set; } = default!;
        public PictureService PictureService { get; }

        public async Task OnGetAsync()
        {
                PictureDetails = await PictureService.GetPicturesDetalsAsync();
        }
    }
}
