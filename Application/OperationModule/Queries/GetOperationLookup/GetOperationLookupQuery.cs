using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.OperationModule.Queries.GetOperationLookup;

public record GetOperationLookupQuery : IRequest<List<OperationLookupDto>> {}

public class GetOperationLookupQueryHandler : IRequestHandler<GetOperationLookupQuery, List<OperationLookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetOperationLookupQueryHandler(IMapper mapper, IAppDbContext context) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OperationLookupDto>> Handle(GetOperationLookupQuery request, CancellationToken candellationToken) {
        return await _context.Operations
                .Include(o => o.ContactPerson)
                .Select(u => new OperationLookupDto() {
                    Text = $"{u.OperationNumber} {u.ContactPerson.Name}" ,
                    Value = u.Id,
                    CompanyId = u.CompanyId
                }).ToListAsync();
    }

}
