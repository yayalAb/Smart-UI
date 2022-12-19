using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.LookUp.Query.GetByKey
{
    public class GetLookUpByKey : IRequest<List<LookupDto>>
    {
        public string Key { get; init; }

        public GetLookUpByKey(string type)
        {
            this.Key = type;
        }
    }

    public class GetLookUpByKeyHandler : IRequestHandler<GetLookUpByKey, List<LookupDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetLookUpByKeyHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<LookupDto>> Handle(GetLookUpByKey request, CancellationToken cancellationToken)
        {
            return await _context.Lookups.Where(l => l.Key == request.Key).ProjectTo<LookupDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}