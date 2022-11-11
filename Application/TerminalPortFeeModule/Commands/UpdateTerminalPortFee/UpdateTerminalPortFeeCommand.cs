

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TerminalPortFeeModule.Commands.UpdateTerminalPortFee
{
    public record UpdateTerminalPortFeeCommand : IRequest<int>
    {
        public int Id { get; init; }
        public string Type { get; init; } 
        public DateTime PaymentDate { get; init; }
        public string PaymentMethod { get; init; }
        public string? BankCode { get; init; }
        public float Amount { get; init; }
        public string Currency { get; init; }
        public string? Description { get; init; }
        public int OperationId { get; init; }
    }
    public class UpdateTerminalPortFeeCommandHandler : IRequestHandler<UpdateTerminalPortFeeCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTerminalPortFeeCommandHandler(IAppDbContext context ,IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateTerminalPortFeeCommand request, CancellationToken cancellationToken)
        {
            var oldShippingAgentFee = await _context.TerminalPortFees.AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.Id);

            if (oldShippingAgentFee == null)
            {
                throw new NotFoundException("shippingAgentFee", new { Id = request.Id });
            }

            oldShippingAgentFee = _mapper.Map<TerminalPortFee>(request);
            _context.TerminalPortFees.Update(oldShippingAgentFee);
            await _context.SaveChangesAsync(cancellationToken);

            return oldShippingAgentFee.Id;
        }
    }
}
