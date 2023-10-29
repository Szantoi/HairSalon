using HairSalon.Bll.Models;
using HairSalon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HairSalon.Bll.Services

{
    public class AppointmentService
    {
        public AppointmentService(HairSalonDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public HairSalonDbContext DbContext { get; }


        private readonly Expression<Func<Data.Entities.Appointment, AppointmentDetails>> _AppointmentSelektor = a => new AppointmentDetails
        {
            Id = a.Id,
            Title = a.Title,
            TimeEnd = a.TimeEnd,
            TimeStart = a.TimeStart,
            Description = a.Description,
            HairSalonUserId = a.HairSalonUserId,
            DutyTimeId = a.DutyTimeId,
            ShopServiceId = a.ShopServiceId

        };

        public async Task<AppointmentDetails> GetAppointmentDetalsAsync(int id)
        {
            return await DbContext.Appointments
                .Where(a => a.Id == id)
                .Select(_AppointmentSelektor)
                .SingleAsync();
        }
        public async Task<IList<AppointmentDetails>> GetAppointmentsDetalsAsync()
        {
            return await DbContext.Appointments
                .OrderBy(a => a.TimeStart)
                .Select(_AppointmentSelektor)
                .ToListAsync();
        }
        public async Task DelletAppointmentAsync(int id)
        {
            DbContext.Appointments.Remove(new Data.Entities.Appointment { Id = id });
            await DbContext.SaveChangesAsync();
        }
        public async Task AddOrUopdateAppointmentAsync(AppointmentDetails appointment)
        {
            EntityEntry<Data.Entities.Appointment> entry;

            if (appointment.Id != 0)
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                entry = DbContext.Entry(await DbContext.Appointments.FindAsync(appointment.Id));
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            else
                entry = DbContext.Add(new Data.Entities.Appointment());

            entry.CurrentValues.SetValues(appointment);
            await DbContext.SaveChangesAsync();
        }
    }
}
