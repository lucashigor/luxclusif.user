using FluentAssertions;
using luxclusif.user.application.Models;
using luxclusif.user.application.UseCases.User.CreateUser;
using luxclusif.user.domain.Conts;
using luxclusif.user.domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using DomainEntity = luxclusif.user.domain.Entity;
using UseCases = luxclusif.user.application.UseCases.User.CreateUser;

namespace luxclusif.user.tests.UnitTest.Application.CreateUser;

[Collection(nameof(CreateUserTestsFixture))]
public class CreateUserTests
{
    private readonly CreateUserTestsFixture fixture;
    public CreateUserTests(CreateUserTestsFixture fixture)
    => this.fixture = fixture;

    [Fact(DisplayName = nameof(CreateUser))]
    [Trait("Application", "Create User - Use Cases")]
    public async void CreateUser()
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        var unitMediator = fixture.GetMediator();

        //Arrange

        var useCase = new UseCases.CreateUser(
            repositoryMock.Object,
            unitOfWorkMock.Object,
            new(),
            unitMediator.Object
            );

        var input = fixture.GetUserCreateInput();

        //Act
        var output = await useCase.Handle(input, CancellationToken.None);

        //Assert
        repositoryMock.Verify(
            rep => rep.Insert(
                It.IsAny<DomainEntity.User>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWorkMock.Verify(unit => unit.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once);


        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Id.Should().NotBe(System.Guid.Empty);
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }

    private static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateUserTestsFixture();

        var invalidInputsList = new List<object[]>();

        var invalidInputShortName = fixture.GetUserCreateInput();
        invalidInputShortName.Name = invalidInputShortName.Name[..2];
        invalidInputsList.Add(new object[] {
            invalidInputShortName,
        ErrorsMessages.BetweenLength.GetMessage(nameof(invalidInputShortName.Name),3,100)});

        var invalidInputTooLongName = fixture.GetUserCreateInput();

        var tooLongNameForUser = fixture.GetValidUserName();

        while (tooLongNameForUser.Length <= 100)
        {
            tooLongNameForUser = $"{tooLongNameForUser} {fixture.Faker.Person.UserName}";
        }

        invalidInputTooLongName.Name = tooLongNameForUser;

        invalidInputsList.Add(new object[] {
            invalidInputTooLongName,
        ErrorsMessages.BetweenLength.GetMessage(nameof(invalidInputShortName.Name),3,100)});

        return invalidInputsList;
    }
}
