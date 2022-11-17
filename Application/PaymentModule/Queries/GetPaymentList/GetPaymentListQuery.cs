using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.PaymentModule.Queries.GetPaymentById;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.PaymentModule.Queries.GetPaymentList{
    public record GetPaymentListQuery : IRequest<PaginatedList<PaymentDto>>{
        public int pageNumber {get; init;}=1;
        public int pageSize {get; init; }=10;
    }

    public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQuery, PaginatedList<PaymentDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetPaymentListQueryHandler(IAppDbContext context , IMapper mapper , ILogger<GetPaymentListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedList<PaymentDto>> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
        {
          return await _context.Payments
            .OrderBy(p => p.Id)
            .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.pageNumber , request.pageSize);
        }
    }



}