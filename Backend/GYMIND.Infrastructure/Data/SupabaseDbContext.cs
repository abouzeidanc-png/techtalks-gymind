using Microsoft.EntityFrameworkCore;
using GYMIND.API.Entities;
using System.Text.Json;

public class SupabaseDbContext : DbContext
{
    public SupabaseDbContext(DbContextOptions<SupabaseDbContext> options)
        : base(options) { }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Gym> Gyms => Set<Gym>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<GymBranch> GymBranches => Set<GymBranch>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<GymSession> GymSessions => Set<GymSession>();
    public DbSet<TrafficTrack> TrafficTracks => Set<TrafficTrack>();
    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<UserNotification> UserNotifications => Set<UserNotification>();
    public DbSet<GymAdminAction> GymAdminActions => Set<GymAdminAction>();
    public DbSet<SystemAdminAction> SystemAdminActions => Set<SystemAdminAction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<UserRole>()
            .HasIndex(ur => new { ur.UserID, ur.RoleID })
            .IsUnique();

        modelBuilder.Entity<UserNotification>()
            .HasKey(un => new { un.UserID, un.NotificationID });

        modelBuilder.Entity<GymBranch>()
            .Property(gb => gb.OperatingHours)
            .HasColumnType("jsonb");

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users"); // table name

            entity.Property(u => u.UserID).HasColumnName("userid");
            entity.Property(u => u.FullName).HasColumnName("fullname");
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.Phone).HasColumnName("phone");
            entity.Property(u => u.PasswordHash).HasColumnName("passwordhash");
            entity.Property(u => u.Location).HasColumnName("location");
            entity.Property(u => u.DateOfBirth).HasColumnName("dateofbirth");
            entity.Property(u => u.MembershipID).HasColumnName("membershipid");
            entity.Property(u => u.Gender).HasColumnName("gender");
            entity.Property(u => u.RoleID).HasColumnName("roleid");
            entity.Property(u => u.CreatedAt).HasColumnName("createdat");
            entity.Property(u => u.IsActive).HasColumnName("isactive");
        });

    }
}
