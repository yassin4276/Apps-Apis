using Microsoft.EntityFrameworkCore;
using Apps_Apis;

namespace Apps_Apis.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Application> Applications { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure composite key for AppRole (ApplicationId, RoleId)
        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.HasKey(ar => new { ar.ApplicationId, ar.RoleId });
            entity.Property(ar => ar.Email).HasMaxLength(256);
            entity.Property(ar => ar.PasswordHash).HasMaxLength(500);
        });

        // Configure relationships for AppRole
        modelBuilder.Entity<AppRole>()
            .HasOne(ar => ar.Application)
            .WithMany(a => a.AppRoles)
            .HasForeignKey(ar => ar.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AppRole>()
            .HasOne(ar => ar.Role)
            .WithMany(r => r.AppRoles)
            .HasForeignKey(ar => ar.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Optional: configure string column lengths for Application
        modelBuilder.Entity<Application>(entity =>
        {
            entity.Property(a => a.Name).HasMaxLength(200);
            entity.Property(a => a.Description).HasMaxLength(1000);
            entity.Property(a => a.Image).HasMaxLength(500);
            entity.Property(a => a.Url).HasMaxLength(500);
            entity.Property(a => a.Stack).HasMaxLength(200);
        });

        // Optional: configure Role Name
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(r => r.Name).HasMaxLength(100);
        });
    }
}
