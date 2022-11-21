using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.UserGroupModule.Queries.UserGroupLookup;

namespace Application.UserGroupModule.Queries.GetOperationLookupQuery;

public record GetOperationLookupQuery : IRequest<List<DropDownLookupDto>> {}

public class GetOperationLookupQueryHandler : IRequestHandler<GetOperationLookupQuery, List<DropDownLookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetOperationLookupQueryHandler(IMapper mapper, IAppDbContext context) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(GetOperationLookupQuery request, CancellationToken candellationToken) {
        return await _context.Operations.Select(u => new DropDownLookupDto() {
            Text = u.OperationNumber,
            Value = u.Id
        }).ToListAsync();
    }

}
