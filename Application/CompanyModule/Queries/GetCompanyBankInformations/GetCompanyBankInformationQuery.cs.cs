using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.CompanyModule.Commands.CreateCompanyCommand;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CompanyModule.Queries.GetCompanyBankInformation;
public record GetCompanyBankInformationQuery : IRequest<List<FetchBankInformationDto>>{
    public int CompanyId { get; set; }
}
public class GetCompanyBankInformationQueryHandler : IRequestHandler<GetCompanyBankInformationQuery, List<FetchBankInformationDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyBankInformationQueryHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<FetchBankInformationDto>> Handle(GetCompanyBankInformationQuery request, CancellationToken cancellationToken)
    {
        if(!await _context.Companies.Where(c => c.Id == request.CompanyId).AnyAsync()){
            throw new GhionException(CustomResponse.NotFound($"company with id {request.CompanyId} is not found"));
        }
        return await _context.BankInformation
                .Where(bi =>bi.CompanyId == request.CompanyId)
                .ProjectTo<FetchBankInformationDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}