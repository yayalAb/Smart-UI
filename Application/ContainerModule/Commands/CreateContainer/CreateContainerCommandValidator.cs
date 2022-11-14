

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
                .NotEmpty();
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.OperationId)
                .NotNull()
                .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");
            
        }

        private bool BeFoundInDb(int operationId)
        {
            return _context.UserGroups.Find(operationId) != null;
        }
    }
}
