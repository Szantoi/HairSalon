using HairSalon.Bll.Models;

namespace HairSalon.Web.Pages.Admin.Appointment.CreateAndEdit
{
    public class AppointentInputModel
    {
        public int? Id { get; set; }
        public AppointmentDetails AppointmentDetails { get; set; }
        public EmployedDetails EmployedDetails { get; set; }
    }
}
