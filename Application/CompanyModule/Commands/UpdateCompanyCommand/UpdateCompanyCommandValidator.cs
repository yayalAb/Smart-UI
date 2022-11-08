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
                .NotEmpty();
            RuleFor(u => u.TinNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.CodeNIF)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty();

        }
    }
}