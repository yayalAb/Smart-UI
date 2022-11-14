
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
                .NotNull()
                .NotEmpty()
                .WithMessage("date is not in the correct format!");
            RuleFor(d => d.Type)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("type is not in the correct format!");
            RuleFor(d => d.BankPermit)
                .MaximumLength(45)
                .WithMessage("bank permit is not in the correct format!");
            RuleFor(d => d.InvoiceNumber)
                .MaximumLength(45)
                .WithMessage("Invoice Number is not in the correct format!");
            RuleFor(d => d.ImporterName)
                .MaximumLength(45)
                .WithMessage("importer name is not in the correct format!");
            RuleFor(d => d.Phone)
                .MaximumLength(45)
                .WithMessage("phone number is not in the correct format!");
            RuleFor(d => d.Country)
                .MaximumLength(45)
                .WithMessage("country name is not in the correct format!");
            RuleFor(d => d.City)
                .MaximumLength(45)
                .WithMessage("city name is not in the correct format!");
            RuleFor(d => d.TinNumber)
                .MaximumLength(45)
                .WithMessage("tin number is not in the correct format!");
            RuleFor(d => d.TransportationMethod)
                .MaximumLength(45)
                .WithMessage("transportation method name is not in the correct format!");
            RuleFor(d => d.Source)
                .MaximumLength(45)
                .WithMessage("source is not in the correct format!");
            RuleFor(d => d.Destination)
                .MaximumLength(45)
                .WithMessage("destination is not in the correct format!");
        }
        private bool BeFoundInDb(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }
    }
}
