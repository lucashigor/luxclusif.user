using luxclusif.user.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace luxclusif.user.tests.Integrations.Persintence;

[CollectionDefinition(nameof(IntegrationDataBaseTestFixture))]
public class IntegrationDataBaseTestCollection : ICollectionFixture<IntegrationDataBaseTestFixture>
{

}

public class IntegrationDataBaseTestFixture : IntegrationsBaseFixture
{
    public DbContextOptions<PrincipalContext> DbContextOptions { get; }

    public IntegrationDataBaseTestFixture()
    {
        DbContextOptions = new DbContextOptionsBuilder<PrincipalContext>()
            .UseInMemoryDatabase("IntegrationTestDatabase")
            .Options;
    }

    public PrincipalContext GetDatabase()
        => new(DbContextOptions);

    public void ResetDatabase()
    {
        using PrincipalContext context = new(DbContextOptions);
        context.User.RemoveRange(context.User);
        context.SaveChanges();
    }
}
