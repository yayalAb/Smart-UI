using Application.CompanyModule.Queries;
using Domain.Entities;
using MediatR;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.CompanyModule.Queries.GetAllCompanyQuery;

public record GetAllCompanies : IRequest<PaginatedList<CompanyDto>> {
    public int? PageCount {get; set;}
    public int? PageSize {get; set;}
}

public class GetAllCompaniesHandler : IRequestHandler<GetAllCompanies, PaginatedList<CompanyDto>> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllCompaniesHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CompanyDto>> Handle(GetAllCompanies request, CancellationToken cancellationToken){
        return await PaginatedList<CompanyDto>.CreateAsync(_context.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
    }
}