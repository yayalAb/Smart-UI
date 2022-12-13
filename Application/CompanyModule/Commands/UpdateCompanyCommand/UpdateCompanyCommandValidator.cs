using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

namespace Application.CompanyModule.Commands.UpdateCompanyCommand
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand> {
        public UpdateCompanyCommandValidator(){
            
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
          
            RuleFor( u => u.ContactPeople!.Select(cp => cp.Name))
                .NotNull()
                .NotEmpty(); 
            RuleFor( u => u.ContactPeople!.Select(cp => cp.Email))
                .ForEach(em => em.EmailAddress().WithMessage("invalid contact person email address"));
             
            RuleFor( u => u.ContactPeople!.Select(cp => cp.Phone))
                .NotNull()
                .NotEmpty(); 
            RuleFor(u => u.ContactPeople!.Select(cp => cp.TinNumber))
                .NotNull()
                .NotEmpty();
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