using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Common.Models;
using Application.Common.Exceptions;

namespace Application.CompanyModule.Queries.GetCompanyQuery
{
    public class GetCompanyQuery : IRequest<CompanyDto> {
        
        public int Id {get; init;}

        public GetCompanyQuery(int id){
            this.Id = id;
        }

    }

    public class GetCompanyQueryHandler: IRequestHandler<GetCompanyQuery, CompanyDto> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<GetCompanyQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandler(IIdentityService identityService , IAppDbContext context , ILogger<GetCompanyQueryHandler> logger , IMapper mapper){
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken) {
            var company = await _context.Companies
            .Include(c => c.Address)
            .Include(c => c.ContactPerson)
            .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
            .Where(c => c.Id == request.Id).FirstOrDefaultAsync();
            if(company == null){
                throw new GhionException(CustomResponse.NotFound("company not found!"));
            }
            return company;

        }

    }
}