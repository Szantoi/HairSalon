using HairSalon.Bll.Models;
using HairSalon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HairSalon.Bll.Services
{
    public class HairSalonUserService
    {
        public HairSalonUserService(HairSalonDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public HairSalonDbContext DbContext { get; }

        private readonly Expression<Func<Data.Entities.HairSalonUser, HairSalonUserDetails>> _HairSalonUserSelektor = h => new HairSalonUserDetails
        {
            Id = h.Id,
            FullName = h.FullName,
            Email = h.Email,
            PhoneNumber = h.PhoneNumber

        };

        public async Task<HairSalonUserDetails> GetHairSalonUserDetalsAsync(int id)
        {
            return await DbContext.HairSalonUsers
                .Where(h => h.Id == id)
                .Select(_HairSalonUserSelektor)
                .SingleAsync();
        }
        public async Task<IList<HairSalonUserDetails>> GetHairSalonUsersDetalsAsync()
        {
            return await DbContext.HairSalonUsers
                .OrderBy(h => h.FullName)
                .Select(_HairSalonUserSelektor)
                .ToListAsync();
        }
        public async Task DelletHairSalonUserAsync(int id)
        {
            DbContext.HairSalonUsers.Remove(new Data.Entities.HairSalonUser { Id = id });
            await DbContext.SaveChangesAsync();
        }
        public async Task AddOrUopdateHairSalonUserAsync(HairSalonUserDetails hairSalonUser)
        {
            EntityEntry<Data.Entities.HairSalonUser> entry;

            if (hairSalonUser.Id != 0)
                entry = DbContext.Entry(await DbContext.HairSalonUsers.FindAsync(hairSalonUser.Id));
            else
                entry = DbContext.Add(new Data.Entities.HairSalonUser());

            entry.CurrentValues.SetValues(hairSalonUser);
            await DbContext.SaveChangesAsync();
        }
    }
}
