using Application.Common.Interfaces;
using Application.CompanyModule.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ContactPersonModule.Queries;
public record GetContactPeopleByCompanyIdQuery : IRequest<ICollection<ContactPersonLookupDto>>
{
    public int CompanyId { get; set; }
}
public class GetContactPeopleByCompanyIdQueryHandler : IRequestHandler<GetContactPeopleByCompanyIdQuery, ICollection<ContactPersonLookupDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetContactPeopleByCompanyIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ICollection<ContactPersonLookupDto>> Handle(GetContactPeopleByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.ContactPeople
        .Where(cp => cp.CompanyId == request.CompanyId)
        .ProjectTo<ContactPersonLookupDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}