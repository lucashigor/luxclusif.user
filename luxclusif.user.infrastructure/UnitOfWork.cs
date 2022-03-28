﻿using luxclusif.user.application.Interfaces;
using luxclusif.user.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace luxclusif.user.infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly PrincipalContext context;

    public UnitOfWork(PrincipalContext context)
    => this.context = context;

    public Task CommitAsync(CancellationToken cancellationToken)
        => context.SaveChangesAsync(cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        context.ChangeTracker.Entries()
    .Where(e => e.Entity != null).ToList()
    .ForEach(e => e.State = EntityState.Detached);

        return Task.CompletedTask;
    }
}
