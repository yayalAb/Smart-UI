
using FluentValidation;

namespace Application.CompanyModule.Commands.CreateCompanyCommand {

    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand> {
        
        public CreateCompanyCommandValidator(){
            
            RuleFor(u => u.Name)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("name is not in the correct format!");
            RuleFor(u => u.TinNumber)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("tin number is not in the correct format!");
            RuleFor(u => u.CodeNIF)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("codenif is not in the correct format!");
            RuleFor( u => u.address)
                .NotNull();
            RuleFor(u => u.address.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("invalid email address");
            When(u => u.contactPerson != null, () =>
            {
                RuleFor( u => u.contactPerson!.Name)
                    .NotNull()
                    .NotEmpty(); 
                RuleFor( u => u.contactPerson!.Email)
                    .NotNull()
                    .NotEmpty()
                    .EmailAddress()
                    .WithMessage("invalid contact person email address");                
                RuleFor( u => u.contactPerson!.Phone)
                    .NotNull()
                    .NotEmpty(); 
                RuleFor(u => u.contactPerson!.TinNumber)
                    .NotNull()
                    .NotEmpty();
                
            });
            RuleFor( u => u.BankInformation)
                .NotNull();
            RuleFor( u => u.BankInformation.Select(bi => bi.AccountHolderName))
                .NotNull();
            RuleFor( u => u.BankInformation.Select(bi => bi.AccountNumber))
                .NotNull();
            RuleFor( u => u.BankInformation.Select(bi => bi.BankAddress))
                .NotNull();
            RuleFor( u => u.BankInformation.Select(bi => bi.BankName))
                .NotNull();
            RuleFor( u => u.BankInformation.Select(bi => bi.SwiftCode))
                .NotNull();
            

            
        }

    }

}