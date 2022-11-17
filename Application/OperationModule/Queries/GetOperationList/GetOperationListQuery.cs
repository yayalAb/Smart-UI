using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.OperationModule.Queries.GetOperationList{
    public record GetOperationListQuery : IRequest<List<OperationDto>>{
      
    }

    public class GetOperationListQueryHandler : IRequestHandler<GetOperationListQuery, List<OperationDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetOperationListQueryHandler(IAppDbContext context , IMapper mapper , ILogger<GetOperationListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<OperationDto>> Handle(GetOperationListQuery request, CancellationToken cancellationToken)
        {
          return  _context.Operations
            .OrderBy(p => p.Id)
            .ProjectTo<OperationDto>(_mapper.ConfigurationProvider).ToList();
            
        }
    }



}