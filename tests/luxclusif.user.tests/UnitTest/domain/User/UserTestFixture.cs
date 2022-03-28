using Bogus;
using Xunit;
using DomainEntity = luxclusif.user.domain.Entity;

namespace luxclusif.user.tests.UnitTest.domain.User;
public class UserTestFixture : UnitBaseFixture
{
    [CollectionDefinition(nameof(UserTestFixture))]
    public class UserTestFixtureCollection : ICollectionFixture<UserTestFixture>
    {

    }
}