

using FluentValidation;

namespace Application.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(a => a.Email)
                .EmailAddress();

        }
    }
}
