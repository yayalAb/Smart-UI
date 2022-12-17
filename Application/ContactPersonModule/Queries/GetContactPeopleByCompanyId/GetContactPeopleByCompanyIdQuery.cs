using Application.Common.Interfaces;
using Application.CompanyModule.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ContactPersonModule.Queries;
public record GetContactPeopleByCompanyIdQuery : IRequest<ICollection<ContactPersonDto>>
{
    public int CompanyId { get; set; }
}
public class GetContactPeopleByCompanyIdQueryHandler : IRequestHandler<GetContactPeopleByCompanyIdQuery, ICollection<ContactPersonDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetContactPeopleByCompanyIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ICollection<ContactPersonDto>> Handle(GetContactPeopleByCompanyIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.ContactPeople
        .Where(cp => cp.CompanyId == request.CompanyId)
        .ProjectTo<ContactPersonDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}