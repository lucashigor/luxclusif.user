using luxclusif.user.domain.Entity;
using luxclusif.user.domain.SeedWork;

namespace luxclusif.user.domain.Repository;
public interface IUserRepository : IRepository
{
    public Task Insert(User user, CancellationToken cancellationToken);
}
