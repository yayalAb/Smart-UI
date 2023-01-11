
using FluentValidation;

namespace Application.CompanyModule.Commands.CreateCompanyCommand
{

    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {

            RuleFor(u => u.Name)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("name is not in the correct format!");
            RuleFor(u => u.TinNumber)
                .MaximumLength(45)
                .WithMessage("tin number is not in the correct format!");
            RuleFor(u => u.CodeNIF)
                .MaximumLength(45)
                .WithMessage("codenif is not in the correct format!");
            // RuleFor(u => u.address)
            //     .NotNull();
            // RuleFor(u => u.address.Email)
            //     .EmailAddress()
            //     .WithMessage("invalid email address");

            // RuleFor(u => u.ContactPeople!.Select(cp => cp.Name))
            //     .NotNull()
            //     .NotEmpty();
            // RuleFor(u => u.ContactPeople!.Select(cp => cp.Email))
            //    .ForEach(em => em.EmailAddress().WithMessage("invalid contact person email address"));

            // RuleFor(u => u.ContactPeople!.Select(cp => cp.Phone))
            //     .NotNull()
            //     .NotEmpty();
            // RuleFor(u => u.ContactPeople!.Select(cp => cp.TinNumber))
            //     .NotNull()
            //     .NotEmpty();

            // RuleFor(u => u.BankInformation)
            //     .NotNull();
            // RuleFor(u => u.BankInformation.Select(bi => bi.AccountHolderName))
            //     .NotNull();
            // RuleFor(u => u.BankInformation.Select(bi => bi.AccountNumber))
            //     .NotNull();
            // RuleFor(u => u.BankInformation.Select(bi => bi.BankAddress))
            //     .NotNull();
            // RuleFor(u => u.BankInformation.Select(bi => bi.BankName))
            //     .NotNull();
            // RuleFor(u => u.BankInformation.Select(bi => bi.SwiftCode))
            //     .NotNull();
            // RuleFor(u => u.BankInformation.Select(bi => bi.CurrencyType))
            //     .NotNull();

        }

    }

}