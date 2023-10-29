using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalon.Data.Entities
{
    public class DutyTime : IEntityTypeConfiguration<DutyTime>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DutyTimeType Type { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        public int EnployedId { get; set; }
        public Employed Employed { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        public void Configure(EntityTypeBuilder<DutyTime> builder)
        {
            builder.Property(d => d.Description).HasMaxLength(500);

            builder.HasOne(d => d.Employed).WithMany(e => e.DutyTimes)
                    .HasForeignKey(d => d.EnployedId).HasPrincipalKey(e => e.Id);
        }
    }
}
