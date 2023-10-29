using HairSalon.Bll.Models;
using HairSalon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Bll.Services
{
    public class DutyTimeService
    {
        public DutyTimeService(HairSalonDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public HairSalonDbContext DbContext { get; }

        private readonly Expression<Func<Data.Entities.DutyTime, DutyTimeDetails>> _DutyTimeSelektor = d => new DutyTimeDetails
        {
            Id = d.Id,
            TimeStart = d.TimeStart,
            TimeEnd = d.TimeEnd,
            Description = d.Description,
            EnployedId = d.EnployedId,
            Type = d.Type
        };

        public async Task<DutyTimeDetails> GetDutyTimeDetalsAsync(int id)
        {
            return await DbContext.DutyTimes
                .Where(d => d.Id == id)
                .Select(_DutyTimeSelektor)
                .SingleAsync();
        }
        public async Task<IList<DutyTimeDetails>> GetDutyTimesDetalsAsync()
        {
            return await DbContext.DutyTimes
                .OrderBy(d => d.TimeStart)
                .Select(_DutyTimeSelektor)
                .ToListAsync();
        }
        public async Task DelletDutyTimeAsync(int id)
        {
            DbContext.DutyTimes.Remove(new Data.Entities.DutyTime { Id = id });
            await DbContext.SaveChangesAsync();
        }
        public async Task AddOrUopdateDutyTimeAsync(DutyTimeDetails dutyTime)
        {
            EntityEntry<Data.Entities.DutyTime> entry;

            if (dutyTime.Id != 0)
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                entry = DbContext.Entry(await DbContext.DutyTimes.FindAsync(dutyTime.Id));
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            else
                entry = DbContext.Add(new Data.Entities.DutyTime());

            entry.CurrentValues.SetValues(dutyTime);
            await DbContext.SaveChangesAsync();
        }
    }
}
