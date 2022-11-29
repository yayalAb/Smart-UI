

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PaymentModule.Queries.GetPaymentById
{
    public record GetPaymentByIdQuery : IRequest<PaymentDto>
    {
        public int Id { get; init; } 
    }
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto> {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetPaymentByIdQueryHandler(IAppDbContext context , IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PaymentDto> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken) {

            var payment = _context.Payments
                .Include(p => p.Operation)
                .ThenInclude(p => p.ShippingAgent)
                .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefault(s => s.Id == request.Id);

            if(payment == null) {
                throw new GhionException(CustomResponse.NotFound("Payment created!"));
            }

            return payment;

        }
    }
}
