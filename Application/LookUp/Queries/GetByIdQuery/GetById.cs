using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.LookUp.Query.GetByIdQuery;

public record GetById : IRequest<LookupDto>
{
    public int Id { get; init; }
}

public class GetByIdHandler : IRequestHandler<GetById, LookupDto>
{

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetByIdHandler(
        IMapper mapper,
        IAppDbContext context
    )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LookupDto> Handle(GetById request, CancellationToken cancellationToken)
    {

        var lookup = await _context.Lookups.Where(l => l.Id == request.Id).ProjectTo<LookupDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        if (lookup == null)
        {
            throw new GhionException(CustomResponse.NotFound("lookup not found!"));
        }

        return lookup;

    }

}