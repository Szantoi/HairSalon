using HairSalon.Bll.Models;
using HairSalon.Bll.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HairSalon.Web.Pages.Admin.DutyTime
{
    public class IndexModel : PageModel
    {
        public IndexModel(DutyTimeService dutyTimeService, EmployedService employedService)
        {
            DutyTimeService = dutyTimeService;
            EmployedService = employedService;
        }

        public IList<EmployedDetails> Employeds { get; private set; }
        public IList<DutyTimeDetails> DutyTimeDetails { get; set; } = default!;
        public DutyTimeService DutyTimeService { get; }
        public EmployedService EmployedService { get; }

        public async Task OnGetAsync()
        {
            Employeds = await EmployedService.GetEmployedsDetalsAsync();
            DutyTimeDetails = await DutyTimeService.GetDutyTimesDetalsAsync();
        }
    }
}
