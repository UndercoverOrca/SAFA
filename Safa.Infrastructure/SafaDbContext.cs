using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Safa.Domain;

namespace Safa.Infrastructure;

public class SafaDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public SafaDbContext(DbContextOptions<SafaDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<UserEntity>()
            .HasMany(x => x.Transactions)
            .WithOne(x => x.);
        
        base.OnModelCreating(builder);
    }
}