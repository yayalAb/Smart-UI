
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.DocumentationModule.Commands.CreateDocumentation
{
    public class CreateDocumentationCommandValidator :AbstractValidator<CreateDocumentationCommand> 
    {
        private readonly IAppDbContext _context;

        public CreateDocumentationCommandValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(d => d.OperationId)
                .NotNull()
                .Must(BeFoundInDb);
            RuleFor(d => d.Date)
                .NotNull();
            RuleFor(d => d.Type)
                .NotNull()
                .NotEmpty();
        }
        private bool BeFoundInDb(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }
    }
}
