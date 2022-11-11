

using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.ShippingAgentFeeModule.Commands.CreateShippingAgentFee
{
    public record CreateShippingAgentFeeCommand : IRequest<int>   
    {
        public string Type { get; init; } 
        public DateTime PaymentDate { get; init; }
        public string PaymentMethod { get; init; }
        public string? BankCode { get; init; }
        public float Amount { get; init; }
        public string Currency { get; init; }
        public string? Description { get; init; }
        public int OperationId { get; init; }
        public int ShippingAgentId { get; init; }
    }
    public class CreateShippingAgentFeeCommandHandler : IRequestHandler<CreateShippingAgentFeeCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CreateShippingAgentFeeCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateShippingAgentFeeCommand request, CancellationToken cancellationToken)
        {
            var newShippingAgentFee = _mapper.Map<ShippingAgentFee>(request);
            await _context.ShippingAgentFees.AddAsync(newShippingAgentFee);
            await _context.SaveChangesAsync(cancellationToken);
            return newShippingAgentFee.Id;
        }
    }
}
