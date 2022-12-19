using System.Diagnostics.SymbolStore;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.DocumentType;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.OperationModule.Queries.GetOperationList
{
    public record DashboardOperationList : IRequest<PaginatedList<DashboardOperationListDto>> {
        public int PageCount { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class DashboardOperationListHandler : IRequestHandler<DashboardOperationList, PaginatedList<DashboardOperationListDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DashboardOperationListHandler(IAppDbContext context, IMapper mapper, ILogger<GetOperationListQueryHandler> logger) {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedList<DashboardOperationListDto>> Handle(DashboardOperationList request, CancellationToken cancellationToken) {
            return await _context.Operations
                .Include(o => o.Payments)
                .OrderBy(o => o.Id)
                .Select(o => new DashboardOperationListDto {
                    OpeartionId = o.Id,
                    OperationNumber = o.OperationNumber,
                    Created = o.Created,
                    Status = (DocumentType.statusDictionary[o.Status]) * 20,
                    TotalPayedMoney = (o.Payments != null) ? totalPaymentCalculator(o.Payments) : 0,
                    NameOnPermit = o.ContactPerson.Name
                })
                .PaginatedListAsync(request.PageCount, request.PageSize);

        }

        public static Double totalPaymentCalculator(ICollection<Payment> payments){
            Double total = 0;
            foreach(Payment payment in payments){
                total += payment.Amount;
            }

            return total;
        }

    }



}