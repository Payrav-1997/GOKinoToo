using Kino.Enums;
using Kino.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kino.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var users = new List<User>
        {
            new()
            {
                Id = new Guid("add32b35-30c0-4cb7-bf30-5c884a980e11"),
                Name = "Admin",
                Email = "Admin@gmail.com",
                RoleId = Role.Admin,
                CreateDate = DateTimeOffset.UtcNow,
                Password = BCrypt.Net.BCrypt.HashPassword("admin")
            }

        };
        builder.HasData(users);
    }
}