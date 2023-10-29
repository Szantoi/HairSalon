using HairSalon.Data.Entities;
using HairSalon.Localization.Resources;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Bll.Models
{
    public class AppointmentDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title", ResourceType = typeof(IResources))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(IResources))]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "TimeStart", ResourceType = typeof(IResources))]
        public DateTime TimeStart { get; set; }

        [Required]
        [Display(Name = "TimeEnd", ResourceType = typeof(IResources))]
        public DateTime TimeEnd { get; set; }


        [Display(Name = "HairSalonUserId", ResourceType = typeof(IResources))]
        public int? HairSalonUserId { get; set; }

        [Required]
        [Display(Name = "DutyTimeId", ResourceType = typeof(IResources))]
        public int DutyTimeId { get; set; }

        [Display(Name = "ShopServiceId", ResourceType = typeof(IResources))]
        public int? ShopServiceId { get; set; }

    }
}
