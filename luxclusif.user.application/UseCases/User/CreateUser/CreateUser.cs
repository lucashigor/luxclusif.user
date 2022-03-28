using luxclusif.user.application.Constants;
using luxclusif.user.application.Interfaces;
using luxclusif.user.application.Models;
using luxclusif.user.domain.Exceptions;
using luxclusif.user.domain.Repository;
using MediatR;
using DomainEntity = luxclusif.user.domain.Entity;

namespace luxclusif.user.application.UseCases.User.CreateUser;
public class CreateUser :
    IRequestHandler<CreateUserInput, CreateUserOutput>
{
    public readonly IUserRepository userRepository;
    public readonly IUnitOfWork unityOfWork;
    public readonly Notifier notifier;
    public readonly IMediator mediator;

    private readonly string EventName = "topic.createduser";

    public CreateUser(IUserRepository userRepository,
        IUnitOfWork unityOfWork,
        Notifier notifier,
        IMediator mediator)
    {
        this.unityOfWork = unityOfWork;
        this.userRepository = userRepository;
        this.notifier = notifier;
        this.mediator = mediator;
    }

    public async Task<CreateUserOutput> Handle(CreateUserInput userInput, CancellationToken cancellationToken)
    {
        DomainEntity.User entity;

        try
        {
            entity = new DomainEntity.User(userInput.Name);
        }
        catch (EntityGenericException)
        {
            notifier.Erros.Add(ErrorCodeConstant.Validation);

            return null!;
        }

        try
        {
            await userRepository.Insert(entity, cancellationToken);
            
            var message = new DefaultMessageNotification(EventName, entity);

            await mediator.Publish(message);

            await unityOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            notifier.Erros.Add(ErrorCodeConstant.ErrorOnSavingNewUser);

            return null!;
        }

        return CreateUserOutput.FromUser(entity);
    }
}
