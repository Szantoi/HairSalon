using HairSalon.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Bll.Models
{
    public class DutyTimeDetails
    {
        public int Id { get; set; }

        [Display(Name = "Description", ResourceType = typeof(IResources))]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Type", ResourceType = typeof(IResources))]
        public DutyTimeType Type { get; set; }

        [Required]
        [Display(Name = "TimeStart", ResourceType = typeof(IResources))]
        public DateTime TimeStart { get; set; }

        [Required]
        [Display(Name = "TimeEnd", ResourceType = typeof(IResources))]
        public DateTime TimeEnd { get; set; }

        [Required]
        [Display(Name = "EnployedId", ResourceType = typeof(IResources))]
        public int EnployedId { get; set; }
    }
}
