
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShippingAgentFeeModule.Commands.UpdateShippingAgentFee
{
    public record UpdateShippingAgentFeeCommand : IRequest<int>
    {
        public int Id { get; set; } 
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
    public class UpdateShippingAgentFeeCommandHandler : IRequestHandler<UpdateShippingAgentFeeCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateShippingAgentFeeCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateShippingAgentFeeCommand request, CancellationToken cancellationToken)
        {
            var oldShippingAgentFee = await _context.ShippingAgentFees.AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.Id);
            
            if (oldShippingAgentFee == null)
            {
                throw new NotFoundException("shippingAgentFee" , new {Id = request.Id});
            }

            oldShippingAgentFee = _mapper.Map<ShippingAgentFee>(request);
             _context.ShippingAgentFees.Update(oldShippingAgentFee);
            await _context.SaveChangesAsync(cancellationToken);

            return oldShippingAgentFee.Id;

        }
    }
}
