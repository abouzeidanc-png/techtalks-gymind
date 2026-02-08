using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GYMIND.API.Data.Configuration
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Entities.Announcement>
    {
        public void Configure(EntityTypeBuilder<Entities.Announcement> entity)
        {
            entity.ToTable("announcements");
            entity.HasKey(a => a.AnnouncementID);
            entity.Property(a => a.AnnouncementID).HasColumnName("announcementid");

            entity.HasOne(a => a.GymBranch)
                .WithMany()
                .HasForeignKey(a => a.GymBranchID);
        }
    }
}
