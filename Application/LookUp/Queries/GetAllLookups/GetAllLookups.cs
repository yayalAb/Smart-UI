using Application.Common.Interfaces;
using MediatR;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.LookUp.Query.GetAllLookups;

public record GetAllLookups : IRequest<PaginatedList<LookupDto>> {
    public int PageNumber {get; set;}
    public int PageCount {get; set;}
}

public class GetAllLookupsHandler: IRequestHandler<GetAllLookups, PaginatedList<LookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllLookupsHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LookupDto>> Handle(GetAllLookups request, CancellationToken cancellationToken) {
        var lookups = await PaginatedList<LookupDto>.CreateAsync(_context.Lookups.ProjectTo<LookupDto>(_mapper.ConfigurationProvider), request.PageNumber, request.PageCount);
        return lookups;
    }

}