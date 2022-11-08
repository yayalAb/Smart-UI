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
                .NotEmpty();
            RuleFor(u => u.TinNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.CodeNIF)
                .NotNull()
                .NotEmpty();
            
        }

    }

}