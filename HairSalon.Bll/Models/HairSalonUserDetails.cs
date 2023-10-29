using HairSalon.Localization.Resources;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Bll.Models
{
    public class HairSalonUserDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "FullName", ResourceType = typeof(IResources))]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(IResources))]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(IResources))]
        public string PhoneNumber { get; set; }
    }
}
