

using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.PaymentModule.Commands.CreatePayment
{
    public record CreatePaymentCommand : IRequest<int>
    {
        public string Type { get; init; }
        public string Name { get; init; }
        public DateTime PaymentDate { get; init; }
        public string PaymentMethod { get; init; }
        public string? BankCode { get; init; }
        public float Amount { get; init; }
        public string Currency { get; init; }
        public string? Description { get; init; }
        public int OperationId { get; init; }
        public int? ShippingAgentId { get; init; }
    }
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var newPayment = _mapper.Map<Payment>(request);
            await _context.Payments.AddAsync(newPayment);
            await _context.SaveChangesAsync(cancellationToken);
            return newPayment.Id;
        }
    }
}
