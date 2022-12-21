using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationDocuments.Queries.CommercialInvoice
{

    public class CommercialInvoiceValidator : AbstractValidator<CommercialInvoice>
    {
        private readonly IAppDbContext _context;

        public CommercialInvoiceValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(c => c.IsProformaInvoice)
                .NotNull();
            RuleFor(c => c.TruckAssignmentId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInTruckAssignmentsTable).WithMessage("truck assignment with the provided id is not found");
            RuleFor(c => c.ContactPersonId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInContactPersonTable  ).WithMessage("contact person with the provided id is not found");
            RuleFor(c => c.BankInformationId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInBankInformationTable).WithMessage("bank information with the provided id is not found");
            RuleFor(c => c.operationId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInOperationTable).WithMessage("bank information with the provided id is not found");
                          
        }
        private bool BeFoundInTruckAssignmentsTable(int truckAssignmentId)
        {
            return _context.TruckAssignments.Find(truckAssignmentId) != null;
        }
        private bool BeFoundInContactPersonTable(int contactPersonId)
        {
            return _context.ContactPeople.Find(contactPersonId) != null;
        }
        private bool BeFoundInBankInformationTable(int bankInformationId)
        {
            return _context.BankInformation.Find(bankInformationId) != null;
        }
        private bool BeFoundInOperationTable(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }
    }

}