using HairSalon.Localization.Resources;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Bll.Models
{
    public class ShopServiceDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(IResources))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(IResources))]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Price", ResourceType = typeof(IResources))]
        public int Price { get; set; }

        [Required]
        [Display(Name = "PeriodMinute", ResourceType = typeof(IResources))]
        public int PeriodMinute { get; set; }
    }
}
