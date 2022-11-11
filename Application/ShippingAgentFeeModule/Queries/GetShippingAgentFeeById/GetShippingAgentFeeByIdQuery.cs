

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;

namespace Application.ShippingAgentFeeModule.Queries.GetShippingAgentFeeById
{
    public record GetShippingAgentFeeByIdQuery : IRequest<ShippingAgentFeeDto>
    {
        public int Id { get; init; } 
    }
    public class GetShippingAgentFeeByIdQueryHandler : IRequestHandler<GetShippingAgentFeeByIdQuery, ShippingAgentFeeDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetShippingAgentFeeByIdQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ShippingAgentFeeDto> Handle(GetShippingAgentFeeByIdQuery request, CancellationToken cancellationToken)
        {
            var shippingAgentFee = _context.ShippingAgentFees
                .ProjectTo<ShippingAgentFeeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefault(s => s.Id == request.Id);

            if(shippingAgentFee == null)
            {
                throw new NotFoundException("Shipping agent fee", new { request.Id });
            }
            return shippingAgentFee;
        }
    }
}
