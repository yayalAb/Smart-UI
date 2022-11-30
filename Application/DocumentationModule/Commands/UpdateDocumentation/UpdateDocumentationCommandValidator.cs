﻿
using Application.Common.Interfaces;
using Domain.Common.DocumentType;
using FluentValidation;

namespace Application.DocumentationModule.Commands.UpdateDocumentation
{
    public class UpdateDocumentationCommandValidator : AbstractValidator<UpdateDocumentationCommand>    
    {
        private readonly IAppDbContext _context;

        public UpdateDocumentationCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(d => d.Id)
                .NotNull()
                .NotEmpty();
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
                .Must(BeOfType)
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

        private bool BeOfType(string Type){
            return DocumentType.Types.Contains(Type);
        }
    
    }
}
