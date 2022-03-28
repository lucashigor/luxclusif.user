using luxclusif.user.domain.Repository;
using luxclusif.user.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using DomainEntity = luxclusif.user.domain.Entity;

namespace luxclusif.user.infrastructure.Repositories;
public class UserRepository
    : IUserRepository
{
    private readonly PrincipalContext context;
    private DbSet<DomainEntity.User> users => context.Set<DomainEntity.User>();

    public UserRepository(PrincipalContext context)
    => this.context = context;


    public async Task Insert(DomainEntity.User user, CancellationToken cancellationToken)
    => await users.AddAsync(user, cancellationToken);
}
