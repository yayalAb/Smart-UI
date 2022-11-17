
using Application.Common.Interfaces;
using MediatR;
using AutoMapper;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;
using Application.Common.Mappings;

namespace Application.OperationModule.Queries.GetOperationPaginatedList;

public class GetOperationPaginatedListQuery : IRequest<PaginatedList<OperationDto>> {
    public int pageNumber {get; init; }
    public int pageSize {get; init; }
}

public class GetOperationPaginatedListQueryHandler : IRequestHandler<GetOperationPaginatedListQuery, PaginatedList<OperationDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetOperationPaginatedListQueryHandler( IAppDbContext context , IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OperationDto>> Handle(GetOperationPaginatedListQuery request, CancellationToken cancellationToken) {
        return await _context.Operations
        .ProjectTo<OperationDto>(_mapper.ConfigurationProvider)
        .PaginatedListAsync(request.pageNumber , request.pageSize);
    }

}