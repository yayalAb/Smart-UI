using System.Reflection.Metadata;
using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Application.GoodModule.Commands.UpdateGoodCommand
{

    public record UpdateGoodCommand : IRequest<Good>
    {
        public string Id {get; init;}
        public string? Description { get; init; }
        public string? HSCode { get; init; }
        public string? Manufacturer { get; init; }
        public string? CBM { get; init; }
        public float? Weight { get; init; }
        public float? Quantity { get; init; }
        public float? UnitPrice { get; init; }
        public string? UnitOfMeasurnment { get; init; }
        public int ContainerId { get; init; }
    }


    public class UpdateGoodCommandHandler : IRequestHandler<UpdateGoodCommand, Good>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateGoodCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateGoodCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<UpdateGoodCommandHandler> logger,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Good> Handle(UpdateGoodCommand request, CancellationToken cancellationToken)
        {

            Good found_good = await _context.Goods.FindAsync(request.Id);

            if (found_good == null) {
                throw new Exception("Good not found");
            }

            //check if the container is Changed
            if(found_good.ContainerId != request.ContainerId) {
                
                Container found_container = await _context.Containers.FindAsync(request.ContainerId);

                if (found_container == null) {
                    throw new Exception("Container not found");
                }

                found_good.ContainerId = request.ContainerId;

            }

            found_good.CBM = request.CBM;
            found_good.Description = request.Description;
            found_good.HSCode = request.HSCode;
            found_good.Manufacturer = request.Manufacturer;
            found_good.UnitOfMeasurnment = request.UnitOfMeasurnment;
            found_good.UnitPrice = request.UnitPrice;
            found_good.Weight = request.Weight;
            found_good.Quantity = request.Quantity;

            Good new_good = _mapper.Map<Good>(request);
            _context.Goods.Add(new_good);
            await _context.SaveChangesAsync(cancellationToken);

            return new_good;

        }

    }

}