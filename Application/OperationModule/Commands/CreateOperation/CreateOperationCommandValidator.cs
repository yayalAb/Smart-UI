

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
            RuleFor(o => o.OpenedDate)
                .NotNull();
            RuleFor(o => o.Quantity)
                .NotNull();
            RuleFor(o => o.Status)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.ShippingAgentId)
                .Must(BeFoundInShippingAgentsTable).WithMessage("shippingAgent with the provided id is not found");
            RuleFor(o => o.PortOfLoadingId)
               .Must(BeFoundInPortsTable).WithMessage("port with the provided id is not found");
            RuleFor(o =>o.CompanyId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInCompanyTable).WithMessage("company with the provided id is not found");



        }
  
  

        private bool BeFoundInShippingAgentsTable(int? shippingAgentId)
        {
            return shippingAgentId == null || _context.ShippingAgents.Find(shippingAgentId) != null;
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
