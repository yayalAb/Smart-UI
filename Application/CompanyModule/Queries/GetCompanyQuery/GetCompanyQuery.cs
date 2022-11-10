using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CompanyModule.Queries.GetCompanyQuery
{
    public class GetCompanyQuery : IRequest<Company> {
        
        public int Id {get; init;}

        public GetCompanyQuery(int id){
            this.Id = id;
        }

    }

    public class GetCompanyQueryHandler: IRequestHandler<GetCompanyQuery, Company> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<GetCompanyQueryHandler> _logger;

        public GetCompanyQueryHandler(IIdentityService identityService , IAppDbContext context , ILogger<GetCompanyQueryHandler> logger){
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<Company> Handle(GetCompanyQuery request, CancellationToken cancellationToken) {
            
            var company = await _context.Companies.Include(c => c.Address).Include(c => c.ContactPerson).Where(c => c.Id == request.Id).FirstOrDefaultAsync();
            if(company == null){
                throw new Exception("company not found!");
            }

            return company;

        }

    }
}