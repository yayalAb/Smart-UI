using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

namespace Application.ContactPersonModule.Commands.ContactPersonUpdateCommand {

    public class ContactPersonUpdateValidator : AbstractValidator<ContactPersonUpdateCommand> {
        public ContactPersonUpdateValidator(){
            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.Phone)
                .NotNull()
                .NotEmpty();
        }
    }

}