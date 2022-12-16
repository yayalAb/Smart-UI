using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.GoodModule.Queries.GetAllGoodQuery;
using AutoMapper;
using Application.ContainerModule;
using Application.GoodModule.Commands.AssignGoodsCommand;
using Application.GoodModule.Commands.UpdateGoodCommand;
using Application.Common.Service;
using Domain.Common.Units;
using System.Reflection;

namespace Application.GoodModule.Queries.GetGoodQuery
{

    public class GetAssignedGoodQuery : IRequest<UpdateGoodCommand>
    {
        public int OperationId { get; init; }
    }

    public class GetAssignedGoodQueryHandler : IRequestHandler<GetAssignedGoodQuery, UpdateGoodCommand>
    {

        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;
        private readonly ILogger<GetAssignedGoodQueryHandler> _logger;

        public GetAssignedGoodQueryHandler( IMapper mapper, IAppDbContext context, ILogger<GetAssignedGoodQueryHandler> logger) {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<UpdateGoodCommand> Handle(GetAssignedGoodQuery request, CancellationToken cancellationToken) {
            
            var operation = await _context.Operations
                .Include(o => o.Containers)!
                    .ThenInclude(c => c.Goods)
                .Include(o => o.Goods)
                .Where(o => o.Id == request.OperationId)
                .Select(o => new UpdateGoodCommand
                {
                    OperationId = o.Id,
                    Containers = _mapper.Map<List<UpdateGoodContainerDto>>(o.Containers),
                    Goods = _mapper.Map<List<UpdateGoodDto>>(o.Goods!.Where(g => g.ContainerId == null))
                }).FirstOrDefaultAsync();

            if (operation == null) {
                throw new GhionException(CustomResponse.NotFound($"operation with id = {request.OperationId} is not found"));
            }

            return operation;

        }

    }
}