using HairSalon.Bll.Models;
using HairSalon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HairSalon.Bll.Services
{
    public class EmployedService
    {
        public EmployedService(HairSalonDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public HairSalonDbContext DbContext { get; }


        private readonly Expression<Func<Data.Entities.Employed, EmployedDetails>> _EmployedSelektor = e => new EmployedDetails
        {
            Id = e.Id,
            Name = e.Name,
            Type = e.Type,
            Description = e.Description,
        };

        public async Task<EmployedDetails> GetEmployedDetalsAsync(int id)
        {
            return await DbContext.Employeds
                .Where(e => e.Id == id)
                .Select(_EmployedSelektor)
                .SingleAsync();
        }
        public async Task<IList<EmployedDetails>> GetEmployedsDetalsAsync()
        {
            return await DbContext.Employeds
                .OrderBy(e => e.Name)
                .Select(_EmployedSelektor)
                .ToListAsync();
        }
        public async Task DelletEmployedAsync(int id)
        {
            DbContext.Employeds.Remove(new Data.Entities.Employed { Id = id });
            await DbContext.SaveChangesAsync();
        }
        public async Task AddOrUopdateEmployedAsync(EmployedDetails employed)
        {
            EntityEntry<Data.Entities.Employed> entry;

            if (employed.Id != 0)
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                entry = DbContext.Entry(await DbContext.Employeds.FindAsync(employed.Id));
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            else
                entry = DbContext.Add(new Data.Entities.Employed());

            entry.CurrentValues.SetValues(employed);
            await DbContext.SaveChangesAsync();
        }
    }
}
