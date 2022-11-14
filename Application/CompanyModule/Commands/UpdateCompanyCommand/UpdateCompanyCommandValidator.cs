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
                .WithMessage("name is not in the corrrect format!");
            RuleFor(u => u.TinNumber)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("tin number is not in the corrrect format!");
            RuleFor(u => u.CodeNIF)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("codenif is not in the corrrect format!");
            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty();

        }
    }
}