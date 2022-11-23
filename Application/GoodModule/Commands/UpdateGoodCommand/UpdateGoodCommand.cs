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
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? HSCode { get; set; }
        public string? Manufacturer { get; set; }
        public float? Weight { get; set; }
        public float? Quantity { get; set; }
        public int? NumberOfPackages { get; set; }
        public string Type { get; set; }
        public string ChasisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string ModelCode { get; set; }
        public int? ContainerId { get; set; }
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

            if (found_good == null)
            {
                throw new Exception("Good not found");
            }

            //check if the container is Changed
            if (found_good.ContainerId != request.ContainerId)
            {

                Container found_container = await _context.Containers.FindAsync(request.ContainerId);

                if (found_container == null)
                {
                    throw new Exception("Container not found");
                }

                found_good.ContainerId = request.ContainerId;

            }
            found_good.Description = request.Description;
            found_good.HSCode = request.HSCode;
            found_good.Manufacturer = request.Manufacturer;
            found_good.Weight = request.Weight;
            found_good.Quantity = request.Quantity;
            found_good.NumberOfPackages = request.NumberOfPackages;
            found_good.Type = request.Type;
            found_good.ChasisNumber = request.ChasisNumber;
            found_good.EngineNumber = request.EngineNumber;
            found_good.ModelCode = request.ModelCode;






            Good new_good = _mapper.Map<Good>(request);
            _context.Goods.Add(new_good);
            await _context.SaveChangesAsync(cancellationToken);

            return new_good;

        }

    }

}