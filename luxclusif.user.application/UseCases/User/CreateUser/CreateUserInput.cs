using MediatR;

namespace luxclusif.user.application.UseCases.User.CreateUser;
public class CreateUserInput : IRequest<CreateUserOutput>
{
    public string Name { get; set; }

    public CreateUserInput(string name)
    {
        Name = name;
    }
}
