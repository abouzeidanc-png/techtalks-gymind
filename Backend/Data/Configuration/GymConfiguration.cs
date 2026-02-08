using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GYMIND.API.Data.Configuration
{
    public class GymConfiguration : IEntityTypeConfiguration<Entities.Gym>
    {
        public void Configure(EntityTypeBuilder<Entities.Gym> entity)
        {
            entity.ToTable("gyms");
            entity.HasKey(g => g.GymId);
            entity.Property(g => g.Name).HasMaxLength(255).IsRequired();
        }
    }

    public class GymBranchConfiguration : IEntityTypeConfiguration<Entities.GymBranch>
    {
        public void Configure(EntityTypeBuilder<Entities.GymBranch> entity)
        {
            entity.ToTable("gymbranches");
            entity.HasKey(gb => gb.GymBranchID);

            // Mapping JsonDocument for OperatingHours
            entity.Property(gb => gb.OperatingHours).HasColumnType("jsonb");

            entity.HasOne(gb => gb.Gym)
                .WithMany()
                .HasForeignKey(gb => gb.GymID);

            entity.HasOne(gb => gb.Location)
                .WithMany()
                .HasForeignKey(gb => gb.LocationID);
        }
    }
}
