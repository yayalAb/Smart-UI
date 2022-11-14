using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GoodModule.Queries.GetAllGoodQuery {
    public class GetAllGoodQuery : IRequest<List<Good>> {}

    public class GetAllGoodQueryHandler: IRequestHandler<GetAllGoodQuery, List<Good>> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;

        public GetAllGoodQueryHandler(
            IIdentityService identityService, 
            IAppDbContext context
        ){
            _identityService = identityService;
            _context = context;
        }

        public async Task<List<Good>> Handle(GetAllGoodQuery request, CancellationToken cancellationToken) {
            
            return await _context.Goods.Include(c => c.Container).ToListAsync();

        }

    }

}