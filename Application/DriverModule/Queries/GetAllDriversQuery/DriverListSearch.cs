using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.GetAllDriversQuery;

public record DriverListSearch : IRequest<PaginatedList<DriverDto>>
{
    public string Word { get; set; }
    public int? PageCount { get; set; } = 1!;
    public int? PageSize { get; set; } = 10!;
}

public class DriverListSearchHandler : IRequestHandler<DriverListSearch, PaginatedList<DriverDto>>
{

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public DriverListSearchHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<DriverDto>> Handle(DriverListSearch request, CancellationToken cancellationToken)
    {
        return await PaginatedList<DriverDto>
            .CreateAsync(
                _context.Drivers
                .Include(t => t.Address)
                .Where(d => d.Fullname.Contains(request.Word) ||
                    d.LicenceNumber.Contains(request.Word) ||
                    d.Address.Email.Contains(request.Word) ||
                    d.Address.Phone.Contains(request.Word) ||
                    d.Address.City.Contains(request.Word) ||
                    d.Address.Subcity.Contains(request.Word) ||
                    d.Address.Country.Contains(request.Word) ||
                    (d.Address.POBOX != null ? d.Address.POBOX.Contains(request.Word) : false) ||
                    d.Address.Region.Contains(request.Word)
                )
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider),
                request.PageCount ?? 1,
                request.PageSize ?? 10
            );
    }

}