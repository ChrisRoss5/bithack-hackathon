using System;
using BddAPI.Enum;
using BddAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BddAPI.Data;

public class BddDbContext(DbContextOptions<BddDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<CommunityHome> CommunityCenters { get; init; }
    public DbSet<Reservation> Reservations { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<UserRole> UserRoles { get; init; }
    public DbSet<RefreshToken> RefreshTokens { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "server=localhost;Database=BjelovarDrustveniDomovi;User=SA;Password=reallyStrongPwd123;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = Guid.Parse("7a64bde5-8705-4ad5-8d69-1aadd2108a89"),
                Name = RoleType.User.ToString(),
                Description = "Superuser role with full access"
            },
            new Role
            {
                Id = Guid.Parse("c9b1f6c4-dce5-4b26-88b1-29b9b29f5d2b"),
                Name = RoleType.Janitor.ToString(),
                Description = "Administrator role with access to most of the features"
            },
            new Role
            {
                Id = Guid.Parse("de19b1a7-8dc3-49e2-b98f-9990a9f59118"),
                Name = RoleType.Mayor.ToString(),
                Description = "Regular user role with limited access"
            },
            new Role
            {
                Id = Guid.Parse("de19b1a7-8dc3-49e2-b98f-9990a9f59118"),
                Name = RoleType.CityOfficial.ToString(),
                Description = "Regular user role with limited access"
            }
        );
    }
}