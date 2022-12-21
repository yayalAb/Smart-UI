using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CompanyModule.Queries.GetAllCompanyQuery;

public record CompanyListSearch : IRequest<PaginatedList<CompanyDto>>
{
    public string Word { get; set; }
    public int? PageCount { get; set; }
    public int? PageSize { get; set; }
}

public class CompanyListSearchHandler : IRequestHandler<CompanyListSearch, PaginatedList<CompanyDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CompanyListSearchHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CompanyDto>> Handle(CompanyListSearch request, CancellationToken cancellationToken)
    {
        return await PaginatedList<CompanyDto>.CreateAsync(
            _context.Companies
                .Where(c => (c.Name != null ? c.Name.Contains(request.Word) : false)||
                    (c.TinNumber != null ? c.TinNumber.Contains(request.Word) : false) ||
                    (c.CodeNIF != null ? c.CodeNIF.Contains(request.Word) : false) ||
                    c.Address.Email.Contains(request.Word) ||
                    c.Address.Phone.Contains(request.Word) ||
                    c.Address.Region.Contains(request.Word) ||
                    c.Address.City.Contains(request.Word) ||
                    c.Address.Subcity.Contains(request.Word) ||
                    c.Address.Country.Contains(request.Word) ||
                    (c.Address.POBOX != null ? c.Address.POBOX.Contains(request.Word) : false)
                )
                .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider), 
            request.PageCount ?? 1, 
            request.PageSize ?? 10
        );
    }
}