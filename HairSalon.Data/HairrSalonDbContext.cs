using HairSalon.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HairSalon.Data
{
    public class HairSalonDbContext : IdentityDbContext<HairSalonUser, IdentityRole<int>, int>
    {
        public HairSalonDbContext(DbContextOptions<HairSalonDbContext> options) :base(options) { }
        
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DutyTime> DutyTimes { get; set; }
        public DbSet<Employed> Employeds { get; set; }
        public DbSet<HairSalonUser> HairSalonUsers { get; set;}
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ShopService> ShopServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
