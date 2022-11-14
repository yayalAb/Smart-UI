﻿

using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.ContainerModule.Commands.CreateContainer
{
    public class CreateContainerCommandValidator : AbstractValidator<CreateContainerCommand>  
    {
        private readonly IAppDbContext _context;

        public CreateContainerCommandValidator(IAppDbContext context )
        {
            _context = context;
            RuleFor(c => c.ContianerNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("container number is not in the correct format!");
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
<<<<<<< HEAD
            RuleFor(c => c.Owner)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("owner is not in the correct format!");
            RuleFor(c => c.ManufacturedDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("name is not in the correct format!");
=======
            RuleFor(c => c.OperationId)
                .NotNull()
                .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");
            
        }
>>>>>>> 68a589be846fc74a04d36d873b4bfefc93d8539c

        private bool BeFoundInDb(int operationId)
        {
            return _context.UserGroups.Find(operationId) != null;
        }
    }
}
