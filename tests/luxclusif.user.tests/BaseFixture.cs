using Bogus;
using DomainEntity = luxclusif.user.domain.Entity;


namespace luxclusif.user.tests;
public class BaseFixture
{
    public BaseFixture()
    => Faker = new Faker();

    public Faker Faker { get; set; }

    public string GetStringRigthSize(int minLength, int maxlength)
    {
        var userName = "";
        while (userName.Length < minLength)
        {
            userName = Faker.Person.FullName;
        }

        if (userName.Length > maxlength)
        {
            userName = userName[..maxlength];
        }

        return userName;
    }

    public string GetValidUserName()
    {
        return GetStringRigthSize(3,100);
    }

    public virtual DomainEntity.User GetValidUser()
    {
        return new DomainEntity.User(
            GetValidUserName()
            );
    }
}
