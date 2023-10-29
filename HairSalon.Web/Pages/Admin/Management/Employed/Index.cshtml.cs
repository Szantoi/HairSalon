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
    public class IndexModel : PageModel
    {
        public IndexModel(EmployedService employedService)
        {
            EmployedService = employedService;
        }

        public IList<EmployedDetails> EmployedDetails { get; set; } = default!;
        public EmployedService EmployedService { get; }

        public async Task OnGetAsync()
        {
            EmployedDetails = await EmployedService.GetEmployedsDetalsAsync();
        }
    }
}
