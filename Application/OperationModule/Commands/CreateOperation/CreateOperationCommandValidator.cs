

using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationModule.Commands.CreateOperation
{
    public class CreateOperationCommandValidator : AbstractValidator<CreateOperationCommand>    
    {
        private readonly IAppDbContext _context;

        public CreateOperationCommandValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(o => o.OperationNumber)
                .NotNull()
                .NotEmpty()
                .Must(BeUniqueOperationNumber).WithMessage("operation number should be unique");
            RuleFor(o => o.OpenedDate)
                .NotNull();
            RuleFor(o => o.BillOfLoadingId)
                .NotNull()
                
                .Must(BeUnique).WithMessage("duplicate bill of loading : only one operation can be created using one bill of loading  ")
                .Must(BeFoundInDb).WithMessage("bill of loading with the provided id is not found");


                
        }
        private bool BeFoundInDb(int billOfLoadingId)
        {
            return _context.BillOfLoadings.Find(billOfLoadingId) != null;
        }
        private bool BeUnique(int billOfLoadingId)
        {
            return !_context.Operations.Where(o => o.BillOfLoadingId == billOfLoadingId).Any();  
        }
        private bool BeUniqueOperationNumber(string operationNumber)
        {
            return !_context.Operations.Where(o => o.OperationNumber == operationNumber).Any();
        }
    }
}
