using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Infra.Contexts.AccountContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Infra.Contexts.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    { }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}