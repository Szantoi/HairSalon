using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace HairSalon.Data.Entities
{
    public class HairSalonUser : IdentityUser<int>, IEntityTypeConfiguration<HairSalonUser>
    {
        public string FullName { get; set; }

        //[Timestamp]
        //public byte[] Version { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        public void Configure(EntityTypeBuilder<HairSalonUser> builder)
        {
            builder.ToTable("HairSalonUser");
            builder.Property(h => h.FullName).HasMaxLength(100);
        }
    }
}
