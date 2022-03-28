using luxclusif.user.application.Interfaces;
using luxclusif.user.application.Models;
using luxclusif.user.application.UseCases.User.CreateUser;
using luxclusif.user.domain.Repository;
using MediatR;
using Moq;
using Xunit;

namespace luxclusif.user.tests.UnitTest.Application.CreateUser;
public class CreateUserTestsFixture : UnitBaseFixture
{
    public Mock<IUserRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
    public Mock<IMediator> GetMediator() => new();
    public Mock<Notifier> GetNotifier() => new();

    public CreateUserInput GetUserCreateInput()
    {
        return new CreateUserInput(
            GetValidUserName()
            );
    }

    [CollectionDefinition(nameof(CreateUserTestsFixture))]
    public class UserTestFixtureCollection : ICollectionFixture<CreateUserTestsFixture>
    {

    }
}
