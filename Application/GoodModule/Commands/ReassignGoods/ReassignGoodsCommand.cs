

using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Domain.Enums;

namespace Application.GoodModule.Commands.AssignGoodsCommand
{

    public record ReassignGoodsCommand : IRequest<CustomResponse>
    {
        public int ContainerId { get; init; }
        public List<int> GoodId { get; init; }

    }

    public class ReassignGoodsCommandHandler : IRequestHandler<ReassignGoodsCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<AssignGoodsCommandHandler> _logger;
        private readonly IMapper _mapper;

        public ReassignGoodsCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<AssignGoodsCommandHandler> logger,
            IMapper mapper
        ) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse> Handle(ReassignGoodsCommand request, CancellationToken cancellationToken) {


            var container = await _context.Containers.FindAsync(request.ContainerId);
            if(container == null){
                throw new GhionException(CustomResponse.NotFound("Container not found!"));
            }

            var goods = _context.Goods.Where(g => request.GoodId.Contains(g.Id)).Select(g => new Good{
                Description = g.Description,
                HSCode = g.HSCode,
                Manufacturer = g.Manufacturer,
                Weight = g.Weight,
                Quantity = g.Quantity,
                NumberOfPackages = g.NumberOfPackages,
                Type = g.Type,
                Location = g.Location,
                ChasisNumber = g.ChasisNumber,
                EngineNumber = g.EngineNumber,
                ModelCode = g.ModelCode,
                IsAssigned = g.IsAssigned,
                Unit = g.Unit,
                UnitPrice = g.UnitPrice,
                CBM = g.CBM,
                ContainerId = request.ContainerId,
                OperationId = g.OperationId,
                TruckAssignmentId = g.TruckAssignmentId,
                LocationPortId = g.LocationPortId,
            });

            _context.Goods.UpdateRange(goods);
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Goods are reassigned");

        }

    }

};