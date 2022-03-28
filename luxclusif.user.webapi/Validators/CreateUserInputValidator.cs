using FluentValidation;
using luxclusif.user.application.UseCases.User.CreateUser;
using luxclusif.user.domain.Validation;

namespace luxclusif.user.webapi.Validators
{
    public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
    {
        public CreateUserInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationConstant.RequiredParameter);

            RuleFor(x => x.Name)
                .Length(3,100)
                .WithMessage(ValidationConstant.RequiredParameter);
        }
    }
}
