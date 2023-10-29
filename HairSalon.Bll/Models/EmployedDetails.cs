using HairSalon.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Bll.Models
{
    public class EmployedDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(IResources))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(IResources))]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Type", ResourceType = typeof(IResources))]
        public EmployedType Type { get; set; }
    }
}
