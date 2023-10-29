using HairSalon.Bll.Models;
using HairSalon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HairSalon.Bll.Services
{
    public class PictureService
    {
        public PictureService(HairSalonDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public HairSalonDbContext DbContext { get; }


        private readonly Expression<Func<Data.Entities.Picture, PictureDetails>> _PictureSelektor = p => new PictureDetails
        {
            Id = p.Id,
            Type = p.Type,
            Name = p.Name,
            Description = p.Description,
            AppointmentId = p.AppointmentId,
            Author = p.Author
        };

        public async Task<PictureDetails> GetPictureDetalsAsync(int id)
        {
            return await DbContext.Pictures
                .Where(p => p.Id == id)
                .Select(_PictureSelektor)
                .SingleAsync();
        }
        public async Task<IList<PictureDetails>> GetPicturesDetalsAsync()
        {
            return await DbContext.Pictures
                .OrderBy(p => p.Type)
                .Select(_PictureSelektor)
                .ToListAsync();
        }
        public async Task DelletPictureAsync(int id)
        {
            DbContext.Pictures.Remove(new Data.Entities.Picture { Id = id });
            await DbContext.SaveChangesAsync();
        }
        public async Task AddOrUopdatePictureAsync(PictureDetails picture)
        {
            EntityEntry<Data.Entities.Picture> entry;

            if (picture.Id != 0)
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                entry = DbContext.Entry(await DbContext.Pictures.FindAsync(picture.Id));
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            else
                entry = DbContext.Add(new Data.Entities.Picture());

            entry.CurrentValues.SetValues(picture);
            await DbContext.SaveChangesAsync();
        }
    }
}
