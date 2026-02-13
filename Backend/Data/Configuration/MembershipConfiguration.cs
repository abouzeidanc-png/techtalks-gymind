using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GYMIND.API.Data.Configuration
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Entities.Membership>
    {
        public void Configure(EntityTypeBuilder<Entities.Membership> entity)
        {
            entity.ToTable("memberships");
            entity.HasKey(m => m.MembershipID);
            entity.Property(n => n.GymBranchID).HasColumnName("gymbranchid");

            entity.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserID);

            entity.HasIndex(m => m.GymID);
        }
    }
}
