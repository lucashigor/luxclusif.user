﻿using FluentAssertions;
using luxclusif.user.domain.Conts;
using luxclusif.user.domain.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;
using DomainEntity = luxclusif.user.domain.Entity;

namespace luxclusif.user.tests.UnitTest.domain.User;

[Collection(nameof(UserTestFixture))]
public class UserTest
{
    private readonly UserTestFixture fixture;

    public UserTest(UserTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact(DisplayName = nameof(Instatiate))]
    [Trait("Domain", "User - Aggregates")]
    public void Instatiate()
    {
        //Arrange
        var validData = fixture.GetValidUser();

        //Act
        var datetimeBefore = DateTime.UtcNow;

        var user = new DomainEntity.User(validData.Name);

        var datetimeAfter = DateTime.UtcNow.AddSeconds(1);

        //Assert
        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.LastUpdateAt.Should().BeNull();
        user.DeletedAt.Should().BeNull();
        (user.CreatedAt > datetimeBefore).Should().BeTrue();
        (user.CreatedAt < datetimeAfter).Should().BeTrue();
    }

    [Fact(DisplayName = nameof(InstatiateRequeridFieldKeyNull))]
    [Trait("Domain", "User - Aggregates")]
    public void InstatiateRequeridFieldKeyNull()
    {
        //Arrange

        //Act
        Action action =
            () => new DomainEntity.User(null!);

        //Assert
        var msg = ErrorsMessages.NotNull.GetMessage(nameof(DomainEntity.User.Name));

        action.Should().Throw<EntityGenericException>()
            .WithMessage(msg);
    }


    public static IEnumerable<object[]> TestBetweenValue(int minLength, int maxLength, string fieldName)
    {        
        //min
        var fixture = new UserTestFixture();

        var invalidInputsList = new List<object[]>();

        var invalidInputShortName = fixture.Faker.Commerce.ProductName();
        invalidInputShortName = invalidInputShortName[..(minLength -1)];
        invalidInputsList.Add(new object[] {
            invalidInputShortName,
        ErrorsMessages.BetweenLength.GetMessage(fieldName,minLength,maxLength)});

        //max
        var tooLongValue = fixture.Faker.Commerce.ProductName();
        while (tooLongValue.Length <= maxLength)
        {
            tooLongValue = $"{tooLongValue} {fixture.Faker.Commerce.ProductName()}";
        }

        invalidInputsList.Add(new object[] {
            tooLongValue,
        ErrorsMessages.BetweenLength.GetMessage(fieldName,minLength,maxLength)});

        return invalidInputsList;
    }

    [Theory(DisplayName = nameof(InstatiateLenghtFieldNameBetweenNotValid))]
    [Trait("Domain", "User - Aggregates")]
    [MemberData(nameof(TestBetweenValue), 3, 100, nameof(DomainEntity.User.Name))]
    public void InstatiateLenghtFieldNameBetweenNotValid(string name, string msg)
    {
        //Arrange

        //Act
        Action action =
            () => new DomainEntity.User(name);

        //Assert
        action.Should().Throw<EntityGenericException>()
            .WithMessage(msg);
    }
}
