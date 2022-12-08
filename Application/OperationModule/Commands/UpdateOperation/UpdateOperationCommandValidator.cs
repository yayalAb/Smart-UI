
using Domain.Common.DestinationTypes;
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationModule.Commands.UpdateOperation
{
    public class UpdateOperationCommandValidator : AbstractValidator<UpdateOperationCommand>    
    {
        private readonly IAppDbContext _context;

        public UpdateOperationCommandValidator(IAppDbContext context)
        {
            _context = context;
        
            RuleFor(o => o.OpenedDate)
                .NotNull();
            RuleFor(o => o.Quantity)
                .NotNull();
            RuleFor(o => o.ShippingAgentId)
                .Must(BeFoundInShippingAgentsTable).WithMessage("shippingAgent with the provided id is not found");
            RuleFor(o => o.PortOfLoadingId)
               .Must(BeFoundInPortsTable)
               .WithMessage("port with the provided id is not found");
            RuleFor(o =>o.CompanyId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInCompanyTable)
                .WithMessage("company with the provided id is not found");
            RuleFor(o =>o.DestinationType)
                .NotNull()
                .NotEmpty()
                .Must(BeOfDestinationType)
                .WithMessage("destination type is not in the correct format!");

        }
  
        private bool BeFoundInShippingAgentsTable(int? shippingAgentId)
        {
            return shippingAgentId == null || _context.ShippingAgents.Find(shippingAgentId) != null;
        }
        private bool BeOfDestinationType(string DesType) {
            return DestinationType.Types.Contains(DesType);
        }
        private bool BeFoundInPortsTable(int? portOfloadingId)
        {
            return portOfloadingId == null || _context.Ports.Find(portOfloadingId) != null;
        }
        private bool BeFoundInCompanyTable(int companyId)
        {
            return  _context.Companies.Find(companyId) != null;
        }
    }
}
