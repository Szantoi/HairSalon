using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Localization.Resources
{
    public class Resources : IResources
    {
        public string Name { get; set;} = "Name";

        public string Description { get; set;} = "Description";

        public string Language { get; set;} = "Language";

        public string TimeStart { get; set;} = "Time Start";

        public string TimeEnd { get; set;} = "Time end";

        public string Type { get; set;} = "Type";

        public string EnployedId { get; set;} = "Enployed Id";

        public string Email { get; set;} = "Email";

        public string FullName { get; set;} = "Full name";

        public string ProfilePicture { get; set;} = "Profile picture";

        public string Author { get; set;} = "Author";

        public string AppointmentId { get; set;} = "Appointment Id";

        public string Price { get; set;} = "Price";

        public string PhoneNumber { get; set; }

        public string Picture { get; set;} = "Picture";

        public string PeriodMinute { get; set;} = "Period minute";

        public string HairSalonUserId { get; set;} = "Hair salon user Id";

        public string DutyTimeId { get; set;} = "Duty time Id";

        public string Title { get; set;} = "Title";

        public string ShopServiceId { get; set;} = "Shop service Id";
    }
}
