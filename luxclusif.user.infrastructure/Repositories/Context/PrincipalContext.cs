using Microsoft.EntityFrameworkCore;
using DomainEntity = luxclusif.user.domain.Entity;

namespace luxclusif.user.infrastructure.Repositories.Context;
public class PrincipalContext : DbContext
{
    public PrincipalContext(DbContextOptions<PrincipalContext> options) : base(options)
    {

    }

    public DbSet<DomainEntity.User> User => Set<DomainEntity.User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<DomainEntity.User>((v) => {
                v.HasKey(k => k.Id);
                v.Property(k => k.Name).HasMaxLength(100);
                v.Property(k => k.CreatedAt);
                v.Property(k => k.LastUpdateAt);
                v.Property(k => k.DeletedAt);
            });
    }
}
