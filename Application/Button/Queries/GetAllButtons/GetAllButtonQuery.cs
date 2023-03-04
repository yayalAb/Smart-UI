using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Button.Queries.GetAllButtons;

public record GetAllButtonQuery : IRequest<PaginatedList<GetButtonDto>>
{
    public int? PageCount { set; get; } = 1!;
    public int? PageSize { get; set; } = 10!;
}
public class GetAllButtonQueryHandler : IRequestHandler<GetAllButtonQuery, PaginatedList<GetButtonDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllButtonQueryHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<GetButtonDto>> Handle(GetAllButtonQuery request, CancellationToken cancellationToken)
    {
        return await PaginatedList<GetButtonDto>
        .CreateAsync(_context.buttons.ProjectTo<GetButtonDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
    }
}
