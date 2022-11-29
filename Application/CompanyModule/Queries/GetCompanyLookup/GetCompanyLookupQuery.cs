
using Application.Common.Interfaces;
using Application.UserGroupModule.Queries.UserGroupLookup;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.GetCompanyLookup;

public record GetCompanyLookupQuery : IRequest<ICollection<DropDownLookupDto>> {}

public record GetCompanyLookupQueryHandler : IRequestHandler<GetCompanyLookupQuery, ICollection<DropDownLookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyLookupQueryHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<DropDownLookupDto>> Handle(GetCompanyLookupQuery request, CancellationToken cancellationToken) {
          return await _context.Companies.Select(u => new DropDownLookupDto() {
            Text = u.Name,
            Value = u.Id
        }).ToListAsync();
    }

}