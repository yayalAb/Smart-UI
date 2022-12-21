using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.PaymentModule.Queries.GetPaymentList {
    public record PaymentListSearch : IRequest<PaginatedList<PaymentDto>>
    {
        public string Word { get; init; }
        public int PageCount { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class PaymentListSearchHandler : IRequestHandler<PaymentListSearch, PaginatedList<PaymentDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PaymentListSearchHandler(IAppDbContext context, IMapper mapper, ILogger<PaymentListSearchHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<PaymentDto>> Handle(PaymentListSearch request, CancellationToken cancellationToken)
        {
            return await _context.Payments
            .Include(p => p.Operation)
            .ThenInclude(p => p.ShippingAgent)
            .Where(p => p.Name.Contains(request.Word) ||
                p.Type.Contains(request.Word) ||
                p.PaymentMethod.Contains(request.Word) ||
                (p.BankCode != null ? p.BankCode.Contains(request.Word) : false) ||
                p.Amount.ToString().Contains(request.Word) ||
                p.Currency.Contains(request.Word) ||
                (p.Description != null ? p.Description.Contains(request.Word) : false) ||
                (p.DONumber != null ? p.DONumber.Contains(request.Word) : false) ||
                p.Operation.OperationNumber.Contains(request.Word) ||
                p.ShippingAgent.FullName.Contains(request.Word)
            )
            .OrderBy(p => p.Id)
            .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageCount, request.PageSize);
        }
    }

}