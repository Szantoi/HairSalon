using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Localization.Resources
{
    public interface IResources
    {
        public static string Name { get; set;}
        public static string Description { get; set;}
        public static string Language { get; set;}
        public static string TimeStart { get; set;}
        public static string TimeEnd { get; set;}
        public static string Title { get; set;}
        public static string Type { get; set;}
        public static string EnployedId { get; set;}
        public static string Email { get; set; }
        public static string FullName { get; set;}
        public static string ProfilePicture { get; set;}
        public static string Author { get; set;}
        public static string AppointmentId { get; set;}
        public static string Price { get; set;}
        public static string PhoneNumber { get; set; }
        public static string Picture { get; set;}
        public static string PeriodMinute { get; set;}
        public static string HairSalonUserId { get; set; }
        public static string DutyTimeId { get; set; }
        public static string ShopServiceId { get; set; }

    }
}
