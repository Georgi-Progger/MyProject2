using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProdASP.Models;
using System.Reflection.Emit;

namespace ProdASP.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Country> Places { get; set; } = null!;
    public DbSet<Republic> Republics { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Country>().HasData(
                new Country { Id = 1, NamePlace = "Россия", Language = "русский", Information = "Какой-то текстКакой-то текстКакой-то текстКакой-то текстКакой-то текстКакой-то текст", Image = @"/images/russia.jpg", }
            );
        builder.HasDefaultSchema("Identity");
        builder.Entity<IdentityUser>(entity =>
        {
            entity.ToTable(name: "User");
        });
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });

    }
}
