using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GYMIND.API.Data.Configuration
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Entities.Membership>
    {
        public void Configure(EntityTypeBuilder<Entities.Membership> entity)
        {
            entity.ToTable("membership");
            entity.HasKey(m => m.MembershipID);

                //Add Gym relationship + map columns
            entity.Property(m => m.MembershipID).HasColumnName("membershipid");
            entity.Property(m => m.UserID).HasColumnName("userid");
            entity.Property(m => m.GymID).HasColumnName("gymid");


            entity.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserID);

            entity.HasOne(m => m.Gym)
                .WithMany()
                .HasForeignKey(m => m.GymID);

        }
    }
}
