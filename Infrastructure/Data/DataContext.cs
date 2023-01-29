using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Premission> Premissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePremission> RolePremissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RolePremission>()
            .HasKey(es => new { es.RoleId, es.PremissionId });
        base.OnModelCreating(modelBuilder);
   
        modelBuilder.Entity<UserRole>()
            .HasKey(es => new { es.RoleId, es.UserId });
        base.OnModelCreating(modelBuilder);
    }
}
