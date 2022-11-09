using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.AddressModule.Commands.AddressCreateCommand {

    public class AddressCreateCommandValidator : AbstractValidator<AddressCreateCommand> {

        public AddressCreateCommandValidator(){

            RuleFor(u => u.City)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Subcity)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Country)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Region)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress().WithMessage("email is not in the correct format");
            RuleFor(u => u.Phone)
                .NotNull()
                .NotEmpty();

        }
        
    }

}