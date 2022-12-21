
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationModule.Queries.GetOperationPaginatedList;

public record SearchOperation : IRequest<PaginatedList<OperationDto>>
{
    public string Word {get; init; }
    public int PageCount { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class SearchOperationHandler : IRequestHandler<SearchOperation, PaginatedList<OperationDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public SearchOperationHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OperationDto>> Handle(SearchOperation request, CancellationToken cancellationToken)
    {
        return await _context.Operations
        .Include(o => o.Goods)
        .Where(o => (o.Consignee != null ? o.Consignee.Contains(request.Word) : false) ||
            (o.NotifyParty != null ? o.NotifyParty.Contains(request.Word) : false) ||
            (o.BillNumber != null ? o.BillNumber.Contains(request.Word) : false) ||
            (o.ShippingLine != null ? o.ShippingLine.Contains(request.Word) : false) ||
            (o.GoodsDescription != null ? o.GoodsDescription.Contains(request.Word) : false) ||
            (o.Quantity.ToString().Contains(request.Word)) ||
            (o.GrossWeight.ToString().Contains(request.Word)) ||
            (o.ATA != null ? o.ATA.Contains(request.Word) : false) ||
            (o.FZIN != null ? o.FZIN.Contains(request.Word) : false) ||
            (o.FZOUT != null ? o.FZOUT.Contains(request.Word) : false) ||
            (o.DestinationType != null ? o.DestinationType.Contains(request.Word) : false) ||
            (o.SourceDocument != null ? o.SourceDocument.Contains(request.Word) : false) ||
            (o.VoyageNumber != null ? o.VoyageNumber.Contains(request.Word) : false) ||
            (o.OperationNumber != null ? o.OperationNumber.Contains(request.Word) : false) ||
            (o.SNumber != null ? o.SNumber.Contains(request.Word) : false) ||
            (o.RecepientName != null ? o.RecepientName.Contains(request.Word) : false) ||
            (o.VesselName != null ? o.VesselName.Contains(request.Word) : false) ||
            (o.CountryOfOrigin != null ? o.CountryOfOrigin.Contains(request.Word) : false) ||
            (o.REGTax.ToString().Contains(request.Word)) ||
            (o.BillOfLoadingNumber != null ? o.BillOfLoadingNumber.Contains(request.Word) : false) ||
            (o.FinalDestination != null ? o.FinalDestination.Contains(request.Word) : false) ||
            (o.Localization != null ? o.Localization.Contains(request.Word) : false) ||
            (o.Shipper != null ? o.Shipper.Contains(request.Word) : false) ||
            (o.PINumber != null ? o.PINumber.Contains(request.Word) : false)
        )
        .ProjectTo<OperationDto>(_mapper.ConfigurationProvider)
        .PaginatedListAsync(request.PageCount, request.PageSize);
    }

}