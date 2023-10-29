using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon.Data.Entities
{
    public class Appointment : IEntityTypeConfiguration<Appointment>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }


        public int? HairSalonUserId { get; set; }
        public HairSalonUser? HairSalonUser { get; set; }

        public int DutyTimeId { get; set; }
        public DutyTime DutyTime { get; set; }

        public int? ShopServiceId { get; set; } 
        public ShopService? ShopService { get; set; }


        public ICollection<Picture> Pictures { get; set; }

        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(100);
            builder.Property(a => a.Description).HasMaxLength(500);

            builder.HasOne(a => a.HairSalonUser).WithMany(u => u.Appointments)
                    .HasForeignKey(a => a.HairSalonUserId).HasPrincipalKey(u => u.Id)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.DutyTime).WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DutyTimeId).HasPrincipalKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ShopService).WithMany(s => s.Appointments)
                    .HasForeignKey(a => a.ShopServiceId).HasPrincipalKey(s => s.Id)
                    .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
