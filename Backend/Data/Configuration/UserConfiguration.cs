
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GYMIND.API.Data.Configuration
{

    public class UserConfiguration : IEntityTypeConfiguration<Entities.User>
    {
        public void Configure(EntityTypeBuilder<Entities.User> entity)
        {

            entity.ToTable("users"); // map to table
            entity.HasKey(u => u.UserID); // primary key

            // Columns
            entity.Property(u => u.UserID).HasColumnName("userid");
            entity.Property(u => u.FullName).HasColumnName("fullname");
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.Phone).HasColumnName("phone");
            entity.Property(u => u.PasswordHash).HasColumnName("passwordhash");

            entity.Property(u => u.Location).HasColumnName("location");
            entity.Property(u => u.DateOfBirth).HasColumnName("dateofbirth");
            entity.Property(u => u.MembershipID).HasColumnName("membershipid");
            entity.Property(u => u.Gender).HasColumnName("gender");
            entity.Property(u => u.CreatedAt).HasColumnName("createdat");
            entity.Property(u => u.IsActive).HasColumnName("isactive").HasDefaultValue(true);

            // Relationships
            entity.HasMany(u => u.UserRole)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
