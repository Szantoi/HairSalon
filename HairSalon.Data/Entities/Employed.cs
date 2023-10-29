using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HairSalon.Data.Entities
{
    public class Employed : IEntityTypeConfiguration<Employed>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public EmployedType Type { get; set; }

        public ICollection<DutyTime>? DutyTimes { get; set; }

        public ICollection<Picture>? Pictures { get; set; }

        public void Configure(EntityTypeBuilder<Employed> builder)
        {
            builder.Property(d => d.Description).HasMaxLength(500);

        }
    }
}
