using System.Runtime.InteropServices;
using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Application.GoodModule.Queries.GoodLookUpQuery {
    public class GoodLookUpQuery : IRequest<List<Good>> {}

    public class GetAllGoodQueryHandler: IRequestHandler<GoodLookUpQuery, List<Good>> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllGoodQueryHandler(
            IIdentityService identityService, 
            IAppDbContext context,
            IMapper mapper
        ){
            _identityService = identityService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Good>> Handle(GoodLookUpQuery request, CancellationToken cancellationToken) {
            
            // return await _context.Goods.ProjectTo<GoodLookUpDto>(_mapper.ConfigurationProvider);
            return await _context.Goods.ToListAsync();

        }

    }
}