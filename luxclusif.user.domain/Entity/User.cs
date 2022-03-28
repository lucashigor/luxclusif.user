using luxclusif.user.domain.SeedWork;
using luxclusif.user.domain.Validation;

namespace luxclusif.user.domain.Entity;
public class User : AgregateRoot
{
    public string Name { get; private set; }

    public User(string name) : base ()
    {
        this.Name = name;

        this.Validate();
    }

    protected override void Validate()
    {
        Name.NotNullOrEmptyOrWhiteSpace();
        Name.BetweenLength(3, 100);

        base.Validate();
    }
}
