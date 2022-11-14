

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
            _context = context;

            RuleFor(o => o.CustomerName)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.NotifyParty)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.BillNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.CustomerName)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.ShippingLine)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.GoodsDescription)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.SourceDocumentType)
                .NotNull()
                .NotEmpty();
          
            RuleFor(o => o.Quantity)
                .NotNull();
            RuleFor(o => o.DestinationType)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.ShippingAgentId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInShippingAgentsTable).WithMessage("shippingAgent with the provided id is not found");
            RuleFor(o => o.PortOfLoadingId)
                .NotNull()
                .NotEmpty()
               .Must(BeFoundInPortsTable).WithMessage("port with the provided id is not found");
            RuleFor(o => o.VoyageNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.TypeOfMerchandise)
                .NotNull()
                .NotEmpty();



        }
  
  
        private bool BeUniqueOperationNumber(string operationNumber)
        {
            return !_context.Operations.Where(o => o.OperationNumber == operationNumber).Any();
        }
        private bool BeFoundInShippingAgentsTable(int shippingAgentId)
        {
            return _context.ShippingAgents.Find(shippingAgentId) != null;
        }
        private bool BeFoundInPortsTable(int portOfloadingId)
        {
            return _context.Ports.Find(portOfloadingId) != null;
        }
    }
}
