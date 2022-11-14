using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            
        }

    }

}