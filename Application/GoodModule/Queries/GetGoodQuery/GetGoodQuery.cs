using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GoodModule.Queries.GetGoodQuery
{

    public class GetGoodQuery : IRequest<Good> {
        
        public int Id {get; init;}

        public GetGoodQuery(int id){
            this.Id = id;
        }

    }

    public class GetGoodQueryHandler: IRequestHandler<GetGoodQuery, Good> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<GetGoodQueryHandler> _logger;

        public GetGoodQueryHandler(
            IIdentityService identityService, 
            IAppDbContext context, 
            ILogger<GetGoodQueryHandler> logger
        ){
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<Good> Handle(GetGoodQuery request, CancellationToken cancellationToken) {
            
            var good = await _context.Goods.Include(c => c.Container).Where(c => c.Id == request.Id ).FirstOrDefaultAsync();
            if(good == null){
                throw new Exception("good not found!");
            }

            return good;

        }

    }
}