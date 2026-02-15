using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GYMIND.API.Data.Configuration
{
    public class GymConfiguration : IEntityTypeConfiguration<Entities.Gym>
    {
        public void Configure(EntityTypeBuilder<Entities.Gym> entity)
        {
            entity.ToTable("gym");
            entity.HasKey(g => g.GymId);

            entity.Property(g => g.GymId).HasColumnName("gymid");

            entity.Property(g => g.Name).HasMaxLength(255).IsRequired().HasColumnName("name");
            entity.Property(g => g.Address).HasMaxLength(500).IsRequired().HasColumnName("address");
            entity.Property(g => g.IsApproved).HasColumnName("isapproved").HasDefaultValue(false);
            entity.Property(g => g.CreatedAt).HasColumnName("createdat").HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.HasMany(g => g.Branches)
                .WithOne(gb => gb.Gym)
                .HasForeignKey(gb => gb.GymID);

        }
    }

    public class GymBranchConfiguration : IEntityTypeConfiguration<Entities.GymBranch>
    {
        public void Configure(EntityTypeBuilder<Entities.GymBranch> entity)
        {
            entity.ToTable("gymbranch");
            entity.HasKey(gb => gb.GymBranchID);

            // Mapping JsonDocument for OperatingHours
            entity.Property(gb => gb.OperatingHours).HasColumnType("jsonb");
            
            entity.Property(gb => gb.GymID).HasColumnName("gymid");
            entity.Property(gb => gb.LocationID).HasColumnName("locationid");
            entity.Property(gb => gb.Name).HasMaxLength(255).IsRequired().HasColumnName("name");
            entity.Property(gb => gb.ServiceDescription).HasMaxLength(1000).HasColumnName("servicedescription");
            entity.Property(gb => gb.CoverImageUrl).HasMaxLength(500).HasColumnName("coverimageurl");
            entity.Property(gb => gb.IsActive).HasColumnName("isactive").HasDefaultValue(true);

            entity.HasOne(gb => gb.Gym)
                .WithMany(g => g.Branches) 
                .HasForeignKey(gb => gb.GymID);

            entity.HasOne(gb => gb.Location)
                .WithMany()
                .HasForeignKey(gb => gb.LocationID);
        }
    }
}