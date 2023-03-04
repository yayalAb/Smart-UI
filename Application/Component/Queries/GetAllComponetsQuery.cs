using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Component.Queries;

public record GetAllComponetsQuery : IRequest<PaginatedList<ComponentDto>>
{
    public int? PageCount { set; get; } = 1!;
    public int? PageSize { get; set; } = 10!;
}
public class GetAllComponetsQueryHandler : IRequestHandler<GetAllComponetsQuery, PaginatedList<ComponentDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllComponetsQueryHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<ComponentDto>> Handle(GetAllComponetsQuery request, CancellationToken cancellationToken)
    {
        return await PaginatedList<ComponentDto>
        .CreateAsync(_context.Components.ProjectTo<ComponentDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
    }
}
