using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon.Data.Entities
{
    public class ShopService : IEntityTypeConfiguration<ShopService>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int PeriodMinute { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        public void Configure(EntityTypeBuilder<ShopService> builder)
        {
            builder.Property(s => s.Description).HasMaxLength(500);
            builder.Property(s => s.Name).HasMaxLength(100);

        }
    }
}
