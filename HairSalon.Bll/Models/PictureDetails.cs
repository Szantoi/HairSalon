using HairSalon.Localization.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Bll.Models
{
    public class PictureDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(IResources))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(IResources))]
        public string? Description { get; set; }

        [Display(Name = "Author", ResourceType = typeof(IResources))]
        public string? Author { get; set; }

        [Required]
        [Display(Name = "Type", ResourceType = typeof(IResources))]
        public PictureType Type { get; set; }

        [Display(Name = "AppointmentId", ResourceType = typeof(IResources))]
        public int? AppointmentId { get; set; }
    }
}
