using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.PaymentModule.Queries.GetPaymentList
{
    public record GetPaymentListQuery : IRequest<PaginatedList<PaymentDto>>
    {
        public int PageCount { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQuery, PaginatedList<PaymentDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetPaymentListQueryHandler(IAppDbContext context, IMapper mapper, ILogger<GetPaymentListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedList<PaymentDto>> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Payments
            .Include(p => p.Operation)
            .ThenInclude(p => p.ShippingAgent)
            .OrderBy(p => p.Id)
            .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageCount, request.PageSize);
        }
    }



}