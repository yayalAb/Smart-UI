
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ContainerModule.Queries.ContainerByOperation;

public class GetByOperation : IRequest<List<ContainerGoodCountDto>>
{
    public int OperationId { get; init; }
    public bool Type { get; init; } = false;
}
public class GetByOperationHandler : IRequestHandler<GetByOperation, List<ContainerGoodCountDto>>
{

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetByOperationHandler(
        IIdentityService identityService,
        IAppDbContext context,
        IMapper mapper
    )
    {
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ContainerGoodCountDto>> Handle(GetByOperation request, CancellationToken cancellationToken)
    {

        return await _context.Containers
            .Where(c => c.OperationId == request.OperationId && (request.Type ? (c.GeneratedDocumentId != null) : true))
            .Select(c => new ContainerGoodCountDto {
                Id = c.Id,
                ContianerNumber = c.ContianerNumber,
                SealNumber = c.SealNumber,
                Location = c.Location,
                LocationPortId = c.LocationPortId,
                OperationId = c.OperationId,
                Article = c.Article,
                TotalPrice = c.TotalPrice,
                Currency = c.Currency,
                WeightMeasurement = c.WeightMeasurement,
                GrossWeight = c.GrossWeight,
                GoodCount = c.Quantity
            }).ToListAsync();

    }

}