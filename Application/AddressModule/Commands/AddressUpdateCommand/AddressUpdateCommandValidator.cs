using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.AddressModule.Commands.AddressUpdateCommand {

    public class AddressUpdateCommandValidator : AbstractValidator<AddressUpdateCommand> {

        public AddressUpdateCommandValidator(){

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
                .EmailAddress();
            RuleFor(u => u.Phone)
                .NotNull()
                .NotEmpty();

        }
        
    }

}