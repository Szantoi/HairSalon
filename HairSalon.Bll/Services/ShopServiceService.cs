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
    public class ShopServiceService
    {
        public ShopServiceService(HairSalonDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public HairSalonDbContext DbContext { get; }


        private readonly Expression<Func<Data.Entities.ShopService, ShopServiceDetails>> _ShopServiceSelektor = s => new ShopServiceDetails
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            PeriodMinute = s.PeriodMinute,
            Price = s.Price
        };

        public async Task<ShopServiceDetails> GetShopServiceDetalsAsync(int id)
        {
            return await DbContext.ShopServices
                .Where(s => s.Id == id)
                .Select(_ShopServiceSelektor)
                .SingleAsync();
        }
        public async Task<IList<ShopServiceDetails>> GetShopServicesDetalsAsync()
        {
            return await DbContext.ShopServices
                .OrderBy(s => s.Name)
                .Select(_ShopServiceSelektor)
                .ToListAsync();
        }
        public async Task DelletShopServiceAsync(int id)
        {
            DbContext.ShopServices.Remove(new Data.Entities.ShopService { Id = id });
            await DbContext.SaveChangesAsync();
        }
        public async Task AddOrUopdateShopServiceAsync(ShopServiceDetails shopService)
        {
            EntityEntry<Data.Entities.ShopService> entry;

            if (shopService.Id != 0)
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                entry = DbContext.Entry(await DbContext.ShopServices.FindAsync(shopService.Id));
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            else
                entry = DbContext.Add(new Data.Entities.ShopService());

            entry.CurrentValues.SetValues(shopService);
            await DbContext.SaveChangesAsync();
        }
    }
}
