using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon.Data.Entities
{
    public class Picture : IEntityTypeConfiguration<Picture>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public PictureType Type { get; set; }

        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Author).HasMaxLength(100);
            builder.Property(p => p.Name).HasMaxLength(100);

            builder.HasOne(p => p.Appointment).WithMany(a => a.Pictures)
                    .HasForeignKey(p => p.AppointmentId).HasPrincipalKey(a => a.Id);
        }
    }
}
