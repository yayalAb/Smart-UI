
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.BillOfLoadingModule.Commands.CreateBillOfLoading
{
    public class CreateBillOfLoadingCommandValidator : AbstractValidator<CreateBillOfLoadingCommand>
    {
        private readonly IAppDbContext _context;

        public CreateBillOfLoadingCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(bl => bl.CustomerName)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.NotifyParty)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.BillNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.CustomerName)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.ShippingLine)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.GoodsDescription)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.ContainerId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInContainersTable).WithMessage("container with the provided id is not found");
            RuleFor(bl => bl.Quantity)
                .NotNull();
            RuleFor(bl => bl.DestinationType)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.ShippingAgentId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInShippingAgentsTable).WithMessage("shippingAgent with the provided id is not found");
            RuleFor(bl => bl.PortOfLoadingId)
                .NotNull()
                .NotEmpty()
               .Must(BeFoundInPortsTable).WithMessage("port with the provided id is not found");
            RuleFor(bl => bl.VoyageNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(bl => bl.TypeOfMerchandise)
                .NotNull()
                .NotEmpty();
 
        }

        private bool BeFoundInContainersTable(int containerId )
        {
            return _context.Containers.Find(containerId) != null;
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
