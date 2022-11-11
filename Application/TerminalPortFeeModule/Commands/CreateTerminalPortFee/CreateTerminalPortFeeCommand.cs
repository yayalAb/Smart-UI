

using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.TerminalPortFeeModule.Commands.CreateTerminalPortFee
{
    public record CreateTerminalPortFeeCommand : IRequest<int>
    {
        public string Type { get; init; }
        public DateTime PaymentDate { get; init; }
        public string PaymentMethod { get; init; }
        public string? BankCode { get; init; }
        public float Amount { get; init; }
        public string Currency { get; init; }
        public string? Description { get; init; }
        public int OperationId { get; init; }
    }
    public class CreateTerminalPortFeeCommandHandler : IRequestHandler<CreateTerminalPortFeeCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CreateTerminalPortFeeCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateTerminalPortFeeCommand request, CancellationToken cancellationToken)
        {
            TerminalPortFee newTerminalPortFee = _mapper.Map<TerminalPortFee>(request);
            await _context.TerminalPortFees.AddAsync(newTerminalPortFee);
            await _context.SaveChangesAsync(cancellationToken);
            return newTerminalPortFee.Id;
        }
    }
} 
