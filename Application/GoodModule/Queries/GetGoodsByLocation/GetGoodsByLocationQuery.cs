using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Application.Common.Models;

using Application.Common.Exceptions;

namespace Application.GoodModule.Queries.GetGoodsByLocation;

public class GetGoodsByLocationQuery : IRequest<List<FetchGoodDto>> {
    public int? OperationId {set; get;}
    public string? Location {get; set;}
}
public class GetGoodsByLocationQueryHandler: IRequestHandler<GetGoodsByLocationQuery, List<FetchGoodDto>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetGoodsByLocationQueryHandler(
        IIdentityService identityService, 
        IAppDbContext context,
        IMapper mapper
    ){
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FetchGoodDto>> Handle(GetGoodsByLocationQuery request, CancellationToken cancellationToken) {
              if (request.OperationId != null && request.Location != null)
        {
            return await _context.Goods
                        .Where(c => c.OperationId == request.OperationId && c.Location == request.Location)
                        .ProjectTo<FetchGoodDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        if (request.OperationId == null && request.Location != null)
        {
            return await _context.Goods
                        .Where(c => c.Location == request.Location)
                        .ProjectTo<FetchGoodDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        if(request.OperationId !=null && request.Location == null){
            return await _context.Goods
                        .Where(c => c.OperationId == request.OperationId)
                        .ProjectTo<FetchGoodDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
       throw new GhionException(CustomResponse.BadRequest("atleast one query param must be provided for filtering"));
    }

}