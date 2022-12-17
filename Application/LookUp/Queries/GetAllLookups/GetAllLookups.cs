using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.LookUp.Query.GetAllLookups;

public record GetAllLookups : IRequest<PaginatedList<LookupDto>>
{
    public int? PageCount { get; set; }
    public int? PageSize { get; set; }
}

public class GetAllLookupsHandler : IRequestHandler<GetAllLookups, PaginatedList<LookupDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllLookupsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LookupDto>> Handle(GetAllLookups request, CancellationToken cancellationToken)
    {
        var lookups = await PaginatedList<LookupDto>.CreateAsync(_context.Lookups.Where(l => l.Key != "key").ProjectTo<LookupDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
        return lookups;
    }

}